using Microsoft.AspNetCore.SignalR;

namespace DDB.DVDCentral.API.Hubs
{
    public class BingoHub : Hub
    {

        // Can have as many parameters as you need.
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("RecieveMessage",user, message);
        }
    }
}
