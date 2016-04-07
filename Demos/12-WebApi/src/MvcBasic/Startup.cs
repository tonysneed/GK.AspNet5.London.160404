using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using MvcBasic.Repositories;

namespace MvcBasic
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Web API only needs MvcCore types
            services.AddMvcCore(options =>
            {
                options.InputFormatters.Add(new JsonInputFormatter());
                options.OutputFormatters.Add(new JsonOutputFormatter());
                options.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            // Add services
            services.AddTransient<IProductsRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            // We'll use attribute-based routing
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
