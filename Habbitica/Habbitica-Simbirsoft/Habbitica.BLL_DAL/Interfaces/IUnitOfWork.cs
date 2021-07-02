using Habbitica.BLL_DAL.Entity;
using Habbitica.BLL_DAL.Repositories;
using System;

namespace Habbitica.BLL_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IProfileRepository UserProfiles { get; }
        IRepository<ToDo> ToDos { get; }

      void Save();
    }
}
