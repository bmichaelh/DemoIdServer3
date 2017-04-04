using System;
using System.Threading.Tasks;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(WebAPI.Startup))]

namespace WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44372/identity",
                RequiredScopes = new[] { "sampleApi" }
            });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
        }
    }
}
