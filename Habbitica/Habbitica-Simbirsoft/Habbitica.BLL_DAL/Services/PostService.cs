using Habbitica.BLL_DAL.DTO;
using Habbitica.BLL_DAL.Interfaces;
using Habbitica.BLL_DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Habbitica.BLL_DAL.Infrastructure;
using System.Text.RegularExpressions;

namespace Habbitica.BLL_DAL.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork DataBase { get; set; }
        public PostService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }
        public void AddPost(EditToDoDTO postDTO)
        {
            ToDo task = new ToDo();

            task.ApplicationUser = DataBase.Accounts.GetByName(postDTO.AuthorUsername).Result;
            DataBase.ToDos.Create(task);
            DataBase.Save();
        }
        
    }
}
