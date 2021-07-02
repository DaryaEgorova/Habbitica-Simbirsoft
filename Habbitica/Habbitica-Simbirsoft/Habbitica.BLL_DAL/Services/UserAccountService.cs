using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Linq;

namespace Maganizer_Project.BLL.Services
{
    public class UserAccountService : IAccountService
    {
        private IUnitOfWork DataBase { get; set; }

        public UserAccountService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public async Task<SignUpResultDTO> CreateUser(SignUpUserDTO signUpDTO)
        {
            ApplicationUser identityUser = new ApplicationUser()
            {
                UserName = signUpDTO.Username,
                Email = signUpDTO.Email
            };

            UserAccount user = new UserAccount()
            {
                IdentityUser = identityUser,
                Password = signUpDTO.Password
            };

            SignUpResultDTO resultDTO = new SignUpResultDTO()
            {
                Result = await DataBase.Accounts.CreateAsync(user)
            }; 

            if (resultDTO.Result.Succeeded)
            {
                UserProfile userProfile = new UserProfile()
                {
                    ApplicationUserId = user.IdentityUser.Id
                };

                DataBase.UserProfiles.Create(userProfile);
                DataBase.Save();

                var code = GetEmailVerificationInfo(identityUser.UserName).Result.VerificationCode;

                resultDTO.VerificationCode = code;
                resultDTO.UserId = identityUser.Id;
                resultDTO.Email = identityUser.Email;
                resultDTO.Username = identityUser.UserName;
            }
            
            return resultDTO;         
        }
        //create now
        public List<UserInfoDTO> GetInfoUsers()
        {
            var users = DataBase.Accounts.GetAll();
            List<UserInfoDTO> userInfoDTOs = new List<UserInfoDTO>();
            foreach (var x in users)
            {
                userInfoDTOs.Add(new UserInfoDTO()
                {
                    UserId = x.Id,
                    Email = x.Email,
                    Username = x.UserName
                });
            }
            return userInfoDTOs;
        }

        public async Task<IdentityResult> ConfirmEmail(string userId, string code)
        {
            var user = await DataBase.Accounts.GetById(userId);

            if(user == null)
            {
                return null;
            }

            var result = await DataBase.Accounts.ConfirmEmailAsync(user, code);

            return result;
        }

        public async Task<SignInResultDTO> SignInAsync(SignInUserDTO signInDTO)
        {
            bool RememberMeBool = false;

            if (signInDTO.RememberMe == "on")
            {
                RememberMeBool = true;
            }

            var result = new SignInResultDTO()
            {
                SignInResult = await DataBase.Accounts.PasswordSignInAsync(signInDTO.Username, signInDTO.Password,
                                                               RememberMeBool),
                EmailConfirmed = DataBase.Accounts.GetByName(signInDTO.Username).Result.EmailConfirmed
            };


            return result;
        }

        public async Task SignOutAsync()
        {
            await DataBase.Accounts.SignOutAsync();
        }

        public async Task<EmailVerificationInfoDTO> GetEmailVerificationInfo(string username)
        {
            var user = await DataBase.Accounts.GetByName(username);
            var code = await DataBase.Accounts.GetEmailConfirmationToken(user);
            return new EmailVerificationInfoDTO()
            {
                VerificationCode = code,
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName
            };
        }

        public void CreateMessageToAdmin(MessageToAdminDTO message)
        {
            MessageToAdmin messageToAdmin = new MessageToAdmin()
            {
                Content = message.Content,
                Subject = message.Subject,
                SentOn = message.SentOn
            };

            messageToAdmin.ApplicationUserId = DataBase.Accounts.GetByName(message.Username).Result.Id;

            DataBase.Accounts.CreateMessageToAdmin(messageToAdmin);
            DataBase.Save();
        }

        public List<MessageToAdminDTO> GetMessagesToAdmin()
        {
            var messages = DataBase.Accounts.GetMessagesToAdmin().ToList();

            if(messages.Count() != 0)
            {
                List<MessageToAdminDTO> messagesDTO = new List<MessageToAdminDTO>();
                foreach (var x in messages)
                {
                    messagesDTO.Add(new MessageToAdminDTO()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Subject = x.Subject,
                        Username = x.ApplicationUser.UserName,
                        SentOn = x.SentOn
                    });
                    
                }

                return messagesDTO;
            }

            return null;
        }

        public void DeleteMessageToAdmin(int id)
        {
            DataBase.Accounts.DeleteMessageToAdmin(id);
            DataBase.Save();
        }
    }
}
