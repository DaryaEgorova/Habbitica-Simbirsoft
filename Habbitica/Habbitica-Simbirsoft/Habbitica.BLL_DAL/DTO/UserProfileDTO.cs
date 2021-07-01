namespace Habbitica.BLL_DAL.DTO
{
    public class UserProfileDTO
    {
        public string Username { get; set; }
        public string EmailForContacts { get; set; }
        public string FacebookForContacts { get; set; }
        public string WebSiteUrl { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string About { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Avatar { get; set; }
    }
}
