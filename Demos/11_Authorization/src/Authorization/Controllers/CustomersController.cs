using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Models;
using Authentication.Policies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Authentication.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IAuthorizationService _authService;

        public CustomersController(IAuthorizationService authService)
        {
            _authService = authService;
        }

        public IActionResult Edit()
        {
            // Hard code the customer for demo purposes
            var vm = new CustomersViewModel
            {
                CustomerName = "Global Knowledge",
                Location = "Vienna"
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string customerName, string location)
        {
            // Pretend we are updating the customer
            var customer = new Customer
            {
                CustomerName = customerName,
                Location = location
            };

            // Enforce authorization policy
            var authorized = await _authService.AuthorizeAsync(User,
                customer, CustomerOperations.Edit);
            if (!authorized)
            {
                return new ChallengeResult();
            }

            var vm = new CustomersViewModel
            {
                CustomerName = customer.CustomerName,
                Location = customer.Location
            };

            return View("Edit", vm);
        }
    }
}
