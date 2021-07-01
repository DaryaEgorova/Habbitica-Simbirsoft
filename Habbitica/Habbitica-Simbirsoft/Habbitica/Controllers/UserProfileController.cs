using Habbitica.BLL_DAL.DTO;
using Habbitica.BLL_DAL.Interfaces;
using Habbitica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Habbitica.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfileService profileService;
        public UserProfileController(IUserProfileService profileService)
        {
            this.profileService = profileService;
        }

        //GET
        [Route("MyProfile")]
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            UserProfileDTO profileInfo = await profileService.GetProfile(User.Identity.Name);
            if(profileInfo == null)
            {
                return View("ErrorNotFound");
            }

            var profileViewModel = new UserProfileViewModel()
            {
                Username = profileInfo.Username,
                EmailForContacts = profileInfo.EmailForContacts,
                FacebookForContacts = profileInfo.FacebookForContacts,
                WebSiteUrl = profileInfo.WebSiteUrl,
                Country = profileInfo.Country,
                City = profileInfo.City,
                About = profileInfo.About,
                FullName = profileInfo.FirstName + " " + profileInfo.LastName,
                Avatar = profileInfo.Avatar,
            };


            return View("UserProfile", profileViewModel);   
        }

        //GET
        [Route("Users")]
        [HttpGet("username")]
        public async Task<IActionResult> GetProfile(string username)
        {
            if(User.Identity.Name == username)
            {
                return RedirectToAction("GetMyProfile");
            }

            UserProfileDTO profileInfo = await profileService.GetProfile(username);

            if (profileInfo == null)
            {
                return View("ErrorNotFound");
            }

            var profileViewModel = new UserProfileViewModel()
            {
                Username = profileInfo.Username,
                EmailForContacts = profileInfo.EmailForContacts,
                WebSiteUrl = profileInfo.WebSiteUrl,
                Country = profileInfo.Country,
                City = profileInfo.City,
                About = profileInfo.About,
                FullName = profileInfo.FirstName + " " + profileInfo.LastName,
                Avatar = profileInfo.Avatar,
            };


            return View("UserProfile", profileViewModel);
        }

        //GET
        [Route("EditProfile")]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {            
            UserProfileDTO profileInfo = await profileService.GetProfile(User.Identity.Name);
            if(profileInfo == null)
            {
                return View("ErrorNotFound");
            }

            var editProfileViewModel = new EditUserProfileViewModel()
            {
                EmailForContacts = profileInfo.EmailForContacts,
                WebSiteUrl = profileInfo.WebSiteUrl,
                Country = profileInfo.Country,
                City = profileInfo.City,
                About = profileInfo.About,
                FirstName = profileInfo.FirstName,
                LastName = profileInfo.LastName,
                OldAvatar = profileInfo.Avatar,
            };

            return View("EditUserProfile", editProfileViewModel);
        }

        //POST
        [Route("EditProfile")]
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserProfileViewModel editProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                EditUserProfileDTO editProfileDTO = new EditUserProfileDTO()
                {
                    Username = User.Identity.Name,
                    FirstName = editProfileViewModel.FirstName,
                    LastName = editProfileViewModel.LastName,
                    EmailForContacts = editProfileViewModel.EmailForContacts,
                    WebSiteUrl = editProfileViewModel.WebSiteUrl,
                    Country = editProfileViewModel.Country,
                    City = editProfileViewModel.City,
                    About = editProfileViewModel.About,
                    NewAvatar = editProfileViewModel.NewAvatar,
                };

                profileService.UpdateProfile(editProfileDTO);

                return RedirectToAction("GetMyProfile");
            }

            var profile = await profileService.GetProfile(User.Identity.Name);

            editProfileViewModel.OldAvatar = profile.Avatar;

            return View("EditUserProfile", editProfileViewModel);
            
        }

    }
}
