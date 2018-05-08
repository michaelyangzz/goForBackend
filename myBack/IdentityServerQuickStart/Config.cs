using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerQuickStart
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> { new ApiResource("api1", "My API") };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
                //new Client
                //{
                //    ClientId="client",

                //    AllowedGrantTypes = GrantTypes.ClientCredentials,

                //    ClientSecrets=
                //    {
                //        new Secret("secret".Sha256())
                //    },
                //    AllowedScopes={"api1"}
                //},
                new Client
                {
                    ClientId="ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {  new Secret("secret".Sha256()) },
                    AllowedScopes={"api1"}
                    
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser> {
                new TestUser{ SubjectId="1", Username="alice", Password="password1" } ,
                new TestUser{ SubjectId="2", Username="bob", Password="password2"}
            };
        }
    }
}
