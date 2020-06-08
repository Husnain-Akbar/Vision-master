using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Models;

namespace Vision.Data.IRepository
{
    public interface IFeedbackRepository:IRepository<Feedback>
    {
        void Update(Feedback feedback);

    }
}
