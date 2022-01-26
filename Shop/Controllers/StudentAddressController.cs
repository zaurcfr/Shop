using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.DB;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class StudentAddressController : Controller
    {
        private readonly ApplicationDbContext context;

        public StudentAddressController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult CreateStudentAddress()
        {
            var students = context.Students.ToList();
            var studentComboBox = new SelectList(students, "Id", "Name");
            ViewBag.Students = studentComboBox;
            return View(new StudentAddress());
        }
        [HttpPost]
        public IActionResult CreateStudentAddress(StudentAddress studentAddress)
        {
            context.StudentAddresses.Add(studentAddress);
            context.SaveChanges();
            return RedirectToAction(nameof(CreateStudentAddress));
        }

        public IActionResult GetAllStudentAddresses()
        {
            var studentAddresses = context.StudentAddresses.Include(i => i.Student).ToList();
            return View(studentAddresses);
        }

        public IActionResult DeleteStudentAddress(int id)
        {
            var studentAddress = context.StudentAddresses.FirstOrDefault(i => i.Id == id);
            context.StudentAddresses.Remove(studentAddress);
            context.SaveChanges();
            return View(nameof(GetAllStudentAddresses));
        }
    }
}
