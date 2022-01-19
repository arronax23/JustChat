using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JustChat.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        public UserVM UserVM { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(UserVM userVM, [FromServices] IAuthenticationRepository authenticationRepository)
        {
            var registrationResult = await authenticationRepository.Register(userVM);

            if (!registrationResult)
                return RedirectToPage("/Error");

            return RedirectToPage("SuccessfullRegistration");
        }
    }
}
