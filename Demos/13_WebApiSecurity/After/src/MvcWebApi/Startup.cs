using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace MvcWebApi
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
                .Build();

            // Add Mvc types
            services.AddMvc(options =>
            {
                // Add global auth filter
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            // Use Jwt bearer auth
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseJwtBearerAuthentication(options =>
            {
                options.Authority = "https://web.local:4444/core";
                options.RequireHttpsMetadata = false;

                options.Audience = "https://web.local:4444/core/resources";
                options.AutomaticAuthenticate = true;
            });

            // We'll use attribute-based routing
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
