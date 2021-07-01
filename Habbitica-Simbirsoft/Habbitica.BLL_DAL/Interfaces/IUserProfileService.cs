using System;
using System.Threading.Tasks;
using Habbitica.BLL_DAL.DTO;

namespace Habbitica.BLL_DAL.Interfaces
{
    public interface IUserProfileService
    {
        void UpdateProfile(EditUserProfileDTO editProfileDTO);
        Task<UserProfileDTO> GetProfile(string username);
    }
}
