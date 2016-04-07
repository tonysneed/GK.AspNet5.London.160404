using Microsoft.Owin.Hosting;
using Serilog;
using System;

namespace OidcServer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "IdentityServer3";

            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .LiterateConsole(outputTemplate: "{Timestamp:HH:MM} [{Level}] ({Name:l}){NewLine} {Message}{NewLine}{Exception}")
                .CreateLogger();

            const string url = "https://web.local:4444/core";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("\n\nServer listening at {0}. Press enter to stop", url);
                Console.ReadLine();
            }
        }
    }
}