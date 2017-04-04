using IdentityServer3.Core;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Web.Helpers;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityModel.Client;
using System.Threading.Tasks;
using System.Net.Http;

[assembly: OwinStartupAttribute(typeof(ClientMVC.Startup))]

namespace ClientMVC
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                Authority = "https://localhost:44372/identity",
                ClientId = "mvc",
                Scope = "openid profile roles sampleApi",
                RedirectUri = "https://localhost:44336/",
                ResponseType = "id_token token",

                SignInAsAuthenticationType = "Cookies",
                UseTokenLifetime = false,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = async n =>
                    {
                        var nid = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            Constants.ClaimTypes.GivenName,
                            Constants.ClaimTypes.Role);

                        // get userinfo data
                        var userInfoClient = new UserInfoClient(new Uri(n.Options.Authority + "/connect/userinfo").ToString());

                        var userInfo = await userInfoClient.GetAsync(n.ProtocolMessage.AccessToken);
                        userInfo.Claims.ToList().ForEach(ui => nid.AddClaim(new Claim(ui.Type, ui.Value)));

                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));

                        nid.AddClaim(new Claim("access_token", n.ProtocolMessage.AccessToken));

                        nid.AddClaim(new Claim("expires_at", DateTimeOffset.Now.AddSeconds(int.Parse(n.ProtocolMessage.ExpiresIn)).ToString()));

                        nid.AddClaim(new Claim("app_specific", "some data"));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);
                    },

                    RedirectToIdentityProvider = n =>
                    {
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }
                        }
                        return Task.FromResult(0);
                    }
                }
            });

            app.UseResourceAuthorization(new AuthorizationManager());
        }
    }
}