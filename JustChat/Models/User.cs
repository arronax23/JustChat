using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Models
{
    public class User : IdentityUser
    {
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
