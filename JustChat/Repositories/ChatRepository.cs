using JustChat.Database;
using JustChat.Models;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void SaveMessage(Message message)
        {
            //_appDbContext.Mess
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

        public IEnumerable<User> GetAllUsers()
        {
            return _appDbContext.Users;
        }
    }
}
