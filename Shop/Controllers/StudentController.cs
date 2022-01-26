using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.DB;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext context;

        public StudentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            return View(new Student());
        }
        public IActionResult CreateStudent(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
            return RedirectToAction(nameof(CreateStudent));
        }

        public IActionResult GetAllStudents()
        {
            var students = context.Students.Include(i => i.StudentAddress).ToList();
            return View(students);
        }

        public IActionResult DeleteStudent(int id)
        {
            var student = context.Students.FirstOrDefault(i => i.Id == id);
            context.Students.Remove(student);
            context.SaveChanges();
            return RedirectToAction(nameof(GetAllStudents));
        }

        public IActionResult EditStudent(int id)
        {
            var student = context.Students.FirstOrDefault(i => i.Id == id);
            return View(student);
        }

        public IActionResult EditStudent(Student student)
        {
            var currentStudent = context.Students.FirstOrDefault(i => i.Id == student.Id);
            context.Students.Remove(currentStudent);
            context.Students.Add(student);
            context.SaveChanges();
            return RedirectToAction(nameof(GetAllStudents));
        }
        
    }
}
