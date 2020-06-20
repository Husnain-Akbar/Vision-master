using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vision.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<WebImages> WebImagesList { get; set; }


        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<Service> ServiceList { get; set; }

        public IEnumerable<Team> TeamList { get; set; }

        public IEnumerable<Feedback> FeedbackList { get; set; }
        public Contact Contact { get; set; }



    }
}
