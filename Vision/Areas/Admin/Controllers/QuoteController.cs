using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vision.Data;
using Vision.Utility;

namespace Vision.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

    public class QuoteController : Controller
    {
        private readonly ApplicationDbContext _db;

        public QuoteController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var quotes= _db.Quotes.Include(b => b.Book).ToList();
            return View(quotes);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetter = await _db.Quotes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsLetter == null)
            {
                return NotFound();
            }

            return View(newsLetter);
        }

        // POST: Admin/NewsLetter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsLetter = await _db.Quotes.FindAsync(id);
            _db.Quotes.Remove(newsLetter);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}