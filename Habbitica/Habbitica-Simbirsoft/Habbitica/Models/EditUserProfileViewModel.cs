using Habbitica.BLL_DAL.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Habbitica.Models
{
    public class EditUserProfileViewModel
    {
        [Required]
        [StringLength(45, ErrorMessage = "Max length - 45")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(45, ErrorMessage = "Max length - 45")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email for contacts")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string EmailForContacts { get; set; }
        
        
        [Required]
        [StringLength(2048)]
        public string FacebookForContacts { get; set; }

        

        [Required]
        [StringLength(45, ErrorMessage = "Max length - 45")]
        public string Country { get; set; }

        [Required]
        [StringLength(45, ErrorMessage = "Max length - 45")]
        public string City { get; set; }

        [Required]
        [StringLength(45, ErrorMessage = "Max length - 45")]
        public string Street { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Max length - 150")]
        public string About { get; set; }

        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile NewAvatar { get; set; }

        public byte[] OldAvatar { get; set; }
    }
}
