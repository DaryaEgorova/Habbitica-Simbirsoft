using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Maganizer_Project.BLL.Infrastructure;
using System.Text.RegularExpressions;

namespace Maganizer_Project.BLL.Services
{
    public class PostService : IPostService
    {
        private IUnitOfWork DataBase { get; set; }
        public PostService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }
        public void AddPost(EditPostDTO postDTO)
        {
            var tags = postDTO.Tags.Split().Distinct();

            Post post = new Post()
            {
                Name = postDTO.Name,
                Content = postDTO.Content,
                DateOfCreation = DateTime.Now,
                Tags = new List<Tag>(tags.Count()),
                FeaturedImage = ImageConvertion.ConvertToByteArray(postDTO.FeaturedImage)                
            };

            foreach(var x in tags)
            {
                Tag tag = new Tag()
                {
                    Name = x,
                    DateOfCreation = DateTime.Now
                };

                post.Tags.Add(tag);
            }

            post.ApplicationUser = DataBase.Accounts.GetByName(postDTO.AuthorUsername).Result;
            DataBase.Posts.Create(post);
            DataBase.Save();
        }

        public GetPostDTO GetPostByName(string name)
        {
            var post = DataBase.Posts.Find(x => x.Name == name).FirstOrDefault();

            if (post == null)
            {
                return null;
            }

            var nextPost = DataBase.Posts.Find(x => x.Id == post.Id + 1).FirstOrDefault();
            var previousPost = DataBase.Posts.Find(x => x.Id == post.Id - 1).FirstOrDefault();

            string nextPostName = null;
            string previousPostName = null;

            if (nextPost != null)
            {
                nextPostName = nextPost.Name;
            }

            if(previousPost != null)
            {
                previousPostName = previousPost.Name;
            }     

            GetPostDTO postDTO = new GetPostDTO()
            {
                Id = post.Id,
                Name = post.Name.Replace("-", " "),
                Content = post.Content,
                DateOfCreation = post.DateOfCreation,
                FeaturedImage = post.FeaturedImage,
                NextPostName = nextPostName,
                PreviousPostName = previousPostName,
                AuthorName = post.ApplicationUser.UserName
            };

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tag, TagDTO>()).CreateMapper();
            postDTO.Tags = mapper.Map<List<Tag>, List<TagDTO>>(post.Tags);

            return postDTO;
        }

        public IEnumerable<GetPostDTO> GetPosts()
        {
            var posts = DataBase.Posts.GetAll();

            if(posts != null) 
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tag, TagDTO>()).CreateMapper();
                List<GetPostDTO> postDTOs = new List<GetPostDTO>();
                foreach(var x in posts) 
                {
                    postDTOs.Add(new GetPostDTO()
                    {
                        Name = x.Name,
                        Tags = mapper.Map<IEnumerable<Tag>, List<TagDTO>>(x.Tags),
                        DateOfCreation = x.DateOfCreation,
                        FeaturedImage = x.FeaturedImage,
                        AuthorName = x.ApplicationUser.UserName
                    });
                }

                return postDTOs;
            }

            return null;
        }
    }
}
