using Authentication.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Authentication.Controllers
{
    // Authorize based on Sales policy
    [Authorize("SalesPolicy")]
    public class SalesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var vm = new SalesViewModel {SalesTotal = 100000};
            return View(vm);
        }
    }
}
