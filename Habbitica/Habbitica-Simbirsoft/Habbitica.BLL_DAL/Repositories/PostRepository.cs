using Maganizer_Project.DAL.EF;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maganizer_Project.DAL.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private MaganizerContext db;

        public PostRepository(MaganizerContext context)
        {
            this.db = context;
        }
        public void Create(Post item)
        {
            var tags = new List<Tag>();
            for(int i = 0; i < item.Tags.Count(); i++)
            {
                var tag_in_db = db.Tags.Where(x => x.Name == item.Tags[i].Name).FirstOrDefault();
                if(tag_in_db != null)
                {
                    tags.Add(tag_in_db);
                }
                else 
                {
                    tags.Add(item.Tags[i]);
                }
            }
            item.Tags = tags;

            db.Posts.Add(item);
        }

        public void Delete(int id)
        {
            Post item = db.Posts.Find(id);
            if (item != null)
            {
                db.Posts.Remove(item);
            }
        }

        public IEnumerable<Post> Find(Func<Post, bool> predicate)
        {
            return db.Posts.Include(o => o.Tags).Include(o => o.ApplicationUser).Where(predicate).ToList();
        }

        public Post Get(int id)
        {
            return db.Posts.Include(x => x.ApplicationUser).ToList().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts.Include(x => x.Tags).Include(o => o.ApplicationUser);
        }

        public void Update(Post item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
