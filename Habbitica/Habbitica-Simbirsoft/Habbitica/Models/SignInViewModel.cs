using System.ComponentModel.DataAnnotations;

namespace Habbitica.Models
{
    public class SignInViewModel
    {   
        [Required(ErrorMessage = "Please enter your username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }   
    }
}
