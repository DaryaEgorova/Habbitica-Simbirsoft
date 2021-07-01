using Microsoft.AspNetCore.Mvc;
using Habbitica.BLL_DAL.Interfaces;
using System.Threading.Tasks;
using Habbitica.Models;
using Habbitica.BLL_DAL.DTO;
using NETCore.MailKit.Core;


namespace Habbitica.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IEmailService emailService;
        public AccountController(IAccountService accountService, IEmailService emailService)
        {
            this.accountService = accountService;
            this.emailService = emailService;
        }

        //GET
        [Route("SignIn")]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View("SignIn");
        }

        //GET
        [Route("SignUp")]
        [HttpGet]
        public IActionResult SignUp()
        {
            return View("SignUp");
        }

        //POST
        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var signUpDTO = new SignUpUserDTO()
                {
                    Username = signUpModel.Username,
                    Email = signUpModel.Email,
                    Password = signUpModel.Password,
                };

                var result = await accountService.CreateUser(signUpDTO);

                if (!result.Result.Succeeded)
                {
                    foreach(var error in result.Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("SignUp", signUpModel);
                }
                
            }

            return View("SignUp", signUpModel);
        }

        
        //POST
        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signInModel, string RememberMe)
        {
            if (ModelState.IsValid)
            {               
                var signInDTO = new SignInUserDTO()
                {
                    Username = signInModel.Username,
                    Password = signInModel.Password,
                    RememberMe = RememberMe
                };

                var result = await accountService.SignInAsync(signInDTO);                          

                if (result.SignInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password are wrong. Please try again");
                }

            }
            
            return View("SignIn", signInModel);
        }

        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await accountService.SignOutAsync();
            return RedirectToAction("SignIn");
        }
    }
}
