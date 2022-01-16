using JustChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.ViewModels
{
    public class ChatVM
    {
        public string RoomName { get; set; }
        public int RoomId { get; set; }
        public string AuthorId { get; set; }
        public string Username { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
