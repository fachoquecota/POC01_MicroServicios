using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace HC_6_POC_Microservicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class txttestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            // Leer el archivo de texto que contiene el JSON
            string jsonFile = "D:\\Texto\\pedrini.txt";
            string json = System.IO.File.ReadAllText(jsonFile);

            // Deserializar el JSON a una lista de objetos
            List<MyObject> objects = JsonConvert.DeserializeObject<List<MyObject>>(json);

            // Procesar los objetos y obtener una lista con los datos deseados
            List<string> data = objects.Select(o => $"{o.FunctionName};{o.Runtime};{o.Tags};{o.Region}").ToList();

            // Guardar los datos en otro archivo de texto
            string outputFile = "D:\\Texto\\txt_data.txt";
            System.IO.File.WriteAllLines(outputFile, data);

            return Ok();
        }
            
        public class MyObject
        {
            public string FunctionName { get; set; }
            public string Runtime { get; set; }
            public object Tags { get; set; }
            public string Region { get; set; }
        }
    }
}
