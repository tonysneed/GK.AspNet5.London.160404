using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MvcBasic.Filters;
using MvcBasic.Repositories;

namespace MvcBasic
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISampleRepository, SampleRespository>();
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    // Resource filter not really needed
                    //options.Filters.Add(typeof(DemoResourceFilter));
                    options.Filters.Add(typeof(DemoActionResultFilter));
                    options.Filters.Add(typeof(DemoExceptionFilter));
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseRuntimeInfoPage();
                app.UseDeveloperExceptionPage();
            }

            // Configure logger factory (for ex, use NLog)
            loggerFactory.MinimumLevel = LogLevel.Error;
            loggerFactory.AddConsole();

            app.UseMvc(routes => 
                routes.MapRoute("default",
                    "{controller=Home}/{action=Index}/{id?}"));
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
