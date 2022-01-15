using JustChat.Models;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthenticationRepository(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> Login(UserVM userVM)
        {
            var user = await _userManager.FindByNameAsync(userVM.Username);
            var signInResult = await _signInManager.PasswordSignInAsync(user, userVM.Password, false, false);

            return signInResult.Succeeded;
        }

        public async Task<bool> Register(UserVM userVM)
        {
            var registrationResult = await _userManager.CreateAsync(new User() { UserName = userVM.Username}, userVM.Password);

            return registrationResult.Succeeded;
        }
    }
}
