using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vision.Data.IRepository;
using Vision.Models;
using Vision.Utility;

namespace Vision.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ContactController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Contact form = new Contact();
            if (id == null)
            {
                return View(form);
            }
            form = _unitOfWork.Contacts.Get(id.GetValueOrDefault());
            if (form == null)
            {
                return NotFound();
            }
            return View(form);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Contact form)
        {
            if (ModelState.IsValid)
            {
                if (form.Id == 0)
                {
                    _unitOfWork.Contacts.Add(form);
                }
                else
                {
                    _unitOfWork.Contacts.Update(form);
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
            var objFromDb = _unitOfWork.Contacts.Get(id);
            if (objFromDb == null)
            {
                return NotFound();
            }

            _unitOfWork.Contacts.Remove(objFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }


        #region API CALLS

        [HttpGet]
        [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Contacts.GetAll() });
            //return Json(new { data = _unitOfWork.SP_Call.ReturnList<Category>(SD.usp_GetAllCategory,null)  });
        }



        #endregion
    }
}