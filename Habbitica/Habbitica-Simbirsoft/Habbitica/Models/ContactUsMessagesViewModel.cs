using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habbitica.Models
{
    public class ContactUsMessagesViewModel
    {
        public List<MessageModel> messages { get; set; }
    }

    public class MessageModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public string AuthorUsername { get; set; }
        public DateTime SentOn { get; set; }
    }
}
