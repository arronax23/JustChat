using JustChat.Models;
using JustChat.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustChat.Repositories.Inrefaces
{
    public interface IChatRepository
    {
        Task<bool> CreateRoom(RoomVM roomVM);
        IEnumerable<User> GetAllUsers();
        void SaveMessage(Message message);
    }
}