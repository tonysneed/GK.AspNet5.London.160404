using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.DependencyInjection;

namespace HelloPipeline
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            // Logging middleware
            app.Use(async (ctx, next) =>
            {
                Console.WriteLine($"Pre-processing request: {ctx.Request.Path}");
                await next();
                Console.WriteLine($"Post-processings response: {ctx.Response.StatusCode}");
            });

            app.Map("/hello", builder =>
            {
                builder.Run(async (HttpContext ctx) =>
                {
                    await ctx.Response.WriteAsync("Hello ASP.NET 5 App");
                });
            });

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = ""
            });

            // Insert terminating middleware
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
