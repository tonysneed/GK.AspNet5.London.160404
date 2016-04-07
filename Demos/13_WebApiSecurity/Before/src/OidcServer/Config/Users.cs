using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace OidcServer.Config
{
    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser{Subject = "123", Username = "alice", Password = "alice", 
                    Claims = new Claim[]
                    {
                        new Claim("username", "alice"),
                        new Claim(Constants.ClaimTypes.Name, "Alice Smith"),
                        new Claim(Constants.ClaimTypes.GivenName, "Alice"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Smith"),
                        new Claim(Constants.ClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(Constants.ClaimTypes.Role, "Sales"),
                    }
                },
                new InMemoryUser{Subject = "456", Username = "bob", Password = "bob", 
                    Claims = new Claim[]
                    {
                        new Claim("username", "bob"),
                        new Claim(Constants.ClaimTypes.Name, "Bob Loblaw"),
                        new Claim(Constants.ClaimTypes.GivenName, "Bob"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Loblaw"),
                        new Claim(Constants.ClaimTypes.Email, "BobLoblaw@email.com"),
                        new Claim(Constants.ClaimTypes.Role, "Attorney"),
                        new Claim(Constants.ClaimTypes.Role, "Blogger"),
                    }
                },
            };
        }
    }
}