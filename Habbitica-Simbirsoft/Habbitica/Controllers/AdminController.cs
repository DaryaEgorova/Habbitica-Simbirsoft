using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Habbitica.BLL_DAL.DTO;
using Habbitica.BLL_DAL.Interfaces;
using Habbitica.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Habbitica.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IPostService postService;
        private readonly IAccountService accountService;

        public AdminController(IWebHostEnvironment hostingEnvironment, IPostService postService, IAccountService accountService)
        {
            _hostingEnvironment = hostingEnvironment;
            this.postService = postService;
            this.accountService = accountService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Index()
        {
            var users = accountService.GetInfoUsers();

            if (users.Count() != 0)
            {
                AdminIndexViewModel adminIndexViewModel = new AdminIndexViewModel
                {
                    Users = new List<UserInfoDTO>(),
                };

                foreach (var x in users)
                {
                    adminIndexViewModel.Users.Add(x);
                }

               
                return View("Index", adminIndexViewModel);
            }

            return View("Index");

        }
     
        
        [Authorize(Roles = "Admin")]
        [HttpGet("MessagesFromUsers")]
        public IActionResult MessagesFromUsers()
        {
            var messages = accountService.GetMessagesToAdmin();
            if(messages != null)
            {
                MessagesFromUsersViewModel messagesModel = new MessagesFromUsersViewModel()
                {
                    Messages = new List<MessageFromUser>()
                };

                foreach (var x in messages)
                {
                    messagesModel.Messages.Add(new MessageFromUser()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Subject = x.Subject,
                        AuthorName = x.Username,
                        SentOn = x.SentOn
                    });
                }
                messagesModel.Messages = messagesModel.Messages.OrderByDescending(x => x.SentOn).ToList();

                return View("MessagesFromUsers", messagesModel);
            }

            return View("MessagesFromUsers", new MessagesFromUsersViewModel() { Messages = new List<MessageFromUser>() });
        }

        [HttpPost]
        public IActionResult DeleteMessageFromUser(int id)
        {
            accountService.DeleteMessageToAdmin(id);
            return RedirectToAction("MessagesFromUsers");
        }
    }
}
