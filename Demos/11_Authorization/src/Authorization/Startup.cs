
using System;
using Authentication.Policies;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Require authenticated users
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                //.AddRequirements(new SalesRoleRequirement())
                .Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SalesPolicy", builder =>
                {
                    builder.RequireAuthenticatedUser()
                        .RequireRole("Sales");
                    //.RequireClaim() // Add other claims
                });
            });

            // Add Mvc types
            services.AddMvc(options =>
            {
                // Add global auth filter
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            // Add authorization handlers
            services.AddTransient<IAuthorizationHandler, MustBeFromSameLocationToEditHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseRuntimeInfoPage();
                app.UseDeveloperExceptionPage();
            }

            // Use cookie authentication
            app.UseCookieAuthentication(options =>
            {
                options.AuthenticationScheme = "Cookies";
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
            });

            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            app.UseMvc(routes => 
                routes.MapRoute("default",
                    "{controller=Home}/{action=Index}/{id?}"));
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
