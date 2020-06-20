using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Vision.Data;
using Vision.Models;

namespace Vision.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _db;
        public BookController(IWebHostEnvironment hostEnvironment,ApplicationDbContext db)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var book =_db.Books.ToList();
            return View(book);
        }

        public IActionResult Upsert(int? id)
        {
            Book book = new Book();
            if (id == null)
            {
                return View(book);
            }
            book = _db.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Book book)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (book.Id == 0)
                {
                    //New Service
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\team");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    book.Image = @"\images\team\" + fileName + extension;

                    _db.Books.Add(book);
                }
                else
                {
                    //Edit Service
                    var teamdb = _db.Books.FirstOrDefault(c => c.Id == book.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\team");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, teamdb.Image
                            .TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, 
                            fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        book.Image = @"\images\team\" + fileName + extension_new;
                    }
                    else
                    {
                        book.Image = teamdb.Image;
                    }

                    _db.Books.Update(book);
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(book);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _db.Books.FirstOrDefault(c=>c.Id==id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, serviceFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (serviceFromDb == null)
            {
                return NotFound();
            }

            _db.Books.Remove(serviceFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}