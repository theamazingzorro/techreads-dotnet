using Microsoft.AspNetCore.Mvc;
using TechReads.Web.Models;
using TechReads.Library.Models;
using TechReads.BusinessLogic;
using TechReads.BusinessLogic.Filters;

namespace TechReads.Web
{
    public class BookController: Controller
    { 
        private readonly IBookManager _bookManager;
        private readonly IReviewManager _reviewManager;
        private readonly IReviewerManager _reviewerManager;

        public BookController(IBookManager bookManager, IReviewManager reviewManager, IReviewerManager reviewerManager)
        {
            _bookManager = bookManager;
            _reviewManager = reviewManager;
            _reviewerManager = reviewerManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new BookListModel(_bookManager.GetBooks());
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Details(int id, ReviewOrdering ordering, ReviewFilter filter)
        {
            var book = _bookManager.GetBookById(id);
            var model = new BookDetailsModel 
            {
                Book = book,
                Reviews = _reviewManager.GetReviewsForBook(book, ordering, filter),
                Reviewers = _reviewerManager.GetReviewers(),
                AverageStars = _bookManager.GetAverageStarsById(id),
                Ordering = ordering,
                Filter = filter
            };
            return View("Details", model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new BookModel(new Book());
            return View("Add", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _bookManager.GetBookById(id);

            if(book == null)
            {
                return RedirectToAction("Index");
            }

            var model = new BookModel(book);
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Save([FromForm]Book book)
        {
            _bookManager.UpSert(book);
            return RedirectToAction("Index", "Book");
        }

        [HttpGet]
        public IActionResult Delet(int id)
        {
            _bookManager.DeleteById(id);
            return RedirectToAction("Index", "Book");
        }
    }
}