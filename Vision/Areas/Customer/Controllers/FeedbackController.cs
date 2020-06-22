using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vision.Data;
using Vision.Data.IRepository;
using Vision.Models;
using Vision.Models.ViewModels;
using Vision.Utility;

namespace Vision.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FeedbackController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _db;

        public FeedbackController(IUnitOfWork unitOfWork,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            

        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

        public IActionResult Index()

        {

            var lisrt = _db.Feedbacks.Include(u => u.ApplicationUser).ToList();

            var an = lisrt.Select(feedback => new FeedbackIndex { 
                Id=feedback.Id,
                Comment=feedback.Comment,
                Name=feedback.ApplicationUser.Name
            });
            var model = new Feed
            {
                Feeds = an
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Upsert(int? id)
        {


            FeedbackVM form = new FeedbackVM();
            
            //if (id == null)
            //{
            //    return View(form);
            //}
            //form = _unitOfWork.Feedbacks.Get(id.GetValueOrDefault());
            //if (form == null)
            //{
            //    return NotFound();
            //}
            return View(form);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(FeedbackVM form)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var user = _unitOfWork.User.GetFirstOrDefault(c => c.Id == claim.Value);

            var formvm = new Feedback
            {
                ApplicationUserId = user.Id,
                Comment=form.Comment
            };

            if (ModelState.IsValid)
            {
                if (form.Id == 0)
                {
                    _db.Feedbacks.Add(formvm);
                }
                else
                {
                    _db.Feedbacks.Update(formvm);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(form);
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Feedbacks.Get(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Feedbacks.Remove(objFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }


        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Feedbacks.GetAll(includeProperties: "ApplicationUser") });
            //return Json(new { data = _unitOfWork.SP_Call.ReturnList<Category>(SD.usp_GetAllCategory,null)  });
        }



        #endregion
    }

}