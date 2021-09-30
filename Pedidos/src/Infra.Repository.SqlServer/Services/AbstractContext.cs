using CC.Warmup;
using Infra.Repository.SqlServer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infra.Repository.SqlServer
{
    public abstract class AbstractContext : DbContext, IContext
    {

        public AbstractContext() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.AppConfiguration.GetConnectionString("DefaultConnection")
                    , opt => opt.UseRowNumberForPaging());
        }
    }
}
