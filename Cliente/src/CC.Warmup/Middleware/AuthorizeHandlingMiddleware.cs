using Core.Shared;
using Core.Shared.Entities.Security;
using Core.Shared.Messages;
using Infra.External.API;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CC.Warmup.Filters
{
    public class AuthorizeHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public static ConcurrentDictionary<string, AuthenticatedUser> Tokens { get; set; } = new ConcurrentDictionary<string, AuthenticatedUser>();

        public AuthorizeHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, IExternalAPI externalAPI, IDomainEventBus bus)
        {
            externalAPI.SetAPI("Auth");

            if (await VerifyAndRenewAuthenticate(context, externalAPI, bus))
            {
                await RegisterClientInfo(context, bus);
                await _next(context);
            }
        }

        #region [ Private Methods ]

        private async Task<bool> VerifyAndRenewAuthenticate(HttpContext context, IExternalAPI externalAPI, IDomainEventBus bus)
        {
            string token = context.Request?.Headers["Authorization"].FirstOrDefault() ?? null;
            var connectionRemoteIpAddress = context.Connection.RemoteIpAddress.MapToIPv4();
            var ips = new string[] { "127.0.0.1", "0.0.0.1", "::1" };

            if ((Configuration.CurrentEnvironment == Common.AppEnvironment.Development ||
                 ips.Contains(connectionRemoteIpAddress.ToString())) || string.IsNullOrEmpty(token))
            {
                await bus.Raise(CrossMessage.CreateCrossMessage(new AuthenticatedUser
                {
                    UserName = "Anonymous",
                    UserId = Guid.Empty
                }));

                return true;
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                await bus.Raise(CrossMessage.CreateCrossMessage(new AuthenticatedUser
                {
                    UserName = "Anonymous",
                    UserId = Guid.Empty
                }));

                return true;

            }

            try
            {
                if (AuthorizeHandlingMiddleware.Tokens.Any(x => x.Key == token && x.Value != null && x.Value.ExpirationAt >= DateTime.Now && (DateTime.Now - (x.Value?.LastCheck ?? DateTime.Now.AddMinutes(-1))).TotalSeconds < 300))
                {
                    await bus.Raise(CrossMessage.CreateCrossMessage(AuthorizeHandlingMiddleware.Tokens.FirstOrDefault(x => x.Key == token).Value));
                    return true;
                }

                AuthorizeHandlingMiddleware.Tokens.TryRemove(token, out AuthenticatedUser user);
            }
            catch (Exception ex)
            {

            }

            externalAPI.AddAuthorization(token);
            AuthenticatedUser checkAuth = null;
            try
            {
                checkAuth = await externalAPI.Request<AuthenticatedUser>("VerifyToken");
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return false;
            }

            if (checkAuth != null && !checkAuth.IsExpired)
            {
                checkAuth.LastCheck = DateTime.Now;

                try
                {
                    AuthorizeHandlingMiddleware.Tokens.TryAdd(token, checkAuth);
                    await bus.Raise(CrossMessage.CreateCrossMessage(checkAuth));

                }
                catch (Exception ex)
                {
                }

                return true;
            }
            else
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return false;
            }
        }

        /// <summary>
        /// Registra as informações do Cliente e da Requisição
        /// </summary>
        /// <param name="context"></param>
        /// <param name="bus"></param>
        /// <returns></returns>
        private Task RegisterClientInfo(HttpContext context, IDomainEventBus bus)
        {
            bus.Raise(CrossMessage.CreateCrossMessage(new HttpClientInfo
            {
                Agent = context.Request.Headers["User-Agent"].FirstOrDefault(),
                ContentType = context.Request.ContentType,
                Ip = context.Connection.RemoteIpAddress.ToString(),
                ServerIp = context.Connection.LocalIpAddress.ToString(),
                UrlRequested = $"{context.Request.Host}{context.Request.Path}",
                Method = context.Request.Method,
                Token = context.Request.Headers["Authorization"].FirstOrDefault()
            })).ConfigureAwait(false);

            return Task.CompletedTask;
        }
        #endregion
    }
}
