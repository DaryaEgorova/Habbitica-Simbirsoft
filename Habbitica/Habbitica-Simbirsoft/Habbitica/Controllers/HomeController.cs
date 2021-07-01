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

            var posts = postService.GetPosts();

            if (posts.Count() != 0)
            {
                Random rnd = new Random();
                int[] usedNumbers = { -1, -1, -1, -1, -1 };
                int n = usedNumbers.Length;
                if (posts.Count() < n)
                {
                    n = posts.Count();
                }
                for (int i = 0; i < n;)
                {
                    int number = rnd.Next(0, posts.Count());
                    if (usedNumbers.Contains(number))
                    {
                        continue;
                    }

                    usedNumbers[i] = number;

                    string tag = "";

                    
                    homeView.PostsSlider.Add(new PostForHomeSliderModel()
                    {
                        Name = posts.ElementAt(number).Name,
                        PostedOn = posts.ElementAt(number).DateOfCreation,
                    });

                    

                    i++;
                }

                posts = posts.OrderByDescending(x => x.DateOfCreation).ToList();
                homeView.LatestPosts = new List<LatestPostModel>();
                n = 20;
                if (posts.Count() < n)
                {
                    n = posts.Count();
                }
                for(int i=0; i < n; i++)
                {
                    homeView.LatestPosts.Add(new LatestPostModel()
                    {
                        Name = posts.ElementAt(i).Name,
                        PostedOn = posts.ElementAt(i).DateOfCreation,
                      });
                }

                return View("Home", homeView);
            }
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
