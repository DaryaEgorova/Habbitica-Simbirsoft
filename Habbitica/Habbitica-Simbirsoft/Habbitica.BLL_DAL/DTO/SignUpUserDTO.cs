using System.ComponentModel.DataAnnotations;

namespace Habbitica.BLL_DAL.DTO
{
    public class SignUpUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
