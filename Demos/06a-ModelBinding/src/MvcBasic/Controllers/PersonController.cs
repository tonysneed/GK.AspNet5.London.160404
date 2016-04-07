using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MvcBasic.Models;
using MvcBasic.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcBasic.Controllers
{
    public class PersonController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var vm = new DelegateViewModel()
            {
                Person = new Person
                {
                    Name = "Joe",
                    Age = 30
                },
            };
            return View(vm);
        }

        /* [HttpPost]
        public IActionResult Update(string name, int age)
        {
            var vm = new PersonViewModel
            {
                Name = name,
                Age = age
            };
            return View("Index", vm);
        } */

        [HttpPost]
        public IActionResult Update(DelegateViewModel delegateViewModel)
        {
            return View("Index", delegateViewModel);
        }
    }
}
