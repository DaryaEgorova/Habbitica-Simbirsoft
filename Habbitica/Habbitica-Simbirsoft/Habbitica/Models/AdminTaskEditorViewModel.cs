using Habbitica.BLL_DAL.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Habbitica.Models
{
    public class AdminTaskEditorViewModel
    {
        [Required(ErrorMessage = "Please enter task's name")]
        [StringLength(100)]
        public string PostName { get; set; }

        [Required(ErrorMessage = "You can not post empty news")]
        public string PostContent { get; set; }

        [Required(ErrorMessage = "Please enter at least one tag")]
        [StringLength(100)]
        public string Tags { get; set; }

        [Required(ErrorMessage = "Please choose a featured image")]
        public IFormFile FeaturedImage { get; set; }
        public bool SuccessPost { get; set; }
    }
}
