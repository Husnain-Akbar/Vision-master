using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vision.Models.ViewModels
{
    public class TravelVM
    {
        public IEnumerable<Book> Book { get; set; }
        [Required]
        public string Depart { get; set; }
        [Required]
        public string Arrival { get; set; }
        public string Class { get; set; }
        [Required]

        public Contact Contact { get; set; }

        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Travel")]

        public string NewsLetter { get; set; }
    }
}
