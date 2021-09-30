using System;
using System.Collections.Generic;
using App.Shared;
using Core.Shared.Messages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Front.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "SafeIpList")]
    [SwaggerResponse(404, "Nenhuma correspondência encontrada", null)]
    [SwaggerResponse(400, "Erro de validação.", typeof(ResponseMessage<List<Notification>>))]
    [SwaggerResponse(401, "Não autenticado.", null)]
    [SwaggerResponse(403, "Não autorizado a executar a ação.", null)]
    [SwaggerResponse(500, "Erro interno da aplicação..", typeof(ResponseMessage<List<Notification>>))]

    public class BaseControllerAbstract : ControllerBase, IDisposable
    {

        #region [ Attr ]

        protected bool disposed = false;
        private readonly NotificationHandler _notifications;

        #endregion

        #region [ Ctor ]

        public BaseControllerAbstract(INotificationHandler<Notification> notification)
        {
            this._notifications = (NotificationHandler)notification;
        }

        #endregion
        
        #region [ Response Methods ]

        protected new IActionResult Response(object message = null, string actionType = "", string id = null)
        {

            if (_notifications.Unauthorized)
                return Unauthorized();

            if (_notifications.Wainting)
                return Accepted();

            if (!_notifications.IsValid)
            {
                _notifications.Notifications.ForEach(x =>
                {
                    ModelState.AddModelError(x.Key, x.Message);
                });

                var result = new ValidationProblemDetails(ModelState);
                result.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";

                return BadRequest(result);
            }
            else
            {
                switch (Request.HttpContext.Request.Method.ToUpper())
                {
                    case "POST":
                    case "PUT":
                    case "DELETE":
                        if (message == null)
                            return NoContent();
                        else
                            return CreatedAtAction(actionType, new { id = id }, message);
                    case "GET":
                        if (message == null)
                            return NotFound();
                        else
                            return Ok(message);
                    default:
                        return NotFound();
                }
            }
        }

        #endregion

        #region [ Class Destroy ]

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            }

            disposed = true;
        }

        #endregion
    }
}
