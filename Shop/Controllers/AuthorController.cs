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
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthorController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult CreateAuthor()
        {
            return View(new Author());
        }
        [HttpPost]
        public IActionResult CreateAuthor(Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();
            return RedirectToAction(nameof(CreateAuthor));
        }
        
        [HttpGet]
        public IActionResult AssignAuthorToBook()
        {
            var books = context.Books.ToList();
            var bookComboBox = new SelectList(books, "Id", "Name");
            var authors = context.Authors.ToList();
            var authorComboBox = new SelectList(authors, "Id", "Name");
            ViewBag.Authors = authorComboBox;
            ViewBag.Books = bookComboBox;
            return View(new BookAuthor());
        }
        [HttpPost]
        public async Task<IActionResult> AssignAuthorToBook(BookAuthor model)
        {
            await context.BookAuthors.AddAsync(model);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(AssignAuthorToBook));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthorBooks()
        {
            var authorBooks = context.Books
                .Include(i => i.BookAuthors)
                .ThenInclude(i => i.Author)
                .ToList();
            var data = context.Books.Include(i => i.BookAuthors).ThenInclude(i => i.Author).Where(i => i.BookAuthors.Any(i => i.AuthorId == 1));
            var books = (from b in context.Books
                        join ba in context.BookAuthors
                        on b.Id equals ba.BookId
                        join a in context.Authors
                        on ba.AuthorId equals a.Id
                        where a.Id == 1
                        select new
                        {
                            b
                        }).ToList();

            return View();
        }
    }
}
