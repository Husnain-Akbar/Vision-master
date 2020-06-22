using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Vision.Data.IRepository;
using Vision.Models;
using Vision.Utility;

namespace Vision.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

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
                    _unitOfWork.Teams.Add(team);
                }
                else
                {
                    //Edit Service
                    var teamdb = _unitOfWork.Teams.Get(team.Id);
                    _unitOfWork.Teams.Update(team);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            
            
            
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var serviceFromDb = _unitOfWork.Teams.Get(id);

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