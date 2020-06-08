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
        public void Update(Contact contact)
        {
            var date = DateTime.Now;
            var objFromDb = db.Contacts.FirstOrDefault(s => s.Id == contact.Id);

            objFromDb.Name = contact.Name;
            objFromDb.Email = contact.Email;
            objFromDb.Message = contact.Message;
            objFromDb.DateTime = date;
            db.SaveChanges();

        }

    }
}
