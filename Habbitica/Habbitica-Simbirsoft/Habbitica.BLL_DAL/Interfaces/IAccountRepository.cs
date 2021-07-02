using Habbitica.BLL_DAL.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Habbitica.BLL_DAL.Interfaces
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateAsync(UserAccount item);
        Task<SignInResult> PasswordSignInAsync(string Username, string Password, bool RememberMe);
        Task SignOutAsync();
        Task<ApplicationUser> GetByName(string username);
        Task<ApplicationUser> GetById(string id);
        Task<string> GetEmailConfirmationToken(ApplicationUser user);
        Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code);
        IEnumerable<ApplicationUser> GetAll();
        void CreateMessageToAdmin(MessageToAdmin messageToAdmin);
        IEnumerable<MessageToAdmin> GetMessagesToAdmin();
        void DeleteMessageToAdmin(int id);
    }
}
