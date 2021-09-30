using Core.BC.Domain.Entities;
using Core.Shared;
using System;
using System.Collections.Generic;

namespace Core.BC.Domain.Interfaces
{
    public interface IListaPrecoServices
    {

        ListaPrecoEntidade Cadastrar(ListaPrecoEntidade produtoModel);

        ListaPrecoEntidade Atualizar(ListaPrecoEntidade produtoModel);

        ListaPrecoEntidade Inativar(ListaPrecoEntidade produtoModel);

        ListaPrecoEntidade Ativar(ListaPrecoEntidade produtoModel);

        IList<ListaPrecoEntidade> ObterListaPrecos(Guid? id);

        ListaPrecoEntidade VincularProdutoValor(Guid id, Guid produtoId, decimal valor);
        
    }
}
