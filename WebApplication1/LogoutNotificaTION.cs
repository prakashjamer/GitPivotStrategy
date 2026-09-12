using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace WebApplication1
{
    public class LogoutNotificaTION
    {
    }
    public interface IMessageHubClient
    {
        Task SendOffersToUser(string message);
    }
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task SendOffersToUser(string message)
        {
            await Clients.All.SendOffersToUser(message);
        }
    }
}
