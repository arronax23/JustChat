using JustChat.Database;
using JustChat.Models;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JustChat.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _appDbContext;

        public ChatRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _appDbContext.Users;
        }

        public IEnumerable<string> GetAllUsersUserNames()
        {
            return GetAllUsers().Select(u => u.UserName);

        }

        public User GetCurrentUser(ClaimsPrincipal user)
        {
            return _appDbContext.Users.FirstOrDefault(u => u.UserName == user.Identity.Name);
        }
        public string GetCurrentUserName(ClaimsPrincipal user)
        {
            return GetCurrentUser(user).UserName;
        }

        public ChatVM GetChatViewModel(int roomId, ClaimsPrincipal user)
        {
            Room room = _appDbContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);

            var chatVM = new ChatVM()
            {
                RoomId = roomId,
                AuthorId = GetCurrentUser(user).Id,
                CurrentUserName = GetCurrentUserName(user),
                RoomName = room.Name,
                Messages = _appDbContext.Messages.Include(m => m.Author).Where(m => m.RoomId == roomId).OrderBy(m => m.TimeStamp),
                UserNamesInvitedToRoom = _appDbContext.Users.Include(u => u.Rooms).Where(u => u.Rooms.Contains(room)).Select(u => u.UserName),
                UsersNamesPossibleToInvite = _appDbContext.Users.Where(u => !u.Rooms.Contains(room)).Select(u => u.UserName)
            };

            return chatVM;
        }


        public async Task<bool> CreateRoom(NewRoomVM newRoomVM)
        {
            var newRoom = new Room()
            {
                Name = newRoomVM.RoomName,
                Users = _appDbContext.Users.Where(u => newRoomVM.InvitedUsersNames.Contains(u.UserName)).ToList()
            };

            _appDbContext.Rooms.Add(newRoom);
            var isCreated = await _appDbContext.SaveChangesAsync() > 0;

            return isCreated;
        }

        public async Task<bool> AddUserToRoom(AddUserToRoomVM addUserToRoomVM)
        {
            var user = _appDbContext.Users.Include(u => u.Rooms).SingleOrDefault(u => u.UserName == addUserToRoomVM.UserName);
            var room = _appDbContext.Rooms.SingleOrDefault(r => r.RoomId == addUserToRoomVM.RoomId);
            user.Rooms.Add(room);

            var isModified = await _appDbContext.SaveChangesAsync() > 0;
            return isModified;
        }
    }
}
