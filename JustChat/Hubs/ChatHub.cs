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

        public async Task SendRoomMessage(
            string content,
            string userName,
            string authorId,
            int roomId,
            string roomName)
        {

            DateTime timeStamp = DateTime.Now;

            var message = new Message()
            {
                Content = content,
                TimeStamp = timeStamp,
                AuthorId = authorId,
                RoomId = roomId
            };

            _appDbContext.Messages.Add(message);
            await _appDbContext.SaveChangesAsync();

            var messageVM = new MessageVM()
            {
                Content = content,
                TimeStamp = timeStamp,
                UserName = userName
            };

            await Clients
                .Group(roomName)
                .SendAsync("ReceiveMessage", messageVM);
        }

    }
}
