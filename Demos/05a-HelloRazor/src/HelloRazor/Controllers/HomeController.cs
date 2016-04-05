using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloRazor.Models;
using HelloRazor.Services;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloRazor.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyService _service;

        public HomeController(CompanyService service)
        {
            _service = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var vm = new CompanyViewModel
            {
                CompanyName = "Global Knowledge",
                EmployeeCount = _service.GetEmployeeCount()
            };
            return View(vm);
        }
    }
}
