using MediatR;
using System.Threading.Tasks;

namespace Core.Shared
{
    public interface IDomainEventBus
    {
        Task Raise<T>(T args) where T : INotification;
    }
}
