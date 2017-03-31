using System.Collections.Generic;
using IdentityServer3.Core.Models;
using System.Configuration;

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
                    //AllowedScopes = new List<string>
                    //{
                    //    "openId",
                    //    "profile"
                    //},
                    AllowAccessToAllScopes = true
                }
            };
        }
    }
}