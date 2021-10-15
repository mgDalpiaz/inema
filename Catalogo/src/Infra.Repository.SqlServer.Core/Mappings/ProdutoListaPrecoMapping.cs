using Core.BC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.SqlServer.Core.Mappings
{
    public class ProdutoListaPrecoModelMapping : AbstractMapping<ProdutoListaPrecoModel>
    {

        public override void Configure(EntityTypeBuilder<ProdutoListaPrecoModel> builder)
        {

            builder.HasOne(p => p.Produto);

            base.Configure(builder);
        }
    }
}
