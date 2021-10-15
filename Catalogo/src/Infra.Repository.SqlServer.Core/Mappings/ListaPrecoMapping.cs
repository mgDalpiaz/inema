using Core.BC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.SqlServer.Core.Mappings
{
    public class ListaPrecoMapping : AbstractMapping<ListaPrecoEntidade>
    {

        public override void Configure(EntityTypeBuilder<ListaPrecoEntidade> builder)
        {

            builder.HasMany(p => p.ProdutoValor);

            base.Configure(builder);
        }
    }
}
