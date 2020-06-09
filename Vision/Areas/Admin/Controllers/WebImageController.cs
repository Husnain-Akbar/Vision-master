using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Vision.Data;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Areas.Admin.Controllers
{
    //[Authorize]
    [Area("Admin")]
    public class WebImageController : Controller
    {
        //private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public WebImageController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            WebImages team = new WebImages();
            if (id == null)
            {
                return View(team);
            }
            team = _unitOfWork.WebImageRepository.Get(id.GetValueOrDefault());
            if (team == null)
            {
                return NotFound();
            }
            return View(team);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(WebImages web)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (web.Id == 0)
                {
                    //New Service
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\webimages");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    web.Picture = @"\images\webimages\" + fileName + extension;

                    _unitOfWork.WebImageRepository.Add(web);
                }
                else
                {
                    //Edit Service
                    var teamdb = _unitOfWork.WebImageRepository.Get(web.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\webimages");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, teamdb.Picture.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        web.Picture = @"\images\webimages\" + fileName + extension_new;
                    }
                    else
                    {
                        web.Picture = teamdb.Picture;
                    }

                    _unitOfWork.WebImageRepository.Update(web);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(web);
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.WebImageRepository.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, serviceFromDb.Picture.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (serviceFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.WebImageRepository.Remove(serviceFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {

            return Json(new { data = _unitOfWork.WebImageRepository.GetAll() });
        }

        

        #endregion
    }
}