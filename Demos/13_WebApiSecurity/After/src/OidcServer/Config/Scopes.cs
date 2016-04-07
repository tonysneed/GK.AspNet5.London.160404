using IdentityServer3.Core.Models;
using System.Collections.Generic;
using Scope = IdentityServer3.Core.Models.Scope;

namespace OidcServer.Config
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            return new[]
            {
                ////////////////////////
                // identity scopes
                ////////////////////////

                StandardScopes.OpenId,
                StandardScopes.ProfileAlwaysInclude,
                StandardScopes.EmailAlwaysInclude,

                ////////////////////////
                // resource scopes
                ////////////////////////

                new Scope
                {
                    Name = "api1",
                    DisplayName = "My API",
                    Type = ScopeType.Resource,

                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                }
            };
        }
    }
}