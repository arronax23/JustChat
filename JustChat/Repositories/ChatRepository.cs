using JustChat.Database;
using JustChat.Models;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
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

        public IEnumerable<User> GetAllUsers()
        {
            return _appDbContext.Users;
        }
    }
}
