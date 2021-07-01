using System.ComponentModel.DataAnnotations;

namespace Habbitica.BLL_DAL.DTO
{
    public class SignInUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }     
        public string RememberMe { get; set; }
    }
}
