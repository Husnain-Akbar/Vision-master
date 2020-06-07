using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class ContactRepository: Repository<Contact>, IContactRepository
    {
        private readonly ApplicationDbContext db;
        public ContactRepository(ApplicationDbContext db) :base(db)
        {
            this.db = db;
                
        }     


    }
}
