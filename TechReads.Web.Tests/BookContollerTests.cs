using System.Linq;
using Xunit;
using TechReads.Library.Data;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using TechReads.Web.Models;
using TechReads.Library.Models;
using System;

namespace TechReads.Web.Tests
{
    public class BookContollerTests
    {
        const int VALID_ID = 1;
        const int INVALID_ID = 9999999;
        readonly Book[] exampleBooks = new Book[] {
                new Book { Title = "test1", Authors = "auth1", BookId = VALID_ID },
                new Book { Title = "test2", Authors = "auth2", BookId = 2 },
                new Book { Title = "test3", Authors = "auth3", BookId = 3 },
                new Book { Title = "test4", Authors = "auth4", BookId = 4 },
                new Book { Title = "test5", Authors = "auth5", BookId = 5 },
                new Book { Title = "test6", Authors = "auth6", BookId = 6 },
                new Book { Title = "test7", Authors = "auth7", BookId = 7 },
                new Book { Title = "test8", Authors = "auth8", BookId = 8 },
                new Book { Title = "test9", Authors = "auth9", BookId = 9 },
                new Book { Title = "test0", Authors = "auth0", BookId = 10 }
        };
        
        private (TechReadsContext, BookController) GetDbContextAndController()
        {
            var options = new DbContextOptionsBuilder<TechReadsContext>()
                .UseInMemoryDatabase(databaseName: "TestBookList-" + Guid.NewGuid())
                .Options;

            var db = new TechReadsContext(options);
            db.Books.AddRange(exampleBooks);
            db.SaveChanges();

            return (db, new BookController(db));
        }

        [Fact]
        public void BookController_ReturnsIndexViewWithBooks()
        {
            var (_, controller) = GetDbContextAndController();

            var result = controller.Index() as ViewResult;

            result.ViewName.ShouldBe("Index");
            result.Model.ShouldBeOfType<BookListModel>();
            (result.Model as BookListModel).Books.ShouldBeEquivalentTo(exampleBooks);
        }

        [Fact]
        public void BookController_ReturnsEditFormForValidID()
        {
            var (_, controller) = GetDbContextAndController();

            var result = controller.Edit(VALID_ID) as ViewResult;

            result.ViewName.ShouldBe("Edit");
            result.Model.ShouldBeOfType<BookModel>();
            (result.Model as BookModel).Book.ShouldBeEquivalentTo(exampleBooks.First(b => b.BookId == VALID_ID));
        }

        [Fact]
        public void BookController_EditFormCanUpdateValidEntry()
        {
            var (db, controller) = GetDbContextAndController();

            var editBook = db.Books.First(b => b.BookId == VALID_ID);
            editBook.Title = "new title";

            var result = controller.Edit(editBook) as RedirectToActionResult;
            
            result.ControllerName.ShouldBe("Book");
            result.ActionName.ShouldBe("Index");

            db.Books.First(b => b.BookId == VALID_ID).ShouldBe(editBook);
        }
    }
}
