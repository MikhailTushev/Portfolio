using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Portfolio.Common;

namespace Portfolio.Web.Hubs
{
    [Authorize(Policy = "CustomHubAuthorizationPolicy")]
    public class NotifyHub : Hub
    {
        private readonly INotificator _notificator;
        private readonly ILogger<NotifyHub> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotifyHub(
            ILogger<NotifyHub> logger,
            IHttpContextAccessor httpContextAccessor,
            INotificator notificator)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _notificator = notificator;
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogDebug("OnConnectedAsync ConnectionId={ConnectionId}", Context.ConnectionId);
            var user = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IUserService>();
            return _notificator.AddUserAsync(user.CurrentUserDto.Id, Context.ConnectionId);
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            _logger.LogDebug("OnDisconnectedAsync ConnectionId={ConnectionId}", Context.ConnectionId);
            return _notificator.RemoveUserAsync(Context.ConnectionId);
        }
    }
}