using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SohaLogin.Database
{
    public class SohaLoginDbContext : DbContext
    {
        public SohaLoginDbContext(DbContextOptions<SohaLoginDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(thisAssembly);
        }
    }
}
