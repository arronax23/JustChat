using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.ViewModels
{
    public class RoomVM
    {
        [Display(Name = "Room Name")]
        public string RoomName { get; set; }
        public IEnumerable<string> InvitedUsersNames { get; set; }
    }
}
