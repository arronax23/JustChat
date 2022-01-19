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

        public async Task JoinRoom(string roomName,string userName)
        {
            var room = _appDbContext.Rooms.SingleOrDefault(r => r.Name == roomName);

            _appDbContext.ActiveRoomUsers.Add(new ActiveRoomUser()
            {
                Room = room,
                UserName = userName
            });;

            await _appDbContext.SaveChangesAsync();

            var activeUsers = _appDbContext.ActiveRoomUsers
                .Where(aru => aru.Room == room)
                .Select(aru => aru.UserName)
                .AsEnumerable();

            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients
                .Group(roomName)
                .SendAsync("JoinedRoom", activeUsers);
        }

        public async Task LeaveRoom(string roomName,string userName)
        {
            var room = _appDbContext.Rooms.SingleOrDefault(r => r.Name == roomName);
            var activeUserLeavingRoom = _appDbContext.ActiveRoomUsers.FirstOrDefault(aru => aru.Room == room && aru.UserName == userName);
            //var activeUserLeavingRoom = _appDbContext.ActiveRoomUsers.Where(aru => aru.Room == room && aru.UserName == userName);

            _appDbContext.ActiveRoomUsers.RemoveRange(activeUserLeavingRoom);
            await _appDbContext.SaveChangesAsync();


            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients
                .Group(roomName)
                .SendAsync("LeftRoom", userName);
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
