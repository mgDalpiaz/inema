using Core.BC.Domain.Entities;
using Core.Shared;
using System;
using System.Collections.Generic;

namespace Core.BC.Domain.Interfaces
{
    public interface IProdutoServices
    {

        ProdutoModel Cadastrar(ProdutoModel produtoModel);
        
        ProdutoModel Atualizar(ProdutoModel produtoModel);
        
        ProdutoModel Inativar(ProdutoModel produtoModel);
        
        ProdutoModel Ativar(ProdutoModel produtoModel);

        IList<ProdutoModel> ObterProdutos(Guid? id);



    }
}
