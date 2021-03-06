using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JustChat.Pages.Chat
{
    public class RoomCreatedModel : PageModel
    {
        public string CreatedRoomName { get; set; }
        public void OnGet(string roomName)
        {
            CreatedRoomName = roomName;
        }
    }
}
