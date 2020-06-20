using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vision.Data;
using Vision.Models;
using Vision.Models.ViewModels;

namespace Vision.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class TravelController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TravelController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            TravelVM travel  = new TravelVM
            {
                Book = _db.Books.ToList()
            };

            return View(travel);
        }

        public IActionResult Quotes(int id)
        {
            var book = _db.Books.Include(b => b.Quote).FirstOrDefault(b =>b.Id == id);

            BookVM bookVM = new BookVM
            {
                Book= book,
                Quote=book.Quote
            };
                return View(bookVM);
            
        }

        [HttpPost]
        public IActionResult Quotes(BookVM book)
        {
            Quote qoute = new Quote
            {
                Name = book.Quote.Name,
                Email = book.Quote.Email,
                Message = book.Quote.Message
            };

            _db.Quotes.Add(qoute);

            Book book1 = new Book();
            book1.QuoteId = book.Quote.Id;
            _db.Update(book1);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}