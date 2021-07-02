using Habbitica.BLL_DAL.EF;
using Habbitica.BLL_DAL.Entity;
using Habbitica.BLL_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using Maganizer_Project.DAL.Repositories;

namespace Habbitica.BLL_DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly HabbiticaContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private AccountRepository accountRepository;
        private UserProfileRepository profileRepository;

        public EFUnitOfWork(HabbiticaContext db, UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IRepository<ToDo> ToDos { get; }

        public void Save()
        {
            db.SaveChanges();
        }

        public IAccountRepository Accounts
        {
            get
            {
                if (accountRepository == null)
                {
                    accountRepository = new AccountRepository(userManager, signInManager, db);
                }
                return accountRepository;
            }
        }

        public IProfileRepository UserProfiles
        {
            get
            {
                if (profileRepository == null)
                {
                    profileRepository = new UserProfileRepository(db);
                }
                return profileRepository;
            }
        }
        
      
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
