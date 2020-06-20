using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vision.Data;
using Vision.Models.ViewModels;

namespace Vision.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SearchController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpPost]

        public IActionResult Search(TravelVM travel)
        {
            return RedirectToAction("Results", new
            {
                Arrival = travel.Arrival,
                Depart = travel.Depart

            });
        }

        public IActionResult Results(string Arrival, string Depart)
        {
            var books = _db.Books.Where(book => book.DepartFrom.ToLower().Contains(Depart)
            || book.ArriveTo.ToLower().Contains(Arrival));

            TravelVM newTravel = new TravelVM
            {
                Book = books
            };
            if (newTravel == null)
            {
                return NotFound();
            }

            return View(newTravel);

        }
    }
}