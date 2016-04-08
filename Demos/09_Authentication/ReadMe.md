# Authentication Demo ReadMe

1. Insert code in `Startup.ConfigureServices` to require authenticated users
  + Use a new auth policy builder to create a policy
    that requires users to be authenticated

    ```csharp
    // Require authenticated users
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    ```

  - In the AddMvc options, add a global auth filter

    ```csharp
    services.AddMvc(options =>
    {
        options.Filters.Add(new AuthorizeFilter(policy));
    });
    ```

  - Press Ctrl+5 to run the web app
    + You should now receive a 401 unauthorized response

2. Add a dependency to project.json:
  - Microsoft.AspNet.Authentication.Cookies

3. In `Startup.Configure` call `app.UseCookieAuthentication`
  - Place it *before* `app.UseStaticFiles`
  - Provide a callback lambda to set options
    + AuthenticationScheme = "Cookies"
    + AutomaticAuthenticate = true
    + AutomaticChallenge = true
    + ExpireTimeSpan = TimeSpan.FromMinutes(30)
    + SlidingExpiration = true
  - Run the app again. This time you should see:
    http://localhost:64115/Account/Login?ReturnUrl=%2F

4. Add a `LoginViewModel` to the Models folder
  - Include Username and Password properties
  - And also properties for ReturnUrl and Providers

    ```csharp
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
        public IEnumerable<AuthenticationDescription> Providers { get; set; }

    ```

5. Add an `AccountController` class to the Controllers folder
  - Add an `[AllowAnonymous]` attribute to the class
  - Add a `Login` action accepting a returnUrl string parameter 

    ```csharp
    public IActionResult Login(string returnUrl)
    {
        var vm = new LoginViewModel()
        {
            ReturnUrl = returnUrl,
            Providers = HttpContext.Authentication.GetAuthenticationSchemes().Where(x => !String.IsNullOrWhiteSpace(x.DisplayName))
        };
        return View("Login", vm);
    }
    ```

6. Add an Account folder to the Views folder,
   then add Login.cshtml view
  - Add dependencies in project.json:
    + Microsoft.AspNet.StaticFiles
    + Microsoft.AspNet.Mvc.TagHelpers
    + Microsoft.AspNet.Tooling.Razor
  - Call `app.UseStaticFiles` in `Startup.Configure`
    + Place before the call to `app.UseMvc`
  - Flesh out the Login view with inputs for username and password
  - If you run the app now, you should be redirected to the Login view

7. Implement another action method (also called Login) to accept the
   POST data for the LoginViewModel
  - In the POST action method, perform the standard MVC validation checks
    for the model-bound LoginViewModel
  - If the normal validation is successful, then check the Username and
    Password and hard-code any usernames and passwords you want to allow
  - If the credentials are invalid, use the MVC validation to report back
    an error message

    ```csharp
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Username != model.Password)
            {
                ModelState.AddModelError("", "Invalid username or password");
            }
        }

        return View("Login", model);
    }
    ```

8. Add code before the closing brace of the `if` statement
   to sign-in the user
  - Create subject and name claims
  - Create claims identity and principle
  - Sign in the user using the claims principle
  - Redirect to the return url
  - Run the app and login with matching username and password
    + You should be authenticated and redirected to the home page

    ```csharp
    var claims = new[]
    {
        new Claim("sub", model.Username),
        new Claim("name", model.Username),
    };

    var ci = new ClaimsIdentity(claims, "password", "name", "role");
    var cp = new ClaimsPrincipal(ci);

    await HttpContext.Authentication.SignInAsync("Cookies", cp);

    if (model.ReturnUrl != null && Url.IsLocalUrl(model.ReturnUrl))
    {
        return Redirect(model.ReturnUrl);
    }
    return Redirect("~/");

    ```
9. 