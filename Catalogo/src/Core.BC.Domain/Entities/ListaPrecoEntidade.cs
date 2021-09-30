using Extension.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.BC.Domain.Entities
{
    public class ListaPrecoEntidade
    {

        #region [ Properties ]

        public DateTime Inicio { get; set; }

        public DateTime Termino { get; set; }

        public IList<ProdutoListaPrecoModel> ProdutoValor { get; set; }

        public decimal DescontoPercentual { get; set; }

        #endregion

        #region [ Properties Calculate ]

        public bool Vigente => Inicio >= DateTime.Now && Termino >= DateTime.Now;

        #endregion

        public ListaPrecoEntidade()
        {
            ProdutoValor = new List<ProdutoListaPrecoModel>();
        }

        #region [ Methods ]

        public void CadastrarValorProduto(ProdutoModel produto, decimal valor)
        {
            ProdutoValor.Add(new ProdutoListaPrecoModel { Produto = produto, Valor = valor });
        }

        public void AtualizarValorProduto(Guid id, decimal valor)
        {
            ProdutoValor.FirstOrDefault(x => x.Produto.Id.Value == id).Valor = valor;
        }

        #endregion

    }
}
