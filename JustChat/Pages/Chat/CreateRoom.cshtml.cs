using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JustChat.Pages.Chat
{
    public class CreateRoomModel : PageModel
    {
        [BindProperty]
        public RoomVM RoomVM { get; set; }
        public void OnGet()
        {
        }
        public void OnPostAsync()
        {
    
        }
    }
}
