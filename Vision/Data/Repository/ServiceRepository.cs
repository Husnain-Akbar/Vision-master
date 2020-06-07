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
    }
}
