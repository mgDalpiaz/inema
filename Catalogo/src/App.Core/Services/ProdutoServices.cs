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
    public class ProdutoServices : BaseServiceAbstract, IProdutoServices
    {
        #region [ Attr ]

        private readonly IRepository<ProdutoModel> _repository;

        #endregion

        #region [ Ctor ]

        public ProdutoServices(IRepository<ProdutoModel> repository, IMapper mapper, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, notifications, messages, bus)
        {
            _repository = repository;
        }

        public ProdutoServices(IRepository<ProdutoModel> repository, IMapper mapper, IUnitOfWork uow, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, uow, notifications, messages, bus)
        {
            _repository = repository;
        }

        #endregion

        #region [ Action Methods ]
        public ResponseMessage<ProdutoModel> Ativar(ProdutoModel produtoModel)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage<ProdutoModel> Atualizar(ProdutoModel produtoModel)
        {
            if (produtoModel == null)
            {
                bus.Raise(Notification.CreateError("Produto", "Contrato de Dados não informado"));
                return ResponseMessage<ProdutoModel>.Invalid();
            }

            produtoModel.ToUpdated(null, Guid.Empty);

            _repository.Save(produtoModel);

            if (Commit().Result)
                 return ResponseMessage<ProdutoModel>.Valid(produtoModel);
            else
                return ResponseMessage<ProdutoModel>.Invalid();
        }

        public ResponseMessage<ProdutoModel> Cadastrar(ProdutoModel produtoModel)
        {
            if(produtoModel == null)
            {
                bus.Raise(Notification.CreateError("Produto", "Contrato de Dados não informado"));
                return ResponseMessage<ProdutoModel>.Invalid();
            }

            produtoModel.ToCreate(null, Guid.Empty);

            _repository.Save(produtoModel);

            if (Commit().Result)
                return ResponseMessage<ProdutoModel>.Valid(produtoModel);
            else
                return ResponseMessage<ProdutoModel>.Invalid();
        }

        public ResponseMessage<ProdutoModel> Inativar(ProdutoModel produtoModel)
        {
            throw new NotImplementedException();
        }

        public ResponseMessage<IList<ProdutoModel>> ObterProdutos(Guid? id)
        {
            var response = new List<ProdutoModel>();
            if(id != null)
            {
                response.Add(_repository.Get(id.GetValueOrDefault()));

            } else
            {
                response = _repository.Get().ToList();
            }
        }

        #endregion

    }
}
