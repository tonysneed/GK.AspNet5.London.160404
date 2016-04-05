using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.OptionsModel;

namespace HelloConfig
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            if (env.IsProduction())
            {
                builder.AddJsonFile("appsettings.prod.json");
            }
            builder.AddEnvironmentVariables();
            builder.AddUserSecrets();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<Copyright>(Configuration.GetSection("copyright"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IOptions<Copyright> copyrightOpts)
        {
            app.UseIISPlatformHandler();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");

                //IConfigurationSection copyright = Configuration.GetSection("copyright");
                //int year = int.Parse(Configuration.GetSection("copyright:year").Value);
                //string company = Configuration.GetSection("copyright:company").Value;

                var year = copyrightOpts.Value.Year;
                var company = copyrightOpts.Value.Company;

                await context.Response.WriteAsync($"Copyright {year} {company}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
