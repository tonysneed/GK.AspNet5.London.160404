using System;
using Microsoft.AspNet.Mvc;
using MvcBasic.Filters;
using MvcBasic.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcBasic.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var vm = new HomeViewModel {Name = "Developer"};
            return View(vm);
        }

        //[ServiceFilter(typeof(DemoExceptionFilter))] // does not work in RC1

        // Use TypeFilter to satisfy dependencies in the filter 
        //[TypeFilter(typeof(DemoExceptionFilter))]
        [Route("/error")]
        public IActionResult SimulateError()
        {
            throw new Exception("Doh!");
        }
    }
}
