using Microsoft.EntityFrameworkCore;
using SohaLogin.Accounts;
using SohaLogin.Database;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SohaLoginAddServices
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAccountService, AccountService>();
        }
        public static void AddDatabase(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<SohaLoginDbContext>(options => options.UseInMemoryDatabase("sohaLoginDatabase"));
            serviceCollection.AddScoped<ISohaLoginDatabase, SohaLoginDatabase>();
        }
    }
}
