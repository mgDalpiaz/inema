using Core.Shared.Base;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.BC.Domain.Entities
{
    public class ProdutoVariacoesEntidade : BaseEntity
    {
        #region [ Properties  ]

        public string Nome { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ProdutoModel Produto { get; set; }

        #endregion

    }
}
