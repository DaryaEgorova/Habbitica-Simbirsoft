using Maganizer_Project.DAL.EF;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maganizer_Project.DAL.Repositories
{
    public class UserProfileRepository : IProfileRepository
    {
        private readonly MaganizerContext db;
        public UserProfileRepository(MaganizerContext context)
        {
            this.db = context;
        }
        public void Create(UserProfile item)
        {
            db.UserProfiles.Add(item);
        }

        public void Delete(int id)
        {
            UserProfile userProfile = db.UserProfiles.Find(id);
            if (userProfile != null)
                db.UserProfiles.Remove(userProfile);
        }

        public IEnumerable<UserProfile> Find(Func<UserProfile, bool> predicate)
        {
            return db.UserProfiles.Where(predicate).ToList();
        }

        public UserProfile Get(int? id)
        {
            return db.UserProfiles.Find(id);
        }

        public IEnumerable<UserProfile> GetAll()
        {
            return db.UserProfiles.Include(x => x.ApplicationUser);
        }

        public UserProfile GetByAccountId(string id)
        {
            return db.UserProfiles.Where(x => x.ApplicationUserId == id).FirstOrDefault();
        }

        public void Update(UserProfile item)
        {
            UserProfile userProfile = db.UserProfiles.Find(item.Id);
            if (userProfile != null)
            {
                userProfile.FirstName = item.FirstName;
                userProfile.LastName = item.LastName;
                userProfile.Country = item.Country;
                userProfile.City = item.City;
                userProfile.Street = item.Street;
                userProfile.About = item.About;
                userProfile.WebSiteUrl = item.WebSiteUrl;
                userProfile.EmailForContacts = item.EmailForContacts;

                if(item.Avatar != null)
                {
                    userProfile.Avatar = item.Avatar;
                }               

                db.UserProfiles.Update(userProfile);

                db.SaveChanges();
            }
                   
        }
    }
}
