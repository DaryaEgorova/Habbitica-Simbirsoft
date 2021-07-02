using Habbitica.BLL_DAL.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Habbitica.BLL_DAL.EF
{
    public class HabbiticaContext: IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        // public DbSet<Post> Posts { get; set; } //тут таски тип
        public DbSet<MessageToAdmin> MessagesToAdmin { get; set; }

        public HabbiticaContext(DbContextOptions<HabbiticaContext> options)
            : base(options)
        {
        }
    }
}
