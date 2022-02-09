using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TechReads.Web.Models;
using TechReads.Library.Data;
using TechReads.Library.Models;

namespace TechReads.Web
{
    public class BookController: Controller
    { 
        private readonly TechReadsContext _context;
        // constructor
        public BookController(TechReadsContext context)
        {
            _context = context;
           EnsureDatabaseSetupCompleted(_context);
        }

        // controller actions
        [HttpGet]
        public IActionResult Index()
        {
            var books = _context.Books.Take(10).ToArray();
            var model = new BookListModel(books);

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            var model = new BookModel(book);

            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Edit([FromForm]Book book)
        {
            _context.Update(book);
            _context.SaveChanges();

            return RedirectToAction("Index", "Book");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.First(b => b.BookId == id);
            _context.Remove(book);
            _context.SaveChanges();

            return RedirectToAction("Index", "Book");
        }

        private static void EnsureDatabaseSetupCompleted(TechReadsContext context)
        {
            context.Database.EnsureCreated();
            SeedData.Initialize(context);
        }
    }
}