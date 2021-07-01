using System;
using System.Collections.Generic;
using System.Text;

namespace Habbitica.BLL_DAL.DTO
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string NextPostName { get; set; }
        public string PreviousPostName { get; set; }
    }
}
