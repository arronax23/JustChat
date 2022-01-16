using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub()
        {

        }

        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendMessage(string roomName, string author, string message, string timeStamp)
        {
            await Clients
                .Group(roomName)
                .SendAsync("ReceiveMessage", author, message, timeStamp);
        }

    }
}
