using Process_03.Interface;

namespace Process_03.Services
{
    public class ProductService : IProductService
    {
        public List<string> GetProducts()
        {
            // Simula la obtención de datos de productos de una fuente de datos
            return new List<string> { "Producto 1", "Producto 2", "Producto 3" };
        }
    }
}
