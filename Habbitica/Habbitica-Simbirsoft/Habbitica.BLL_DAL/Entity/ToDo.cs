using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Habbitica.BLL_DAL.Entity
{
    public class ToDo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
