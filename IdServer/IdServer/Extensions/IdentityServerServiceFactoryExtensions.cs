using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using IdServer;
using IdServer.Services;

namespace IdentityServer3.Core.Configuration
{
    public static class IdentityServerServiceFactoryExtensions
    {
        public static IdentityServerServiceFactory Configure(this IdentityServerServiceFactory factory, string connectionString)
        {
            var serviceOptions = new EntityFrameworkServiceOptions { ConnectionString = connectionString };
            factory.RegisterConfigurationServices(serviceOptions);
            factory.RegisterOperationalServices(serviceOptions);

            factory.Register(new Registration<Context>(resolver => new Context(connectionString)));
            factory.Register(new Registration<UserStore>());
            factory.Register(new Registration<UserManager>());
            factory.UserService = new Registration<IUserService, IdentityUserService>();

            return factory;
        }
    }
}