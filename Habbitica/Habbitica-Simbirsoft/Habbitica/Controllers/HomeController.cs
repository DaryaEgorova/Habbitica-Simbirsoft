using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Habbitica.Models;
using Habbitica.Extensions;
using Habbitica.BLL_DAL.DTO;
using Habbitica.BLL_DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Habbitica.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService postService;

       private readonly IAccountService accountService;
        public HomeController(IPostService postService, IAccountService accountService)
        {
            this.postService = postService;
            this.accountService = accountService;
        }

        [HttpGet]
        public IActionResult Index()
        {      
            HomeViewModel homeView = new HomeViewModel()
            {
                PostsSlider = new List<PostForHomeSliderModel>()
            };

            return View("Home");
        }

        
        [Authorize]
        [HttpGet("ContactUs")]
        public IActionResult ContactUs()
        {
            return View("ContactUs");
        }

        [Authorize]
        [HttpPost("ContactUs")]
        public IActionResult SendContactMessage(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                MessageToAdminDTO contactUsMessage = new MessageToAdminDTO()
                {
                    Content = model.Message,
                    Subject = model.Subject,
                    Username = User.Identity.Name,
                    SentOn = DateTime.Now
                };

                accountService.CreateMessageToAdmin(contactUsMessage);

                model.Success = true;

                return View("ContactUs", model);
            }

            return View("ContactUs", model);
        }
    }
}
