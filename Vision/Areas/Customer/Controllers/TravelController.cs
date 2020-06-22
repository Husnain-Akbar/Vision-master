using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost]
        public IActionResult Index(TravelVM travelVM)
        {
            if (travelVM.Contact.Name!=null && travelVM.Contact.Email!=null&& 
                travelVM.Contact.Message!=null)
            {
                var contact = new Contact()
                {
                    Name = travelVM.Contact.Name,
                    Email = travelVM.Contact.Email,
                    Message = travelVM.Contact.Message

                };


                _db.Add(contact);

                _db.SaveChanges();

            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
           var book=  _db.Books.FirstOrDefault(b => b.Id == id);
            return View(book);
        }
        public IActionResult Quotes(int id)
        {
            var book = _db.Books.Include(b => b.Quotes).FirstOrDefault(b =>b.Id == id);

            var bookVM = new BookVM
            {
                Book=book,
                Quote= new Models.Quote()
            };

           return View(bookVM);
            
        }

        [HttpPost]
        public IActionResult Quotes(BookVM bookVM)
        {
            Quote qoute = new Quote
            {
               Name=bookVM.Quote.Name,
               Email=bookVM.Quote.Email,
               Message=bookVM.Quote.Message,
               BookId=bookVM.Book.Id
            };

            _db.Quotes.Add(qoute);
            _db.SaveChanges();
            

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult NewsLetter(TravelVM travelVM)
        {
            if (travelVM.NewsLetter!=null)
            {
                var newsletter = new NewsLetter()
                {
                    Email=travelVM.NewsLetter
                };

                _db.Add(newsletter);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _db.NewsLetters.FindAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use.");
            }

        }

    }
}