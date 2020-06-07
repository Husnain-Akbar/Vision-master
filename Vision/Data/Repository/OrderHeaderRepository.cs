using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Data.Repository
{
    public class OrderHeaderRepository: Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext db;

        public OrderHeaderRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
    }
}
