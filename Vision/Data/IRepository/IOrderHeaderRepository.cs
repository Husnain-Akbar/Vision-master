using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Models;

namespace Vision.Data.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void ChangeOrderStatus(int orderHeaderId, string status);
    }
}
