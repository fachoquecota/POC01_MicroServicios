using Microsoft.AspNetCore.Mvc;
using Process_04.Service;
namespace Process_04.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IService _service;
        public WeatherForecastController(IService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetData();
            return Ok(result);  
        }

    }
}