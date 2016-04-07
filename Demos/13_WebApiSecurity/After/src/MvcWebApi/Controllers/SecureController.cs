using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace MvcWebApi.Controllers
{
    [Route("api/[controller]")]
    public class SecureController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var claims = from c in User.Claims
                        select new { c.Type, c.Value };
            return Json(claims);
        }
    }
}
