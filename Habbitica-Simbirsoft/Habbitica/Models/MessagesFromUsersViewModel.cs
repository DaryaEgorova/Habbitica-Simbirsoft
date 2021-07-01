using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habbitica.Models
{
    public class MessagesFromUsersViewModel
    {
        public List<MessageFromUser> Messages { get; set; }
    }

    public class MessageFromUser
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public DateTime SentOn { get; set; }
        public string AuthorName { get; set; }
    }
}
