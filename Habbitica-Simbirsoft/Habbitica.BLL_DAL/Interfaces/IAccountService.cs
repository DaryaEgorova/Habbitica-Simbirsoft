using Habbitica.BLL_DAL.Entity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Habbitica.BLL_DAL.DTO;

namespace Habbitica.BLL_DAL.Interfaces
{
    public interface IAccountService
    {
        Task<SignUpResultDTO> CreateUser(SignUpUserDTO signUpUser);
        Task<SignInResultDTO> SignInAsync(SignInUserDTO signInUser);
        Task SignOutAsync();
        Task<IdentityResult> ConfirmEmail(string userId, string code);
        List<UserInfoDTO> GetInfoUsers();
        void CreateMessageToAdmin(MessageToAdminDTO message);
        List<MessageToAdminDTO> GetMessagesToAdmin();
        void DeleteMessageToAdmin(int id);

    }
}
