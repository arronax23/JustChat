using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.Models
{
    public class AnonymousConnection
    {
        public int AnonymousConnectionId { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string Group { get; set; }
        public bool IsPaired { get; set; }
    }
}
