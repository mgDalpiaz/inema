using App.Shared;
using AutoMapper;
using Core.BC.Domain.Entities;
using Core.BC.Domain.Enums;
using Core.BC.Domain.Interfaces;
using Core.Shared;
using Core.Shared.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Services
{
    public class CatalogoServices : BaseServiceAbstract, ICatalogoServices
    {
        #region [ Ctor ]

        public CatalogoServices(IMapper mapper, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, notifications, messages, bus)
        {
        }

        public CatalogoServices(IMapper mapper, IUnitOfWork uow, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, uow, notifications, messages, bus)
        {
        }

        #endregion

        #region [ Methods ]

        public CatalogoEntidade Ativar(CatalogoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public CatalogoEntidade Atualizar(CatalogoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public CatalogoEntidade Cadastrar(CatalogoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public IList<ListaPrecoEntidade> FiltrarProdutos(string nome, TipoOrdenacao tipoOrdenacao)
        {
            throw new NotImplementedException();
        }

        public CatalogoEntidade Inativar(CatalogoEntidade produtoModel)
        {
            throw new NotImplementedException();
        }

        public IList<CatalogoEntidade> ObterCatalogos(Guid? id)
        {
            throw new NotImplementedException();
        }

        public IList<ListaPrecoEntidade> ProdutosOrdernados(TipoOrdenacao tipoOrdenacao)
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
