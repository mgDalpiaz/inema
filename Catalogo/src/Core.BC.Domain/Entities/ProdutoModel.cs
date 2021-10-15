using Core.Shared.Base;
using System;
using System.Collections.Generic;

namespace Core.BC.Domain.Entities
{
    public class ProdutoModel : BaseEntity
    {
        #region [ Propriedades ]
               
        public string NomeProduto { get; set; }

        public bool Perecivel { get; set; }

        public DateTime DataValidade { get; set; }

        public string NumeroDeSerie { get; set; }
        
        public string CodigoDeBarras { get; set; }

        public string CodigoLote { get; set; }

        public virtual IEnumerable<ProdutoVariacoesEntidade> Variacoes { get; set; }

        #endregion



    }
}
