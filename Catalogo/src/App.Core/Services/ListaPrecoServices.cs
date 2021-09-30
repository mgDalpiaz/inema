using App.Shared;
using AutoMapper;
using Core.BC.Domain.Entities;
using Core.BC.Domain.Interfaces;
using Core.Shared;
using Core.Shared.Messages;
using Infra.Repository.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Services.Services
{
    class ListaPrecoServices : BaseServiceAbstract, IListaPrecoServices
    {
        #region [ Attr ]

        private readonly IRepository<ProdutoModel> _repositoryProduto;
        private readonly IRepository<ListaPrecoEntidade> _repositoryListaPreco;

        #endregion

        #region [ Ctor ]

        public ListaPrecoServices(IRepository<ProdutoModel> repositoryProduto, IRepository<ListaPrecoEntidade> repositoryListaPreco, IMapper mapper, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, notifications, messages, bus)
        {
            _repositoryListaPreco = repositoryListaPreco;
            _repositoryProduto = repositoryProduto;
        }

        public ListaPrecoServices(IRepository<ProdutoModel> repositoryProduto, IRepository<ListaPrecoEntidade> repositoryListaPreco, IMapper mapper, IUnitOfWork uow, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, uow, notifications, messages, bus)
        {
            _repositoryListaPreco = repositoryListaPreco;
            _repositoryProduto = repositoryProduto;
        }

        #endregion

        #region [ Methods ]

        public ListaPrecoEntidade Ativar(ListaPrecoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public ListaPrecoEntidade Atualizar(ListaPrecoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public ListaPrecoEntidade Cadastrar(ListaPrecoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public ListaPrecoEntidade Inativar(ListaPrecoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public IList<ListaPrecoEntidade> ObterListaPrecos(Guid? id)
        {
            throw new NotImplementedException();
        }

        public ListaPrecoEntidade VincularProdutoValor(Guid id, Guid produtoId, decimal valor)
        {
            var lista = _repositoryListaPreco.Get(id);
            
            if(lista == null)
            {
                bus.Raise(Notification.CreateError("Lista de Preco", "Lista de Preco não encontrada para o Id passado"));
                return null;
            }

            if (lista.ProdutoValor.Any(x => x.Produto.Id == produtoId))
                lista.AtualizarValorProduto(produtoId, valor);
            else
            {
                var produto = _repositoryProduto.Get(produtoId);

                if (produto == null)
                {
                    bus.Raise(Notification.CreateError("Produto", "Produto não encontrado."));
                    return null;
                }

                lista.CadastrarValorProduto(produto, valor);
            }

            return lista;
        }


        #endregion
    }
}
