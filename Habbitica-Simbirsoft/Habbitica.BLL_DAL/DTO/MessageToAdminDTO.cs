using System;
using System.Collections.Generic;
using System.Text;

namespace Habbitica.BLL_DAL.DTO
{
    public class MessageToAdminDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
        public DateTime SentOn { get; set; }
        public string Username { get; set; }
    }
}
