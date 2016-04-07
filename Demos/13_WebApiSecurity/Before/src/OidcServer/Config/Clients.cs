using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace OidcServer.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Resource Owner Flow Client",
                    Enabled = true,
                    ClientId = "ro.client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },

                    Flow = Flows.ResourceOwner,

                    // only allowed to access api1
                    AllowedScopes = new List<string>
                    {
                        "api1"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:64112",
                    },

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    AbsoluteRefreshTokenLifetime = 86400,
                    SlidingRefreshTokenLifetime = 43200,

                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Sliding
                },
            };
        }
    }
}