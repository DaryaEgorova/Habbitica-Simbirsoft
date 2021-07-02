using Maganizer_Project.DAL.EF;
using Maganizer_Project.DAL.Entities;
using Maganizer_Project.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Maganizer_Project.DAL.Repositories
{
    //public class CategoryRepository : IRepository<Category>
    //{
    //    private MaganizerContext db;
    //    public CategoryRepository(MaganizerContext context)
    //    {
    //        this.db = context;
    //    }
    //    public void Create(Category item)
    //    {
    //        db.Categories.Add(item);
    //    }

    //    public void Delete(Guid id)
    //    {
    //        Category item = db.Categories.Find(id);
    //        if (item != null)
    //        {
    //            db.Categories.Remove(item);
    //        }
    //    }

    //    public IEnumerable<Category> Find(Func<Category, bool> predicate)
    //    {
    //        return db.Categories.Where(predicate).ToList();
    //    }

    //    public Category Get(Guid id)
    //    {
    //        return db.Categories.Find(id);
    //    }

    //    public IEnumerable<Category> GetAll()
    //    {
    //        return db.Categories;
    //    }

    //    public void Update(Category item)
    //    {
    //        db.Entry(item).State = EntityState.Modified;
    //    }
    //}
}
