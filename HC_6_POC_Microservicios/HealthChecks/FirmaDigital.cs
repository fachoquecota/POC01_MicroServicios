using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace HC_6_POC_Microservicios.HealthChecks
{
    public class FirmaDigital : IHealthCheck
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        public FirmaDigital(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_apiUrl = "https://localhost:7190/api/Process";
            _apiUrl = "https://f1cy1su3m7.execute-api.us-east-1.amazonaws.com/dev/upload/";
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                var requestContent = new StringContent("", Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(_apiUrl, requestContent, cancellationToken);
                //var response = await _httpClient.GetAsync(_apiUrl, cancellationToken);
                stopwatch.Stop();
                var latency = stopwatch.ElapsedMilliseconds;

                if (response.IsSuccessStatusCode)
                {
                    if (latency <= 5)
                    {
                        return HealthCheckResult.Healthy("La API está funcionando correctamente. Latencia: " + latency + " ms.");
                    }
                    else if (latency >= 6 && latency <= 15)
                    {
                        return HealthCheckResult.Degraded("La API está funcionando con lentitud. Latencia: " + latency + " ms.");
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy("La API tarda demasiado en responder. Latencia: " + latency + " ms.");
                    }
                }
                else
                {
                    return HealthCheckResult.Unhealthy("La API está en un estado no saludable. Latencia: " + latency + " ms.");
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("La API no está disponible: " + ex.Message);
            }
        }
    }
}
