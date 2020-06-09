using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class WebImagesRepository : Repository<WebImages>, IWebImageRepository
    {
        private readonly ApplicationDbContext db;
        public WebImagesRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public void Update(WebImages web)
        {

            var objFromDb = db.WebImages.FirstOrDefault(s => s.Id == web.Id);

            objFromDb.Name = web.Name;
            objFromDb.Picture = web.Picture;
            db.SaveChanges();

        }
    }
}
