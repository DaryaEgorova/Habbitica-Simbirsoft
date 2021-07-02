using Habbitica.BLL_DAL.DTO;
using Habbitica.BLL_DAL.Infrastructure;
using Habbitica.BLL_DAL.Interfaces;
using Habbitica.BLL_DAL.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Habbitica.BLL_DAL.DTO;
using Habbitica.BLL_DAL.Entity;
using Habbitica.BLL_DAL.Interfaces;

namespace Habbitica.BLL_DAL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private IUnitOfWork DataBase { get; set; }
        
        public UserProfileService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public async Task<UserProfileDTO> GetProfile(string username)
        {
            var accountResult = await DataBase.Accounts.GetByName(username);

            if(accountResult == null)
            {
                return null;
            }

            var userProfiles = DataBase.UserProfiles.GetAll();

            var userProfile = userProfiles.FirstOrDefault(x => x.ApplicationUserId == accountResult.Id);
            
            if(userProfile == null)
            {
                return null;
            }

            return new UserProfileDTO()
            {
                Username = userProfile.ApplicationUser.UserName,
                About = userProfile.About,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                EmailForContacts = userProfile.EmailForContacts,
                FacebookForContacts = userProfile.FacebookForContacts,
                Country = userProfile.Country,
                City = userProfile.City,
                Avatar = userProfile.Avatar
            };

        }
        public void UpdateProfile(EditUserProfileDTO editProfileDTO)
        {
            var account = DataBase.Accounts.GetByName(editProfileDTO.Username);

            byte[] newAvatar;

            if (editProfileDTO.NewAvatar != null)
            {
                newAvatar = ImageConvertion.ConvertToByteArray(editProfileDTO.NewAvatar);
            }
            else
            {
                newAvatar = null;
            }

            var profileId = DataBase.UserProfiles.GetByAccountId(account.Result.Id).Id;

            UserProfile userProfile = new UserProfile()
            {
                Id = profileId,
                FirstName = editProfileDTO.FirstName,
                LastName = editProfileDTO.LastName,
                Country = editProfileDTO.Country,
                City = editProfileDTO.City,
                About = editProfileDTO.About,
                EmailForContacts = editProfileDTO.EmailForContacts,
                FacebookForContacts = editProfileDTO.FacebookForContacts,
                Avatar = newAvatar,
                ApplicationUserId = account.Result.Id
            };

            DataBase.UserProfiles.Update(userProfile);
            DataBase.Save();

        }

    }
}
