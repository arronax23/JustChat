using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
