using HelloMvcWithDI.Patterns;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloMvcWithDI.Controllers
{
    [Route("[controller]"), Route("")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: /<controller>/
        [Route("[action]"), Route("")]
        public IActionResult Index()
        {
            var products = _productRepository.GetProducts();
            //return View(products);
            return Json(products);
        }
    }
}
