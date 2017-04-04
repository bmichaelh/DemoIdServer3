using System.Collections.Generic;
using IdentityServer3.Core.Models;
using System.Configuration;
using System.Security.Claims;

namespace IdServer
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    //AccessTokenType = AccessTokenType.Jwt,
                    Enabled = true,
                    ClientName = "Frontier Logistics Customer Extranet",
                    ClientId = "mvc",
                    Flow = Flows.Implicit,
                    //ClientSecrets = new List<Secret>
                    //{
                    //    new Secret("secret".Sha256())
                    //},
                    RedirectUris = new List<string>
                    {
                        (string)ConfigurationManager.AppSettings["options.redirectUri"]
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        (string)ConfigurationManager.AppSettings["options.redirectUri"]
                    },
                    Claims = new List<Claim>
                    {
                        new Claim("customerRole", "somethingnew")
                    },
                    //AllowedScopes = new List<string>
                    //{
                    //    "openId",
                    //    "profile",
                    //    "roles"
                    //},
                    AllowAccessToAllScopes = true
                },
                new Client
                {
                    ClientName = "MVC Client (service communication)",
                    ClientId = "mvc_service",
                    Flow = Flows.ClientCredentials,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = new List<string>
                    {
                        "sampleApi"
                    }
                }
            };
        }
    }
}