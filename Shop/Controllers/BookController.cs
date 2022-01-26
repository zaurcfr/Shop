using Microsoft.AspNetCore.Mvc;
using Shop.DB;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext context;

        public BookController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult CreateBook()
        {
            return View(new Book());
        }
        [HttpPost]
        public IActionResult CreateBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction(nameof(CreateBook));
        }
    }
}
