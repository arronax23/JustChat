using JustChat.Database;
using JustChat.Models;
using JustChat.Repositories.Inrefaces;
using JustChat.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext _appDbContext;

        public AuthenticationRepository(SignInManager<User> signInManager, UserManager<User> userManager, AppDbContext appDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appDbContext= appDbContext;
        }

        public async Task<bool> Login(UserVM userVM)
        {
            var user = await _userManager.FindByNameAsync(userVM.Username);
            if(user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, userVM.Password, false, false);

                return signInResult.Succeeded;
            }

            return false;
        }

        public async Task<bool> Register(UserVM userVM)
        {
            var registrationResult = await _userManager.CreateAsync(new User() { UserName = userVM.Username}, userVM.Password);

            var user =_appDbContext.Users.Include(u => u.Rooms).SingleOrDefault(u => u.UserName == userVM.Username);
            var mainRoom = _appDbContext.Rooms.SingleOrDefault(r => r.RoomId == 1 && r.Name == "Main Room");

            user.Rooms.Add(mainRoom);
            var isModified = await _appDbContext.SaveChangesAsync() > 0;

            return registrationResult.Succeeded && isModified;
        }
    }
}
