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
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ChatModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public ChatVM ChatVM { get; set; }
        [BindProperty]
        public MessageVM MessageVM { get; set; }

        public ChatModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void OnGet(int roomId)
        {
            ChatVM = new ChatVM();

            ChatVM.RoomId = roomId;
            ChatVM.AuthorId = _appDbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id;
            ChatVM.UserName = _appDbContext.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).UserName;
            Room room = _appDbContext.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            ChatVM.RoomName = room.Name;
            ChatVM.Users = _appDbContext.Users.Include(u => u.Rooms).Where(u => u.Rooms.Contains(room));
            ChatVM.Messages = _appDbContext.Messages.Include(m => m.Author).Where(m => m.RoomId == roomId);

        }

        //public async Task<IActionResult> OnPostAsync([FromServices] IHubContext<ChatHub> hubContext)
        //{
        //    MessageVM.TimeStamp = DateTime.UtcNow;

        //    var message = new Message()
        //    {
        //        Content = MessageVM.Content,
        //        TimeStamp = MessageVM.TimeStamp,
        //        //AuthorId = MessageVM.AuthorId,
        //        RoomId = MessageVM.RoomId
        //    };

        //    _appDbContext.Messages.Add(message);
        //    await _appDbContext.SaveChangesAsync();

        //    await hubContext
        //        .Clients
        //        .Group(MessageVM.RoomName)
        //        .SendAsync("ReceiveMessage", MessageVM);

        //    return RedirectToPage("Chat", new { roomId = MessageVM.RoomId });
        //}
    }
}
