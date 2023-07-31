using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace asp_MVC_letsTry.SignalR
{
    public class Chat : Hub
    {
        public Chat() { }
        public async Task Send(string message, string to)
        {
            if (Context.UserIdentifier is string userEmail)
            {
                string currentTime = DateTime.Now.ToString("HH:mm");
                await this.Clients.Users(to, userEmail).SendAsync("Receive", message,
                    new
                    {
                        name = Context.User?.Identity?.Name,
                        surname = Context.User?.FindFirst(ClaimTypes.Surname)?.Value,
                        time = currentTime
                    });
                //await this.Clients.All.SendAsync("Receive", message);
            }
        }
        public override Task OnConnectedAsync()
        {
            //var userID = Context.UserIdentifier;
            //var user = Context.User;
            return base.OnConnectedAsync();
        }
        public async Task SendInvitation(string to)
        {
            await Clients.User(to).SendAsync("Invitation", Context.User?.Identity?.Name + " " + Context.User?.FindFirst(ClaimTypes.Surname)?.Value, Context.UserIdentifier, to);
        }
        public async Task Answer(string to, bool decision)
        {
            await Clients.User(to).SendAsync("AnswerAfterInvitation", decision, to);
        }
    }
}
