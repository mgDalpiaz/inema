using Infra.Repository.SqlServer.Core.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra.Repository.SqlServer.Core
{
    public class Context : AbstractContext
    {
        public Context() : base()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
