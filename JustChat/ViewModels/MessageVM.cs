using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.ViewModels
{
    public class MessageVM
    {
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }
        public string AuthorId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
    }
}
