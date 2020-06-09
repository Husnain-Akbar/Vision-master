using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vision.Models.ViewModels
{
    public class FeedbackVM
    {
        public int Id { get; set; }
        public string Comment { get; set; }

    }
    public class FeedbackIndex
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Comment { get; set; }

    }

    public class Feed
    {
        public IEnumerable<FeedbackIndex> Feeds{ get; set; }

    }
}
