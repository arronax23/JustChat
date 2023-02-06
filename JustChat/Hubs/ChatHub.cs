using JustChat.Database;
using JustChat.Models;
using JustChat.ViewModels;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

        public async Task SendAnonymousMessage(
            string content,
            string userName,
            string groupName)
        {

            DateTime timeStamp = DateTime.Now;

            var anonymousMessageVM = new AnonymousMessageVM()
            {
                Content = content,
                TimeStamp = timeStamp,
                UserName = userName,
                GroupName = groupName
            };

            await Clients
                .Group(groupName)
                .SendAsync("ReceiveAnonymousMessage", anonymousMessageVM);
        }

        public async Task LeaveLobby(string userName)
        {
            var anonymousConnetion = _appDbContext.AnonymousConnections
                .Include(a => a.User)
                .SingleOrDefault(a => a.User.UserName == userName);

            if (anonymousConnetion != null)
            {
                _appDbContext.AnonymousConnections.Remove(anonymousConnetion);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task LeaveAnonymousGroup(string group, string userName)
        {
            var anonymousConnetions = _appDbContext.AnonymousConnections
                .Include(a => a.User)
                .Where(a => a.Group == group || a.User.UserName == userName);

            _appDbContext.AnonymousConnections.RemoveRange(anonymousConnetions);
            await _appDbContext.SaveChangesAsync();

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);

            await Clients
                .Group(group)
                .SendAsync("StrangerLeftConversation");
        }

        public async Task LeaveAnonymousGroupShortened(string group)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }

        public async Task PairAnonymousUsers(string userName)
        {
            string currentUserId = GetUserIdByUserName(userName);

            var currentAnonymousConnection = GetCurrentAnonymousConnection(currentUserId);

            if (currentAnonymousConnection == null)
            {
                _appDbContext.AnonymousConnections.Add(new AnonymousConnection() { UserId = currentUserId });
                await _appDbContext.SaveChangesAsync();

                currentAnonymousConnection = GetCurrentAnonymousConnection(currentUserId);
            }

            await _appDbContext.Entry(currentAnonymousConnection).ReloadAsync();
            bool isPaired = currentAnonymousConnection.IsPaired;
            AnonymousConnection potentialPair = _appDbContext.AnonymousConnections.SingleOrDefault(c => c.UserId != currentUserId && c.IsPaired == false);

            if (isPaired)
            {
                string group = GetCurrentAnonymousConnection(currentUserId).Group;
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
                await Clients
                    .Group(group)
                    .SendAsync("ReceiveGroupName", group);

                return;
            }

            if (potentialPair != null)
            {
                string groupName = Guid.NewGuid().ToString();

                currentAnonymousConnection.Group = groupName;
                currentAnonymousConnection.IsPaired = true;
                potentialPair.Group = groupName;
                potentialPair.IsPaired = true;
                await _appDbContext.SaveChangesAsync();

                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            }

        }

        public string GetUserIdByUserName(string userName)
        {
            return _appDbContext.Users.SingleOrDefault(u => u.UserName == userName).Id;
        }

        public AnonymousConnection GetCurrentAnonymousConnection(string userId)
        {
            return _appDbContext.AnonymousConnections.SingleOrDefault(a => a.UserId == userId);
        }

    }
}
