using System.ComponentModel.DataAnnotations;

namespace Habbitica.Models
{
    public class SignUpViewModel
    {
        [RegularExpression(@"^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$",
            ErrorMessage = "Your username can be 8-20 characters long. " +
            "No \"_\" or \".\" at the beginning. " +
            "No \"__\" or \"_.\" or \"._\" or \"..\" inside. " +
            "Allowed characters - Aa-Zz; 0-9. " +
            "No \"_\" or \".\" at the end")]
        [Required(ErrorMessage = "Please enter your username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a strong password")]
        [Compare("ConfirmPassword", ErrorMessage = "Password does not match")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    
}
