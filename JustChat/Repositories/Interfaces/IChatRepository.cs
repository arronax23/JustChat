using JustChat.Models;
using JustChat.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JustChat.Repositories.Inrefaces
{
    public interface IChatRepository
    {
        Task<bool> AddUserToRoom(AddUserToRoomVM addUserToRoomVM);
        Task<bool> CreateRoom(NewRoomVM newRoomVM);
        IEnumerable<User> GetAllUsers();
        IEnumerable<string> GetAllUsersUserNames();
        ChatVM GetChatViewModel(int roomId, ClaimsPrincipal user);
    }
}