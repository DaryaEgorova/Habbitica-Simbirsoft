using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Habbitica.Models
{
    public class ContactUsViewModel
    {         
        [Required]
        [StringLength(200, ErrorMessage = "Max length - 200")]
        public string Message { get; set; }

        [StringLength(30, ErrorMessage = "Max length - 30")]
        public string Subject { get; set; }
        public bool Success { get; set; }
    }
}
