using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {
        private ApplicationDbContext db;

        public FrequencyRepository(ApplicationDbContext db) :base(db)
        {
            this.db = db;
        }
    }
}
