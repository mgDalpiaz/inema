using Core.BC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.SqlServer.Core.Mappings
{
    public class CatalogoMapping : AbstractMapping<CatalogoEntidade>
    {

        public override void Configure(EntityTypeBuilder<CatalogoEntidade> builder)
        {

            builder.HasMany(p => p.Produtos);

            builder.HasMany(p => p.ListaPreco);

            base.Configure(builder);
        }
    }
}
