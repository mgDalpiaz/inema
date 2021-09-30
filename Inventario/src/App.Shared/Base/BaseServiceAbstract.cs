using AutoMapper;
using Core.Shared;
using Core.Shared.Entities.Security;
using Core.Shared.Messages;
using MediatR;
using System;
using System.Threading.Tasks;

namespace App.Shared
{
    public abstract class BaseServiceAbstract : IDisposable
    {

        #region [ Attr ]

        protected readonly IMapper mapper;
        protected readonly NotificationHandler notifications;
        protected readonly CrossMessageHandler messages;
        protected readonly AuthenticatedUser user;
        private readonly IUnitOfWork _uow;
        protected readonly IDomainEventBus bus;

        #endregion

        #region [ Ctor ]

        public BaseServiceAbstract(IMapper mapper, IUnitOfWork uow, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus)
        {
            this.mapper = mapper;
            this._uow = uow;
            this.bus = bus;
            this.notifications = (NotificationHandler)notifications;
            this.messages = (CrossMessageHandler)messages;
            this.user = this.messages.GetMessage<AuthenticatedUser>();

        }

        public BaseServiceAbstract(IMapper mapper, INotificationHandler<Notification> notifications, INotificationHandler<CrossMessage> messages, IDomainEventBus bus)
        {
            this.mapper = mapper;
            this.notifications = (NotificationHandler)notifications;
            this.messages = (CrossMessageHandler)messages;
            this.bus = bus;
            this.user = this.messages.GetMessage<AuthenticatedUser>();
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Controla a unit Of Work das operacoes
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Commit()
        {
            if (!notifications.IsValid) return false;

            if (await _uow.Commit())
                return true;
            else
                await _uow.Rollback();

            await bus.Raise(Notification.CreateError("Commit", "Erro ao finalizar operação com a base de dados.")).ConfigureAwait(false);

            return false;
        }

        public void Dispose()
        {
            this.messages.Dispose();
            this.notifications.Dispose();
        }

        #endregion

    }
}
