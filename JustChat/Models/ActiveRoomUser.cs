using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Models
{
    public class ActiveRoomUser
    {
        public int ActiveRoomUserId { get; set; }
        public string UserName { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
