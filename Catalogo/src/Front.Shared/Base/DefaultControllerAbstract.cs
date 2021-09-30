using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Shared;
using Core.Shared.Base;
using Core.Shared.Messages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Front.Shared
{
    public abstract class DefaultControllerAbstract<T> : BaseControllerAbstract where T : BaseEntity
    {
        #region [ Attr ]

        protected IAppService<T> service;
        protected readonly ILogger<T> logger;


        #endregion

        #region [ Ctor ]

        public DefaultControllerAbstract(IAppService<T> service, ILogger<T> logger, INotificationHandler<Notification> notification) : base(notification)
        {
            this.service = service;
            this.logger = logger;
        }

        #endregion

        #region [ Default Action Methods ]

        [HttpDelete]
        [Route("{id?}")]
        [SwaggerOperation(
        Summary = "Inativar",
        Description = "Com base no id passado inativa um recurso caso o mesmo exista e esteja ativo.")]
        [SwaggerResponse(404, "Recurso não encontrado para realizar a ação de inativar", null)]
        public virtual async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await service.Remove(new DefaultRemoveCommand<T> { Id = id }).ConfigureAwait(false);

            return Response(response.IsValid ? response.Data : null);

        }

        [HttpPut]
        [Route("{id?}")]
        [SwaggerOperation(
        Summary = "Retornar a Situação de Ativo",
        Description = "Com base na chave (id) passo traz o recurso inativo para a situação de ativo novamente.")]
        [SwaggerResponse(404, "Recurso não encontrado para realizar a ação de retornar para ativo", null)]
        [SwaggerResponse(200, "Ação realizada com sucesso.", typeof(BaseEntity))]
        public virtual async Task<IActionResult> Active([FromRoute] Guid id)
        {
            var response = await service.ReturnToActive(new DefaultReturnCommand<T> { Id = id }).ConfigureAwait(false);

            return Response();

        }

        [HttpGet]
        [Route("{id?}")]
        [SwaggerOperation(
        Summary = "Pesquisar pela Chave (id)",
        Description = "Com base na chave realiza a pesquisa na base de dados do recurso.")]
        [SwaggerResponse(404, "Id passado não possui nenhum correpondência na base de dados.", null)]
        [SwaggerResponse(200, "Ação realizada com sucesso.", typeof(BaseEntity))]
        public virtual async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = await service.ListById(new DefaultListByIdCommand<T> { Id = id }).ConfigureAwait(false); ;

            return Response(response.IsValid ? response.Data : null);
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Pesquisar todos cadastros existentes",
        Description = "Pesquisa de forma paginada todos os recursos cadasrtrados deste objeto na base de dados.",
        OperationId = "")]
        [SwaggerResponse(404, "Não há nenhum objeto cadastrado para este recurso.", null)]
        [SwaggerResponse(200, "Ação realizada com sucesso.", typeof(ResponsePaginated<List<BaseEntity>>))]
        public virtual async Task<IActionResult> GetAll(
           [FromHeader] int pageSize = 10,
           [FromHeader] int page = 1,
           [FromHeader] string order = "desc",
           [FromHeader] string columnOrder = "InsertedAt"
           )
        {
            var response = await service.List(new DefaultListCommand<T>
            {
                PageSize = pageSize,
                Page = page,
                Order = order,
                ColumnOrder = columnOrder
            }).ConfigureAwait(false);

            return Response(response.IsValid && response.Data.Count > 0 ? response : null);
        }

        #endregion

    }
}
