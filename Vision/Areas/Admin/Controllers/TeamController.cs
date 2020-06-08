using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Vision.Data.IRepository;
using Vision.Models;

namespace Vision.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public TeamController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
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
            Team team = new Team();
            if (id == null)
            {
                return View(team);
            }
            team = _unitOfWork.Teams.Get(id.GetValueOrDefault());
            if (team == null)
            {
                return NotFound();
            }
            return View(team);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert( Team team)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (team.Id == 0)
                {
                    //New Service
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\team");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    team.ImageUrl = @"\images\team\" + fileName + extension;

                    _unitOfWork.Teams.Add(team);
                }
                else
                {
                    //Edit Service
                    var teamdb = _unitOfWork.Service.Get(team.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\team");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, teamdb.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        team.ImageUrl = @"\images\team\" + fileName + extension_new;
                    }
                    else
                    {
                        team.ImageUrl = teamdb.ImageUrl;
                    }

                    _unitOfWork.Teams.Update(team);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(team);
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.Teams.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, serviceFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (serviceFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Teams.Remove(serviceFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region API Calls
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Teams.GetAll() });
        }



        #endregion


    }
}