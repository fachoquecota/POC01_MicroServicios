using Microsoft.AspNetCore.Mvc;
using Process_02.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Process_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        // GET: api/<ProcessController>
        [HttpGet]
        public IEnumerable<Tarea> Get()
        {
            return new List<Tarea>
            {
                new Tarea { id = 20, Tarea1 = "CONSULTA A PROCESS 2" }
            };
        }

        //// GET api/<ProcessController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ProcessController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ProcessController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ProcessController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
