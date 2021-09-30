using Core.BC.Domain.Entities;
using Core.BC.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Core.BC.Domain.Interfaces
{
    public interface ICatalogoServices
    {
        CatalogoEntidade Cadastrar(CatalogoEntidade produtoModel);

        CatalogoEntidade Atualizar(CatalogoEntidade produtoModel);

        CatalogoEntidade Inativar(CatalogoEntidade produtoModel);

        CatalogoEntidade Ativar(CatalogoEntidade produtoModel);

        IList<CatalogoEntidade> ObterCatalogos(Guid? id);

        IList<ListaPrecoEntidade> ProdutosOrdernados(TipoOrdenacao tipoOrdenacao);

        IList<ListaPrecoEntidade> FiltrarProdutos(string nome, TipoOrdenacao tipoOrdenacao);

    }
}
