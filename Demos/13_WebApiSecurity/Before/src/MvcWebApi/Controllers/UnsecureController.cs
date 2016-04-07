using Microsoft.AspNet.Mvc;

namespace MvcWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UnsecureController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var values = new [] { "value1", "value2", "value3", "value4", "value5" };
            return Ok(values);
        }
    }
}
