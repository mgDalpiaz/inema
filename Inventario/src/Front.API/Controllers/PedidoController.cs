using Core.BC.Domain.Interfaces;
using Core.Shared;
using Core.Shared.Messages;
using Front.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Front.API.Controllers
{
    [ApiExplorerSettings(GroupName = @"Pedidos")]
    public class PedidoController : BaseControllerAbstract
    {
        #region [ Ctor ]

        public PedidoController(
            IPedidoServices service,
            INotificationHandler<Notification> notification)
            : base(notification)
        {

        }

        #endregion
    }
}
