using JustChat.Database;
using JustChat.Models;
using JustChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Repositories
{
    public class ChatRepository
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
        public async Task CreateRoom(RoomVM roomVM)
        {
            var newRoom = new Room()
            {
                Name = roomVM.RoomName,
                Users = _appDbContext.Users.Where(u => roomVM.UserIds.Contains(u.Id)).ToList()
            };
            _appDbContext.Rooms.Add(newRoom);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
