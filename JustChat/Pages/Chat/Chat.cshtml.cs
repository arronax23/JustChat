using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JustChat.Database;
using JustChat.Hubs;
using JustChat.Models;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;



namespace JustChat.Pages.Chat
{
    [Authorize]
    public class ChatModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public ChatVM ChatVM { get; set; }
        public IEnumerable<string> UsersNamesPossibleToInvite { get; set; }

        public ChatModel(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
        }

        public void OnGet(int roomId)
        {
            ChatVM = new ChatVM();

            ChatVM.RoomId = roomId;
            ChatVM.AuthorId = _appDbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
            ChatVM.CurrentUserName = _appDbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserName;
            Room room = _appDbContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            ChatVM.RoomName = room.Name;
            ChatVM.Messages = _appDbContext.Messages.Include(m => m.Author).Where(m => m.RoomId == roomId);

            ChatVM.UserNames = _appDbContext.Users.Include(u => u.Rooms).Where(u => u.Rooms.Contains(room)).Select(u => u.UserName);

            UsersNamesPossibleToInvite = _appDbContext.Users.Where(u => !u.Rooms.Contains(room)).Select(u => u.UserName);
        }
    }
}
