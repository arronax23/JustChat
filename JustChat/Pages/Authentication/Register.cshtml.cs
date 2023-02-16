using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustChat.Models;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JustChat.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly UserManager<User> _userManager;

        public UserVM UserVM { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public RegisterModel(IPasswordValidator<User> passwordValidator, UserManager<User> userManager)
        {
            _passwordValidator = passwordValidator;
            _userManager = userManager;
        }

        public void OnGet(List<string> errors = null)
        {
            Errors = errors;
        }

        public async Task<IActionResult> OnPost(UserVM userVM, [FromServices] IAuthenticationRepository authenticationRepository)
        {
            var passwordValidationResult = await _passwordValidator.ValidateAsync(_userManager, new User() { UserName = userVM.Username }, userVM.Password);
            if (passwordValidationResult.Succeeded)
            {
                var registrationResult = await authenticationRepository.Register(userVM);

                if (!registrationResult)
                    return RedirectToPage("/Error");

                return RedirectToPage("SuccessfullRegistration");
            }

            IEnumerable<string> passwordValidationErrors = passwordValidationResult.Errors.Select(ie => ie.Description);
            return RedirectToPage("Register", new {errors = passwordValidationErrors } );
            
        }
    }
}
