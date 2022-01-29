using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shop.DB;
using Shop.Entities;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            //context.Studnts.Add(new Studnt
            //{
            //    Name = "Arif",
            //    Surname = "Bagirli",
            //    Email = "arif@gmail.com",
            //    Password = "arif",
            //    Username = "arifbgrff",
            //    ClassNumber = "3914"
            //});
            //context.SaveChanges();

            //var users = context.Users.ToList();
            //var teachers = context.Teachers.ToList();
            //var studnts = context.Studnts.ToList();
            return View();
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
