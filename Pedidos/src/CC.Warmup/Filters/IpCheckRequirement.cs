using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Warmup.Filters
{
    public class IpCheckRequirement : IAuthorizationRequirement
    {
        public bool IpClaimRequired { get; set; } = true;
    }

    public class IpCheckHandler : AuthorizationHandler<IpCheckRequirement>
    {
        public IpCheckHandler(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private IHttpContextAccessor HttpContextAccessor { get; }
        private HttpContext HttpContext => HttpContextAccessor.HttpContext;


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IpCheckRequirement requirement)
        {
            string token = HttpContext.Request?.Headers["Authorization"].FirstOrDefault() ?? null;
            var connectionRemoteIpAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4();
            var ips = new string[] { "127.0.0.1", "0.0.0.1", "::1" };

            if (!requirement.IpClaimRequired || !string.IsNullOrWhiteSpace(token))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (ips.Contains(connectionRemoteIpAddress.ToString()))
                context.Succeed(requirement);
            else
                context.Fail();
            
            return Task.CompletedTask;
        }
    }
}
