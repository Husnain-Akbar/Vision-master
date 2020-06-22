using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public decimal Price { get; set; }

        public decimal Price50 { get; set; }
        public ICollection<Quote> Quotes { get; set; 
        }
    }
}
