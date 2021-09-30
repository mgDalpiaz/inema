using Core.Shared;
using Core.Shared.Messages;
using Extension.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CC.Warmup.Filters
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToTraceMessage(true));
                await HandleExceptionAsync(context, ex);
            }
        }

        #region [ Private Methods ]

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            var notification = Notification.CreateInternal(ex);
            var error = new ValidationProblemDetails(new Dictionary<string, string[]>() { { notification.Key, new String[] { notification.Message, notification.TechnicalMessage } } });
            error.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;

            var result = JsonConvert.SerializeObject(error, Newtonsoft.Json.Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            return context.Response.WriteAsync(result);
        }

        #endregion
    }
}
