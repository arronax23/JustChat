using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JustChat.Pages.Chat
{
    [Authorize]
    public class AnonymousChatModel : PageModel
    {
        public string CurrentUsername { get; set; }
        public void OnGet()
        {
            CurrentUsername = User.Identity.Name;
        }
    }
}
