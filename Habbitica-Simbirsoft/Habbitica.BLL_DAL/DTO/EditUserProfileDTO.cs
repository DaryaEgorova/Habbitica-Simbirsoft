using Microsoft.AspNetCore.Http;

namespace Habbitica.BLL_DAL.DTO
{
    public class EditUserProfileDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailForContacts { get; set; }
        public string FacebookForContacts { get; set; }
        public string WebSiteUrl { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string About { get; set; }
        public IFormFile NewAvatar { get; set; }
    }
}
