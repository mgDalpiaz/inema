
using AutoMapper;
using Core.Shared;
using Core.Shared.Base;
using Core.Shared.Messages;
using Infra.Repository.Shared.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace App.Shared.Base
{
    public abstract class GenericServiceAbstract<T> : BaseServiceAbstract, IAppService<T>
        where T : BaseEntity
    {
        #region [ Attr ]

        protected readonly IRepository<T> repository;
        protected readonly ILogger<T> logger;

        #endregion

        #region [ Ctor ]

        public GenericServiceAbstract(
            IMapper mapper,
            IUnitOfWork uow,
            IDomainEventBus bus,
            INotificationHandler<Notification> notifications,
            INotificationHandler<CrossMessage> messages,
            ILogger<T> logger,
            IRepository<T> repository
            )
            : base(mapper, uow, notifications, messages, bus)
        {
            this.logger = logger;
            this.repository = repository;
        }

        #endregion

        #region [ CRUD Methods ]

        public virtual async Task<ResponseMessage<T>> Create(ICreateCommand cmd)
        {
            #region [ CODE ]

            T data = mapper.Map<T>(cmd);
            data = this.SpecializedCreation(data);

            if (!(await AssertToCreate(data)) || data == null)
                return ResponseMessage<T>.Invalid();

            data.ToCreate(user.UserName, user.UserId);
            
            repository.Save(data);

            if (await Commit())
            {
                await this.CreatedSuccessed();
                return new ResponseMessage<T>(data);
            }
            else
            {
                await this.CreatedFailed();
                return ResponseMessage<T>.Invalid();
            }

            #endregion
        }

        public virtual async Task<ResponseMessage<T>> Update(IUpdateCommand cmd)
        {
            #region [ CODE ]

            T current = repository.Get(cmd.Id);

            if (current == null)
            {
                await bus.Raise(Notification.CreateError(typeof(T).Name, "Não foi localizado dados para alteração com o Id informado."));
                return ResponseMessage<T>.Invalid();
            }

            T data = mapper.Map<T>(cmd);

            var dst = current;

            data = this.SpecializedUpdate(data, dst);

            if (!(await AssertToUpdate(data, current)) || data == null)
            {
                return ResponseMessage<T>.Invalid();
            }
          
            dst.PopulateDiffBySource(data);

            // Se n teve alterar faz um Fake Success
            if (!dst.IsUpdate)
                return new ResponseMessage<T>(dst);

            dst.ToUpdated(user.UserName, user.UserId);

            repository.Save(dst);

            if (await Commit())
            {
                await this.UpdatedSuccessed();
                return new ResponseMessage<T>(dst);
            }
            else
            {
                await this.UpdatedFailed();
                return ResponseMessage<T>.Invalid();
            }

            #endregion
        }

        public virtual async Task<ResponseMessage<T>> Remove(IRemoveCommand cmd)
        {
            #region [ CODE ]

            var current = repository.Get(cmd.Id);

            if (current == null)
            {
                await bus.Raise(Notification.CreateError("Id", "Nenhum registro encontrado."));
                return ResponseMessage<T>.Invalid();
            }

            current = this.SpecializedDelete(current);

            if (!(await AssertToRemove(current)) || current == null)
                return ResponseMessage<T>.Invalid();

            current.ToInactivate(user.UserName, user.UserId);

            repository.Save(current);

            if (await Commit())
            {
                await this.RemovedSuccessed();
                return new ResponseMessage<T>();
            }
            else
            {
                await this.RemovedFailed();
                return ResponseMessage<T>.Invalid();
            }

            #endregion
        }

        public virtual async Task<ResponseMessage<T>> ReturnToActive(IReturnCommand cmd)
        {
            #region [ CODE ]

            var current = repository.Get(cmd.Id);

            if (current == null)
            {
                await bus.Raise(Notification.CreateError("Id", "Nenhum registro encontrado."));
                return ResponseMessage<T>.Invalid();
            }

            current = this.SpecializedActivation(current);

            if (!(await AssertToActived(current)) || current == null)
                return ResponseMessage<T>.Invalid();

            current.ToActivate(user.UserName, user.UserId);

            repository.Save(current);

            if (await Commit())
            {
                await this.ActivedSuccessed();
                return new ResponseMessage<T>(current);
            }
            else
            {
                await this.ActivedFailed();
                return ResponseMessage<T>.Invalid();
            }

            #endregion
        }

        public virtual Task<ResponseMessage<T>> ListById(IListByIdCommand cmd)
        {
                return Task.FromResult(new ResponseMessage<T>(repository.Get().FirstOrDefault(x => x.Id == cmd.Id && x.IsActive)));
        }

        public virtual Task<ResponsePaginated<List<T>>> List(IListCommand cmd)
        {
            #region [ CODE ]

            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(typeof(T)).Find(cmd.ColumnOrder, true);

            if (prop == null)
                prop = TypeDescriptor.GetProperties(typeof(T)).Find("InsertedAt", true);

            if (string.IsNullOrWhiteSpace(cmd.Order) || (cmd.Order != "desc" && cmd.Order != "asc"))
                cmd.Order = "desc";

            var query = repository.Get().Where(x => x.IsActive);


            if (cmd.Order.Equals("asc"))
            {
                query = query.OrderBy(x => prop.GetValue(x));
            }
            else if (cmd.Order.Equals("desc"))
            {
                query = query.OrderByDescending(x => prop.GetValue(x));
            }

            List<T> data = query.Take(cmd.PageSize * cmd.Page)
                .Skip(cmd.Page == 1 ? 0 : (cmd.PageSize * (cmd.Page - 1)))
                .ToList();

            int totalRows = query != null ? query.Count() : 0;
            var paginated = new ResponsePaginated<List<T>>(data ?? new List<T>(), cmd.Page, cmd.PageSize, totalRows);

            return Task.FromResult(paginated);

            #endregion
        }

        public virtual Task<int> CountListRow()
        {
                return Task.FromResult(repository.Get().Where(x => x.IsActive).Count());
        }

        #endregion

        #region [ Extended Protected CRUD Methods ]

        protected virtual T SpecializedCreation(T data)
        {
            return data;
        }

        protected virtual T SpecializedUpdate(T data, T current)
        {
            return data;
        }

        protected virtual T SpecializedDelete(T data)
        {
            return data;
        }

        protected virtual T SpecializedActivation(T data)
        {
            return data;
        }

        #endregion

        #region [ Virtual Check Methods ]

        public virtual async Task<bool> AssertToCreate(T data) {
            return true;
        }

        public virtual async Task<bool> AssertToUpdate(T data, T current)
        {
            return true;
        }

        public virtual async Task<bool> AssertToRemove(T data)
        {
            return true;
        }

        public virtual async Task<bool> AssertToActived(T data)
        {
            return true;
        }

        #endregion

        #region [ Virtual Confirm Event ]

        public virtual async Task CreatedSuccessed()
        {
        }

        public virtual async Task UpdatedSuccessed()
        {
        }

        public virtual async Task RemovedSuccessed()
        {
        }

        public virtual async Task ActivedSuccessed()
        {
        }

        public virtual async Task CreatedFailed()
        {
        }

        public virtual async Task UpdatedFailed()
        {
        }

        public virtual async Task RemovedFailed()
        {
        }

        public virtual async Task ActivedFailed()
        {
        }

        public virtual async Task<ResponsePaginated<List<T>>> ListByUnitId(IListCommand cmd)
        {
            throw new System.NotImplementedException();
        }

        public virtual async Task<ResponsePaginated<List<T>>> ListByContractTypeCLT(IListCommand cmd)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
