using Core.Shared.Base;

namespace Core.BC.Domain.Entities
{
    public class  ProdutoListaPrecoModel : BaseEntity
    {

        #region [ Properties ]

        public virtual ProdutoModel Produto { get; set; }

        public decimal Valor { get; set; }

        #endregion

    }
}
