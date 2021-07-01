using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Habbitica.BLL_DAL.Entity
{
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailForContacts { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string About { get; set; }
        public string WebSiteUrl { get; set; }
        public byte[] Avatar { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
