using Core.BC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.SqlServer.Core.Mappings
{
    public class ProdutoMapping : AbstractMapping<ProdutoModel>
    {

        public override void Configure(EntityTypeBuilder<ProdutoModel> builder)
        {
            builder.Property(p => p.CodigoDeBarras)
                .IsRequired(false)
                .HasColumnType("varchar(30)");

            builder.Property(p => p.CodigoLote)
                .HasColumnType("varchar(30)");

            base.Configure(builder);
        }
    }
}
