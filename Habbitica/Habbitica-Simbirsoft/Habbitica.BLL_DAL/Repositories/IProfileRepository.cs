using Habbitica.BLL_DAL.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Habbitica.BLL_DAL.Interfaces
{
    public interface IProfileRepository
    {
        IEnumerable<UserProfile> GetAll();
        UserProfile Get(int? id);
        UserProfile GetByAccountId(string id);
        IEnumerable<UserProfile> Find(Func<UserProfile, Boolean> predicate);
        void Create(UserProfile item);
        void Update(UserProfile item);
        void Delete(int id);
    }
}
