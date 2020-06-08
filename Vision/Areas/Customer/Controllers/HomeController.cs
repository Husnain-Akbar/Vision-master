﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vision.Models;
using Vision.Data.IRepository;
using Vision.Models.ViewModels;
using Vision.Data;
using Vision.Extension;
using Vision.Utility;
namespace Vision.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private HomeViewModel HomeVM;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            HomeVM = new HomeViewModel()
            {
                //WebImagesList = _db.WebImages.ToList(),
                CategoryList = _unitOfWork.Category.GetAll(),
                ServiceList = _unitOfWork.Service.GetAll(includeProperties: "Frequency"),
                //TeamList =_unitOfWork.Teams.GetAll(),
                //FeedbackList=_unitOfWork.Feedbacks.GetAll()
            };

            return View(HomeVM);
        }

        public IActionResult Details(int id)
        {
            var serviceFromDb = _unitOfWork.Service.
                GetFirstOrDefault(includeProperties: "Category,Frequency", filter: c => c.Id == id);
            return View(serviceFromDb);
        }

        public IActionResult AddToCart(int serviceId)
        {
            List<int> sessionList = new List<int>();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
            {
                sessionList.Add(serviceId);
                HttpContext.Session.SetObject(SD.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                if (!sessionList.Contains(serviceId))
                {
                    sessionList.Add(serviceId);
                    HttpContext.Session.SetObject(SD.SessionCart, sessionList);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
