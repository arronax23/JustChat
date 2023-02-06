using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustChat.ViewModels
{
    public class AnonymousMessageVM
    {
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }
        public string GroupName { get; set; }
    }
}
