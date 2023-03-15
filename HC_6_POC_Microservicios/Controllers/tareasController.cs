using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HC_6_POC_Microservicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tareasController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HealthCheckService _healthCheckService;

        public tareasController(IHttpClientFactory clientFactory, HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
            _clientFactory = clientFactory;
        }
        
        [HttpGet("health")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                var client = _clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Get,
                    "https://localhost:7190/api/Process");

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    watch.Stop();
                    var elapsedMs = watch.ElapsedMilliseconds;
                    if (elapsedMs > 3)
                    {
                        var json = new
                        {
                            status = "Trabajando",
                            responseTimeMs = elapsedMs
                        };
                        
                        return Ok(HealthCheckResult.Degraded("Trabajando"));
                    }
                    else
                    {
                        var json = new
                        {
                            status = "Saludable Disponible",
                            responseTimeMs = elapsedMs
                        };
                        return Ok(json);
                    }
                    

                    
                }
                else
                {
                    var json = new
                    {
                        status = "unhealthy",
                        responseTimeMs = -1
                    };

                    return StatusCode(500, json);
                }
            }
            catch (Exception ex)
            {
                var json = new
                {
                    status = "unhealthy",
                    responseTimeMs = -1,
                    message = ex.Message
                };

                return StatusCode(500, json);
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        //public tareasController(IHttpClientFactory clientFactory)
        //{
        //    _clientFactory = clientFactory;
        //}

        // GET: api/<productosController>
        [HttpGet]
        [Route("Tarea1")]
        public async Task<IActionResult> Get_Tarea1()
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7190/api/Process");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpGet]
        [Route("Tarea2")]
        public async Task<IActionResult> Get_Tarea2()
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7044/api/Process");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }






        [HttpGet]
        [Route("crearArchivos")]
        public async Task<IActionResult> Get_Tarea3(string prefix, int cantidad)
        {
            try
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("C:\\Users\\choquecota\\Desktop\\0010");

                foreach (System.IO.FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                string directoryPath = "C:\\Users\\choquecota\\Desktop\\0010"; // ruta de la carpeta donde se crearán los archivos
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                for (int i = 0; i < cantidad; i++)
                {
                    string fileName = prefix + Guid.NewGuid().ToString() + "_684060055" + ".txt";
                    string filePath = Path.Combine(directoryPath, fileName);
                    using (StreamWriter writer = System.IO.File.CreateText(filePath))
                    {
                        await writer.WriteLineAsync("{\"enterpriseCodeGE\":\"F00\",\"sentId\":11682680,\"emailSentId\":753178202,\"contentLength\":0,\"emailCC\":\"\",\"emlPath\":\"\",\"email\":\"lidayllaconza@gmail.com\",\"customerName\":\"CFC ABASTECEDORES INDUSTRIALES SOCIEDAD ANONIMA CERRADA\",\"emailsubject\":\"RECIBO S43000116796-5207714-2022-12\",\"emailcontent\":\"\",\"reference1\":\"20607732133\",\"reference2\":\"RUC\",\"reference3\":\"S43000116796-5207714-2022-12.pdf;Conoce como Pagar\",\"reference4\":\"CFC ABASTECEDORES INDUSTRIALES SOCIEDAD ANONIMA CE\",\"reference5\":\"5207714\",\"reference6\":\"1\",\"reference7\":\"S43000116796\",\"reference8\":\"27/01/2023\",\"reference9\":\"RUC\",\"reference10\":\"ACTIVO\",\"reference11\":\"\",\"reference12\":\"\",\"reference13\":\"\",\"reference14\":\"\",\"reference15\":\"\",\"bounceDate\":\"\",\"readingDate\":\"\",\"cutoffDate\":\"2023-01-12T05:00:00.000Z\",\"sentDate\":\"2023-01-12T22:14:53.030Z\",\"emailStatusCode\":\"5\",\"resend\":[{    \r\n            \"resendId\" : \"663242255\",    \r\n            \"email\" :\"contabilidad@mqaperu.com.pe\",    \r\n            \"user\" :\"danielpozo.entel\",    \r\n            \"status\" :\"Enviado\",    \r\n            \"sentDate\" :\"Ene 16 2023  4:37PM\",    \r\n            \"readDate\" :\"\",    \r\n            \"idStatus\" : \"5\",    \r\n            \"bounceReason\":\"\"    \r\n        }],\"documentNumber\":\"\",\"reserved9\":\"\",\"accountNumber\":\"\",\"cardNumber\":\"\",\"details\":[{    \r\n       \"IsTablet\" :\"0\",    \r\n       \"IsDesktop\" :\"0\",    \r\n       \"IsMobile\" :\"0\",    \r\n       \"IsTouchEnabled\" :\"0\",    \r\n       \"IsBot\" :\"0\",    \r\n       \"Is_EU\" :\"0\",    \r\n       \"TimeZone_IsDst\" :\"0\",    \r\n       \"Threat_IsTor\" :\"0\",    \r\n       \"Threat_IsProxy\" :\"0\",    \r\n       \"Threat_IsAnonymous\" :\"0\",    \r\n       \"Threat_IsKnownAttacker\" :\"0\",    \r\n       \"Threat_IsKnownAbuser\" :\"0\",    \r\n       \"Threat_IsThreat\" :\"0\",    \r\n       \"Threat_IsBogon\" :\"0\",    \r\n       \"idMessageSend\" :\"671820128\",    \r\n       \"idMessageSendTo\" :\"1\",    \r\n       \"idMessageState\" :\"1\",    \r\n       \"description\" :\"Registrado\",    \r\n       \"to\" :\"cotizaciones@mqaperu.com.pe\",    \r\n       \"toName\" :\"CFC ABASTECEDORES INDUSTRIALES SOCIEDAD ANONIMA CERRADA\",    \r\n       \"bounceDate\" :\"\",    \r\n       \"bounceReason\" :\"\",    \r\n       \"readDate\" :\"\",    \r\n       \"userDetail\" :\"danielpozo.entel\",    \r\n       \"messageState\" :\"Registrado\",    \r\n       \"errorReason\" :\"\",    \r\n       \"bounceReasonDetails\" :\"\",    \r\n       \"errorReasonDetails\" :\"\",    \r\n       \"idMessageSendNew\" :\"671820128\",    \r\n       \"idMessageSendToNew\" :\"682849944\",    \r\n       \"idMessageStateNew\" :\"1\",    \r\n       \"descriptionNew\" :\"Registrado\",    \r\n       \"toNew\" :\"cotizaciones@mqaperu.com.pe\",    \r\n       \"toNameNew\" :\"CFC ABASTECEDORES INDUSTRIALES SOCIEDAD ANONIMA CERRADA\",    \r\n       \"bounceReasonNew\" :\"\",    \r\n       \"userDetailNew\" :\"danielpozo.entel\",    \r\n       \"messageStateNew\" :\"Registrado\",    \r\n       \"errorReasonNew\" :\"\",    \r\n       \"bounceReasonDetailsNew\" :\"\",    \r\n       \"errorReasonDetailsNew\" :\"\",    \r\n       \"bounceDateNew\" :\"\",    \r\n       \"IpAddress\" :\"\",    \r\n       \"UserAgent\" :\"\",    \r\n       \"UrlReferrer\" :\"\",    \r\n       \"AbsolutePath\" :\"\",    \r\n       \"PathAndQuery\" :\"\",    \r\n       \"Host\" :\"\",    \r\n       \"Port\" :\"0\",    \r\n       \"Browser_Type\" :\"\",    \r\n       \"Browser_Name\" :\"\",    \r\n       \"Browser_Version\" :\"\",    \r\n       \"DeviceType\" :\"\",    \r\n       \"OS_Name\" :\"\",    \r\n       \"OS_Version\" :\"\",    \r\n       \"OS_Platform\" :\"\",    \r\n       \"BrandName\" :\"\",    \r\n       \"Model\" :\"\",    \r\n       \"City\" :\"\",    \r\n       \"Region\" :\"\",    \r\n       \"Region_Code\" :\"\",    \r\n       \"Country_Name\" :\"\",    \r\n       \"Country_Code\" :\"\",    \r\n       \"Continent_Name\" :\"\",    \r\n       \"Continent_Code\" :\"\",    \r\n       \"Latitude\" :\"\",    \r\n       \"Longitude\" :\"\",    \r\n       \"Postal\" :\"\",    \r\n       \"Calling_Code\" :\"\",    \r\n       \"Flag\" :\"\",    \r\n       \"Emoji_Flag\" :\"\",    \r\n       \"Emoji_Unicode\" :\"\",    \r\n       \"Asn_Asn\" :\"\",    \r\n       \"Asn_Name\" :\"\",    \r\n       \"Asn_Domain\" :\"\",    \r\n       \"Asn_Route\" :\"\",    \r\n       \"Asn_Type\" :\"\",    \r\n       \"Languages_Name\" :\"\",    \r\n       \"Languages_Native\" :\"\",    \r\n       \"Currency_Name\" :\"\",    \r\n       \"Currency_Code\" :\"\",    \r\n       \"Currency_Symbol\" :\"\",    \r\n       \"Currency_Native\" :\"\",    \r\n       \"Currency_Plural\" :\"\",    \r\n       \"TimeZone_Name\" :\"\",    \r\n       \"TimeZone_Abbr\" :\"\",    \r\n       \"TimeZone_Offset\" :\"\",    \r\n       \"TimeZone_CurrentTime\" :\"\"    \r\n             }],\"detailsStates\":[{     \r\n    \"status\":\"Registrado\",    \r\n    \"statusCount\":\"1\"    \r\n   }],\"detailsCount\":4,\"attachmentPath\":\"\",\"enterpriseCode\":\"01009\",\"productCode\":\"F00\",\"direccionFirmaWSUrbano\":\"\",\"dowloadIndicator\":\"\",\"cargoDate\":\"\",\"denomination\":\"\",\"user\":\"ElMensajero\",\"correlative\":\"\",\"descriptionState\":\"\",\"receptionDate\":\"\",\"bounceReason\":\"\",\"bounceReasonDetails\":\"\",\"errorReason\":\"\",\"errorReasonDetails\":\"\",\"originType\":\"Outsourcing\",\"otp\":0,\"totalPagPdf\":\"0\",\"messageCode\":\"\",\"pdfTotalPages\":0,\"enterprise\":\"5dae2de312fa930b6046200b\",\"product\":\"5e8668f502da59c92e8f6e3f\",\"productName\":\"SERVICIOS FIJOS ENTEL\",\"emailStatus\":\"\",\"registeredAt\":\"2023-02-21T10:07:56.270Z\",\"year\":\"2023\",\"month\":\"1\",\"day\":\"12\",\"statusConcatenate\":\"\",\"statusValues\":\"\",\"flagSync\":\"1\",\"yearNew\":\"2023\",\"monthNew\":\"1\",\"dayNew\":\"12\",\"parameters\":[],\"registrationdate\":\"2023-02-21T10:07:56.270Z\"}");
                    }
                }
                return Ok("Archivos creados correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        



        //// GET api/<productosController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<productosController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<productosController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<productosController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
