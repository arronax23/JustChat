using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public User Author { get; set; }
        public string AuthorId { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
    }
}
