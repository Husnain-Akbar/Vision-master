using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vision.Models;

namespace Vision.Data.IRepository
{
    public interface IFrequencyRepository:IRepository<Frequency>
    {
        IEnumerable<SelectListItem> GetFrequencyListForDropDown();
        void Update(Frequency frequency);


    }
}
