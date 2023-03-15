using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Process_03.Interface;

namespace Process_03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            // Obtiene los productos utilizando el servicio inyectado
            return _productService.GetProducts();
        }
    }
}
