using HelloMvc.Models;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloMvc.Controllers
{
    //[Route(""), Route("Home")]
    [Route("[controller]"), Route("")]
    public class HomeController : Controller
    {
        // GET: /<controller>/
        //[Route(""), Route("Index")]
        [Route("[action]"), Route("")]
        public IActionResult Index()
        {
            //ViewData["name"] = "Tony";
            //ViewBag.lastname = "Sneed";
            var vm = new CustomerViewModel
            {
                FirstName = "John",
                LastName = "Lennon"
            };
            return View(vm);
        }

        // POST
        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit()
        {
            return View();
        }

        // GET: /<controller>/5
        [Route("{id:int}")]
        public IActionResult Index(int id)
        {
            return Content($"Index with id {id}");
        }

        // GET: hello?city=London
        [Route("Hello/{city?}")]
        public IActionResult SayHello(string city = "London")
        {
            return Content($"Hello {city}");
        }

        [Route("Goodbye/{city?}")]
        public IActionResult SayGoodbye(string city = "London")
        {
            return Content($"Goodbye {city}");
        }
    }
}
