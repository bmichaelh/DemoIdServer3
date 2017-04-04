using IdentityServer3.Core.Configuration;
using IdentityManager.Configuration;
using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

[assembly: OwinStartupAttribute(typeof(IdServer.Startup))]
namespace IdServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ConfigureAuth(app);

            

            app.Map("/identity", id => {
                var factory = new IdentityServerServiceFactory()
                    .Configure(connectionString)
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());

                id.UseIdentityServer(new IdentityServerOptions
                {
                    SiteName = "Demo Identity Server",
                    IssuerUri = (string)ConfigurationManager.AppSettings["options.issuerUri"],
                    SigningCertificate = LoadCertificate(),

                    Factory = factory,

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        EnablePostSignOutAutoRedirect = true
                    }
                });

            });

            app.Map("/admin", adminApp => {
                adminApp.UseIdentityManager(new IdentityManagerOptions()
                {
                    Factory = new IdentityManagerServiceFactory().Configure(connectionString)
                });
            });
        }
        X509Certificate2 LoadCertificate()
        {
            string certName = string.Format(@"{0}\bin\{1}", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["signing-certificate.name"]);
            string certPW = (string)ConfigurationManager.AppSettings["signing-certificate.password"];

            return new X509Certificate2(certName, certPW);
        }
    }
}
