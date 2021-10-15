using Core.BC.Domain.Entities;
using Core.BC.Domain.Interfaces;
using Core.Shared.Messages;
using Front.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace Front.API.Controllers
{
    [ApiExplorerSettings(GroupName = @"Produtos")]
    public class ProdutoController : BaseControllerAbstract
    {
        #region [ Attr ]

        private readonly IProdutoServices _produtoServices;

        #endregion
        #region [ Ctor ]

        public ProdutoController(
            IProdutoServices service,
            INotificationHandler<Notification> notification)
            : base(notification)
        {
            _produtoServices = service;
        }

        #endregion

        #region [ CRUD Methods ]

        [HttpPost]
        [SwaggerOperation(
        Summary = "Retornar a Situação de Ativo",
        Description = "Com base na chave (id) passo traz o recurso inativo para a situação de ativo novamente.")]
        [SwaggerResponse(404, "Recurso não encontrado para realizar a ação de retornar para ativo", null)]
        [SwaggerResponse(200, "Ação realizada com sucesso.", typeof(ProdutoModel))]
        public virtual async Task<IActionResult> Cadastrar([FromBody] ProdutoModel model)
        {
            var response = _produtoServices.Cadastrar(model);

            return Response(response);

        }

        [HttpPut]
        [SwaggerOperation(
Summary = "Retornar a Situação de Ativo",
Description = "Com base na chave (id) passo traz o recurso inativo para a situação de ativo novamente.")]
        [SwaggerResponse(404, "Recurso não encontrado para realizar a ação de retornar para ativo", null)]
        [SwaggerResponse(200, "Ação realizada com sucesso.", typeof(ProdutoModel))]
        public virtual async Task<IActionResult> Atualizar([FromBody] ProdutoModel model)
        {
            var response = _produtoServices.Atualizar(model);

            return Response(response);

        }

        #endregion
    }
}
