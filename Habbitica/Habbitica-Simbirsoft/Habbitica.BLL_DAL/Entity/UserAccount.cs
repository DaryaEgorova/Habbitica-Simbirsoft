using Habbitica.BLL_DAL.Entity;

namespace Habbitica.BLL_DAL.Entities
{
    public class UserAccount
    {    
        public ApplicationUser IdentityUser { get; set; }
        public string Password { get; set; }
    }
}
