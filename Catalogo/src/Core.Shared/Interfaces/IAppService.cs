using Core.Shared.Base;
using Core.Shared.Messages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Shared
{
    public interface IAppService<T> where T : BaseEntity
    {
        Task<ResponseMessage<T>> Create(ICreateCommand cmd);

        Task<ResponseMessage<T>> Update(IUpdateCommand cmd);

        Task<ResponseMessage<T>> Remove(IRemoveCommand cmd);

        Task<ResponseMessage<T>> ReturnToActive(IReturnCommand cmd);

        Task<ResponseMessage<T>> ListById(IListByIdCommand cmd);

        Task<ResponsePaginated<List<T>>> List(IListCommand cmd);

        Task<ResponsePaginated<List<T>>> ListByUnitId(IListCommand cmd);

        Task<ResponsePaginated<List<T>>> ListByContractTypeCLT(IListCommand cmd);
    }
}
