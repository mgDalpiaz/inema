using App.Shared;
using AutoMapper;
using Core.BC.Domain.Interfaces;
using Core.Shared;
using Core.Shared.Messages;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Services.Services
{
    public class PedidoServices : BaseServiceAbstract, IPedidoServices
    {
        #region [ Ctor ]

        public PedidoServices(IMapper mapper, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, notifications, messages, bus)
        {
        }

        public PedidoServices(IMapper mapper, IUnitOfWork uow, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus) : base(mapper, uow, notifications, messages, bus)
        {
        }

        #endregion
    }
}
