using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Text;

namespace HC_6_POC_Microservicios.HealthChecks
{
    public class Process_01 : IHealthCheck
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;
        private Random random = new Random();

        public Process_01(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiUrl = "https://localhost:7190/api/Process";
            //_apiUrl = "http://34.199.24.55:8058/registrarencolamientoservice";
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                //var requestContent = new StringContent("", Encoding.UTF8, "application/json");
                //var response = await _httpClient.PostAsync(_apiUrl, requestContent, cancellationToken);
                var response = await _httpClient.GetAsync(_apiUrl, cancellationToken);
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
                        return HealthCheckResult.Unhealthy("La API no responde. Latencia: " + latency + " ms.");
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
//{
//    public class LatencyHealthCheck : IHealthCheck
//{
//    private Random random = new Random();
//    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
//        CancellationToken cancellationToken = default)
//    {
//        int latency = random.Next(1, 15);
//        if (latency < 5)
//        {
//            return Task.FromResult(
//                HealthCheckResult.Healthy("Ltencia -- ok(" + latency + ")."));
//        }
//        else if (latency < 10)
//        {
//            return Task.FromResult(
//                HealthCheckResult.Degraded("Latencia lenta, puede ser desaprobada ok(" + latency + ")."));
//        }
//        else
//        {
//            return Task.FromResult(
//                HealthCheckResult.Unhealthy("No sano ok(" + latency + ")."));
//        }

//    }
//}
//}