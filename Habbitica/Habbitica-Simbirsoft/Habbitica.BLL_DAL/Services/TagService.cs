using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.DAL.Interfaces;
using Maganizer_Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Maganizer_Project.BLL.Services
{
    public class TagService : ITagService
    {
        private IUnitOfWork DataBase { get; set; }

        public TagService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public IEnumerable<TagDTO> GetTags()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Tag, TagDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Tag>, List<TagDTO>>(DataBase.Tags.GetAll());
        }

        public TagArchiveDTO GetTag(string name)
        {
            var tags = DataBase.Tags.Find(x => x.Name == name);

            if(tags != null) 
            {
                var tag = tags.FirstOrDefault();

                TagArchiveDTO tagArchiveDTO = new TagArchiveDTO()
                {
                    Name = tag.Name,
                    Posts = new List<PostOfTagDTO>()
                };

                foreach(var x in tag.Posts) 
                {
                    var AuthorName = DataBase.Posts.Get(x.Id).ApplicationUser.UserName;
                    tagArchiveDTO.Posts.Add(new PostOfTagDTO()
                    {
                        PostName = x.Name,
                        FirstTagName = x.Tags.FirstOrDefault().Name,
                        PostedOn = x.DateOfCreation,
                        FeaturedPostImage = x.FeaturedImage,
                        AuthorName = AuthorName
                    });

                    
                }

                return tagArchiveDTO;
            }

            return null;
        }

        public void DeleteTag(string name)
        {
            var tag = DataBase.Tags.Find(x => x.Name == name).FirstOrDefault();
            DataBase.Tags.Delete(tag.Id);
            DataBase.Save();
        }
    }
}
