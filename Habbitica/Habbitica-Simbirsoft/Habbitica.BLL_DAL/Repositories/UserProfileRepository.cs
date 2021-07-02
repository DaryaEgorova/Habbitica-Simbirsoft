using Habbitica.BLL_DAL.EF;
using Habbitica.BLL_DAL.Entity;
using Habbitica.BLL_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Habbitica.BLL_DAL.Repositories
{
    public class UserProfileRepository : IProfileRepository
    {
        private readonly HabbiticaContext db;
        public UserProfileRepository(HabbiticaContext context)
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
                userProfile.About = item.About;
                userProfile.EmailForContacts = item.EmailForContacts;
                userProfile.FacebookForContacts = item.FacebookForContacts;

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
