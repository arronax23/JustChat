using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.ViewModels
{
    public class RoomVM
    {
        public string RoomName { get; set; }
        public IEnumerable<string> UserIds { get; set; }
    }
}
