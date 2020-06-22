using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vision.Data;
using Vision.Models;
using Vision.Utility;

namespace Vision.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

    public class NewsLetterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsLetterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/NewsLetter
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsLetters.ToListAsync());
        }

        // GET: Admin/NewsLetter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetter = await _context.NewsLetters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsLetter == null)
            {
                return NotFound();
            }

            return View(newsLetter);
        }

        // GET: Admin/NewsLetter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/NewsLetter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email")] NewsLetter newsLetter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsLetter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsLetter);
        }

        // GET: Admin/NewsLetter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetter = await _context.NewsLetters.FindAsync(id);
            if (newsLetter == null)
            {
                return NotFound();
            }
            return View(newsLetter);
        }

        // POST: Admin/NewsLetter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email")] NewsLetter newsLetter)
        {
            if (id != newsLetter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsLetter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsLetterExists(newsLetter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsLetter);
        }

        // GET: Admin/NewsLetter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsLetter = await _context.NewsLetters
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
            var newsLetter = await _context.NewsLetters.FindAsync(id);
            _context.NewsLetters.Remove(newsLetter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsLetterExists(int id)
        {
            return _context.NewsLetters.Any(e => e.Id == id);
        }
    }
}
