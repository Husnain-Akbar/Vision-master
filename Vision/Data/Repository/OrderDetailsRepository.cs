using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class OrderDetailsRepository: Repository<OrderDetails>, IOrderDetailsRepository
    {
        private ApplicationDbContext db;

        public OrderDetailsRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
    }
}
