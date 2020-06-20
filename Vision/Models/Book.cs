using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vision.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string DepartFrom { get; set; }
        public string ArriveTo { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public int QuoteId { get; set; }
        public Quote Quote { get; set; 
        }
    }
}
