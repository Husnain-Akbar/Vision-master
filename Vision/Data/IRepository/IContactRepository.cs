using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Models;

namespace Vision.Data.IRepository
{
    public interface IContactRepository:IRepository<Contact>
    {
        void Update(Contact contact);

    }
}
