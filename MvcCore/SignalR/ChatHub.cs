using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Console.SignalR
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            
        }

        //public void Echo(string message)
        //{
        //    //you're going to configure your client app to listen for this
        //    Clients.All.SendAsync("Send", message);
        //}

        //public async Task Send(string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", message);
        //    await Clients.All.SendAsync("OnMessageReceived",  message);
        //}

        public override Task OnConnectedAsync()
        {
                return base.OnConnectedAsync();
        }
    }
}
