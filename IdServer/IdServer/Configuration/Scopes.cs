using System.Collections.Generic;
using IdentityServer3.Core.Models;
using IdentityManager;

namespace IdServer
{
    public class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
            {
                new Scope
                {
                    Enabled = true,
                    Name = "roles",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role"),
                        new ScopeClaim("customerRole")
                    }
                },
                new Scope
                {
                    Enabled = true,
                    DisplayName = "Sample API",
                    Name = "sampleApi",
                    Description = "Access to a sample API",
                    Type = ScopeType.Resource,

                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role"),
                        new ScopeClaim("customerRole")
                    }
                }
            };

            scopes.AddRange(StandardScopes.All);

            return scopes;

            //return new Scope[]
            //{
            //    StandardScopes.OpenId,
            //    StandardScopes.OpenId,
            //    StandardScopes.Profile,
            //    StandardScopes.Email,
            //    StandardScopes.OfflineAccess,
            //    new Scope
            //    {
            //        Name = "read",
            //        DisplayName = "Read data",
            //        Type = ScopeType.Resource,
            //        Emphasize = false,
            //    },
            //    new Scope
            //    {
            //        Name = "write",
            //        DisplayName = "Write data",
            //        Type = ScopeType.Resource,
            //        Emphasize = true,
            //    },
            //    new Scope
            //    {
            //        Name = "forbidden",
            //        DisplayName = "Forbidden scope",
            //        Type = ScopeType.Resource,
            //        Emphasize = true
            //    },
            //    new Scope
            //    {
            //        Name = "identity_manager",
            //        DisplayName = "Identity Manager",
            //        Description = "Authorization for Identity Manager",
            //        Type = ScopeType.Identity,
            //        Claims = new List<ScopeClaim>
            //        {
            //            new ScopeClaim(Constants.ClaimTypes.Name),
            //            new ScopeClaim(Constants.ClaimTypes.Role)
            //        }
            //    },
            //    new Scope
            //    {
            //        Enabled = true,
            //        DisplayName = "Frontier WebAPI",
            //        Name = "ftlgWebAPI",
            //        Description = "Access to Web API",
            //        Type = ScopeType.Resource
            //    }
            // };
        }
    }
}