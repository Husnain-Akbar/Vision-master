using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vision.Models
{
    public class NewsLetter
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
    }

}
