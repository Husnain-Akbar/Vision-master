using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private ApplicationDbContext db;

        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return db.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var objFromDb = db.Category.FirstOrDefault(s => s.Id == category.Id);

            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder;

            db.SaveChanges();

        }
    }
}
