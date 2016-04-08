using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Authentication.Models;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Authentication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl)
        {
            var vm = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                Providers = HttpContext.Authentication
                    .GetAuthenticationSchemes()
                    .Where(x => !string.IsNullOrWhiteSpace(x.DisplayName))
            };
            return View("Login", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate the user credentials
                // Match username and password - for demo purposes only
                if (model.Username != model.Password)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View("Login", model);
                }

                // Create subject and name claims
                var claims = new List<Claim>
                {
                    new Claim("sub", model.Username),
                    new Claim("name", model.Username),
                };

                // Assign claims for authorization
                // In a real world scenario, look up in a data store
                if (model.Username.ToUpper() == "BOB")
                {
                    claims.Add(new Claim("role", "Sales"));
                    claims.Add(new Claim("Location", "Kosice"));
                }
                if (model.Username.ToUpper() == "ALICE")
                {
                    claims.Add(new Claim("Location", "London"));
                }

                // Create claims identity and principle
                var ci = new ClaimsIdentity(claims, "password", "name", "role");
                var cp = new ClaimsPrincipal(ci);

                // Sign in the user using the claims principle
                await HttpContext.Authentication.SignInAsync("Cookies", cp);

                // Redirect to the return url if present,
                // otherwise redirect to root
                if (model.ReturnUrl != null && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return Redirect("~/");
            }

            return View("Login", model);
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.Authentication.SignOutAsync("Cookies");
                return RedirectToAction("Logout");
            }
            return View("LoggedOut");
        }
    }
}
