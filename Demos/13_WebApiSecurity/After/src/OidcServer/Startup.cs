using IdentityServer3.Core.Configuration;
using Owin;
using OidcServer.Config;

namespace OidcServer
{
    internal class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var factory = new IdentityServerServiceFactory();
            factory
                .UseInMemoryClients(Clients.Get())
                .UseInMemoryScopes(Scopes.Get())
                .UseInMemoryUsers(Users.Get());

            var options = new IdentityServerOptions
            {
                SiteName = "IdentityServer3",
                
                SigningCertificate = Certificate.Get(),
                Factory = factory
            };

            appBuilder.UseIdentityServer(options);
        }
    }
}