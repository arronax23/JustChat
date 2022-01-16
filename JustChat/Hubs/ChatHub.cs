using JustChat.Database;
using JustChat.Models;
using JustChat.ViewModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly AppDbContext _appDbContext;

        public ChatHub(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task JoinRoom(string roomName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        public async Task SendRoomMessage(MessageVM messageVM)
        {
            messageVM.TimeStamp = DateTime.UtcNow;

            var message = new Message()
            {
                Content = messageVM.Content,
                TimeStamp = messageVM.TimeStamp,
                AuthorId = messageVM.AuthorId,
                RoomId = messageVM.RoomId
            };

            _appDbContext.Messages.Add(message);

            await Clients
                .Group(messageVM.RoomName)
                .SendAsync("ReceiveMessage", messageVM);
        }

    }
}
