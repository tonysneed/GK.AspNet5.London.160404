using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using IdentityModel.Extensions;
using Newtonsoft.Json.Linq;

namespace NativeClient
{
    class Program
    {
        // To sniff HTTP traffic, append .fiddler to localhost
        private const string BaseAddress = "http://localhost:64112/api/";

        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to invoke unsecure service");
            Console.ReadLine();

            var values = CallService().Result;
            foreach (string value in values)
            {
                Console.WriteLine($"Value: {value}");
            }

            Console.WriteLine("\nContinue? {Y/N}");
            var proceed = Console.ReadLine().ToUpper() == "Y";
            if (!proceed) return;

            Console.WriteLine("\nCredentials:");
            Console.WriteLine("User name {alice, bob}:");
            var userName = Console.ReadLine();
            Console.WriteLine("Password {same as username}:");
            var password = Console.ReadLine();

            Console.WriteLine("\nPress Enter to request token");
            Console.ReadLine();
            var tokenResponse = RequestToken(userName, password);
            ShowResponse(tokenResponse);

            Console.WriteLine("\nPress Enter to invoke secure service");
            Console.ReadLine();

            var claims = CallSecureService(tokenResponse.AccessToken).Result;
            "Service claims:".ConsoleGreen();
            Console.WriteLine(claims);
        }

        private static async Task<IEnumerable<string>> CallService()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };

            var response = await client.GetAsync("unsecure");
            response.EnsureSuccessStatusCode();
            var values = await response.Content.ReadAsAsync<IEnumerable<string>>();
            return values;
        }

        private static async Task<JArray> CallSecureService(string accessToken)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseAddress)
            };

            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("secure");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var claims = JArray.Parse(json);
            return claims;
        }

        private static TokenResponse RequestToken(string userName, string password)
        {
            const string tokenEndpoint = "https://web.local:4444/core/connect/token";

            var client = new TokenClient(
                tokenEndpoint,
                "ro.client",
                "secret");

            var tokenResponse = client.RequestResourceOwnerPasswordAsync(userName, password, "api1").Result;
            return tokenResponse;
        }

        private static void ShowResponse(TokenResponse response)
        {
            if (!response.IsError)
            {
                "Token response:".ConsoleGreen();
                Console.WriteLine(response.Json);

                if (response.AccessToken.Contains("."))
                {
                    "\nAccess Token (decoded):".ConsoleGreen();

                    var parts = response.AccessToken.Split('.');
                    var header = parts[0];
                    var claims = parts[1];

                    Console.WriteLine(JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(header))));
                    Console.WriteLine(JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claims))));
                }
            }
            else
            {
                if (response.IsHttpError)
                {
                    "HTTP error: ".ConsoleGreen();
                    Console.WriteLine(response.HttpErrorStatusCode);
                    "HTTP error reason: ".ConsoleGreen();
                    Console.WriteLine(response.HttpErrorReason);
                }
                else
                {
                    "Protocol error response:".ConsoleGreen();
                    Console.WriteLine(response.Json);
                }
            }
        }
    }
}
