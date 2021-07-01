using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Habbitica.BLL_DAL.DTO
{
    public class SignInResultDTO
    {
        public SignInResult SignInResult { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
