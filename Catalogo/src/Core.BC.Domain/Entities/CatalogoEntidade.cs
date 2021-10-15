using Core.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.BC.Domain.Entities
{
    public class CatalogoEntidade : BaseEntity
    {
        #region [ Properties ]

        public string NomeCatalogo { get; set; }

        public bool Ativo { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Termino { get; set; }

        public virtual IEnumerable<ListaPrecoEntidade> ListaPreco { get; set; }

        public virtual IEnumerable<ProdutoModel> Produtos { get; set; }

        #endregion

        #region [ Methods ]

        public IEnumerable<ListaPrecoEntidade> ProcurarProduto(string nome)
        {
            var produtos = this.Produtos.Where(x => 
                                    x.NomeProduto.Contains(nome, StringComparison.InvariantCultureIgnoreCase) || 
                                    x.Variacoes.Any(y => y.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase))
                                );
            if (!produtos.Any())
                return new List<ListaPrecoEntidade>();
            else
            {                
                return ListaPreco.Where(x => x.Inicio <= DateTime.Now && x.Termino >= DateTime.Now && produtos.Any(y => x.ProdutoValor.Any(z => z.Produto.Id == y.Id)));
            }
                


        }

        #endregion


    }
}
