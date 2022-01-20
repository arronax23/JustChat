using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustChat.Models;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JustChat.Pages.Chat
{
    public class CreateRoomModel : PageModel
    {
        private readonly IChatRepository _chatRepository;
        public CreateRoomModel(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public IEnumerable<string> AllUsersNames { get; set; }
        public string CurrentUserName { get; set; }

        public void OnGet()
        {
            AllUsersNames = _chatRepository.GetAllUsersUserNames();
            CurrentUserName = _chatRepository.GetCurrentUserName(User);
        }

    }
}
