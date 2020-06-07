using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
        {
            return db.Frequency.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }
        public void Update(Frequency frequency)
        {
            var objFromDb = db.Frequency.FirstOrDefault(s => s.Id == frequency.Id);

            objFromDb.Name = frequency.Name;
            objFromDb.FrequencyCount = frequency.FrequencyCount;

            db.SaveChanges();

        }


    }
}
