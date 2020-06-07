using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class ServiceRepository: Repository<Service>, IServiceRepository
    {
        private ApplicationDbContext db;

        public ServiceRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }

        public void Update(Service service)
        {
            var objFromDb = db.Service.FirstOrDefault(s => s.Id == service.Id);

            objFromDb.Name = service.Name;
            objFromDb.LongDesc = service.LongDesc;
            objFromDb.Price = service.Price;
            objFromDb.ImageUrl = service.ImageUrl;
            objFromDb.FrequencyId = service.FrequencyId;
            objFromDb.CategoryId = service.CategoryId;

            db.SaveChanges();

        }

    }
}
