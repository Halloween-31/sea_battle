using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace asp_MVC_letsTry.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}