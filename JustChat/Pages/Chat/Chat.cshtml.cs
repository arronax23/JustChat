using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JustChat.Database;
using JustChat.Hubs;
using JustChat.Models;
using JustChat.Repositories.Inrefaces;
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
        private readonly IChatRepository _chatRepository;

        public ChatModel(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public ChatVM ChatVM { get; set; }

        public void OnGet(int roomId)
        {
            ChatVM = _chatRepository.GetChatViewModel(roomId, User);
        }
    }
}
