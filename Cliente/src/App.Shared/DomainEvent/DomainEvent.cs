using System.Threading.Tasks;
using Core.Shared;
using MediatR;

namespace App.Shared
{
    public sealed class DomainEvent : IDomainEventBus
    {
        private readonly IMediator mediator;

        public DomainEvent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Raise<T>(T args) where T : INotification
        {
            return mediator.Publish(args);
        }
    }
}
