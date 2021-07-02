using Maganizer_Project.BLL.DTO;
using Maganizer_Project.BLL.Interfaces;
using Maganizer_Project.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Maganizer_Project.DAL.Entities;
using System.Threading.Tasks;

namespace Maganizer_Project.BLL.Services
{
    public class CommentService: ICommentService
    {
        private IUnitOfWork DataBase { get; set; }
        public CommentService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public IEnumerable<CommentDTO> GetCommentsOfPost(int postId)
        {           
            var comments = DataBase.Comments.GetAll().Where(x => x.PostId == postId);
            List<CommentDTO> commentsDTO = new List<CommentDTO>();

            var profiles = DataBase.UserProfiles.GetAll();

            foreach (var x in comments)
            {        
                commentsDTO.Add(new CommentDTO
                {
                    Content = x.Content,
                    AuthorName = x.ApplicationUser.UserName,
                    PostDate = x.DateOfCreation,
                });
            }       
            
            for(int i=0; i<commentsDTO.Count(); i++) 
            {
                commentsDTO[i].AuthorAvatar = profiles.FirstOrDefault(x => x.ApplicationUser.UserName == commentsDTO[i].AuthorName).Avatar;
            }

            return commentsDTO;


        }

        public async Task AddComment(CommentDTO commentDTO)
        {
            var user = await DataBase.Accounts.GetByName(commentDTO.AuthorName);
            var comment = new Comment()
            {
                PostId = commentDTO.PostId,
                Content = commentDTO.Content,
                DateOfCreation = commentDTO.PostDate,
                ApplicationUserId = user.Id
            };

            DataBase.Comments.Create(comment);
            DataBase.Save();
        }
    }
}
