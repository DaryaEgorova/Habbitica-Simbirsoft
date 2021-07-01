using System.Collections.Generic;
using Habbitica.BLL_DAL.DTO;

namespace Habbitica.BLL_DAL.Interfaces
{
    public interface IPostService
    {
        void AddPost(EditPostDTO postDTO);
        GetPostDTO GetPostByName(string name);
        IEnumerable<GetPostDTO> GetPosts();

    }
}
