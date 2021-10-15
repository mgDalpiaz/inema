using Core.BC.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Repository.SqlServer.Core.Mappings
{
    public class ProdutoVariacoesMapping : AbstractMapping<ProdutoVariacoesEntidade>
    {

        public override void Configure(EntityTypeBuilder<ProdutoVariacoesEntidade> builder)
        {

            builder.HasOne(p => p.Produto);

            base.Configure(builder);
        }
    }
}
