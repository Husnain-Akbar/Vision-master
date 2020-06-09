using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vision.Data.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        ICategoryRepository Category { get; }
        IFrequencyRepository Frequency { get; }
        IServiceRepository Service { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }

        ITeamRepository Teams { get;  }
        IFeedbackRepository Feedbacks { get;  }
        IContactRepository Contacts { get;  }
         IUserRepository User { get; }
        IWebImageRepository WebImageRepository { get; }
        void Save();
    }
}
