using Core.BC.Domain.Entities;
using Core.Shared;
using Core.Shared.Messages;
using System;
using System.Collections.Generic;

namespace Core.BC.Domain.Interfaces
{
    public interface IProdutoServices
    {

        ResponseMessage<ProdutoModel> Cadastrar(ProdutoModel produtoModel);

        ResponseMessage<ProdutoModel> Atualizar(ProdutoModel produtoModel);
        
        ResponseMessage<ProdutoModel> Inativar(ProdutoModel produtoModel);
        
        ResponseMessage<ProdutoModel> Ativar(ProdutoModel produtoModel);

        ResponseMessage<IList<ProdutoModel>> ObterProdutos(Guid? id);



    }
}
