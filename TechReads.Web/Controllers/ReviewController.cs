using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TechReads.BusinessLogic;
using TechReads.Library.Models;
using TechReads.Web.Models;

namespace TechReads.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewerManager _reviewerManager;
        private readonly IReviewManager _reviewManager;
        private readonly IBookManager _bookManager;
        public ReviewController(IReviewerManager reviewerManager, IReviewManager reviewManager, IBookManager bookManager)
        {
            _reviewerManager = reviewerManager;
            _reviewManager = reviewManager;
            _bookManager = bookManager;
        }

        // GET: ReviewController/Add
        public ActionResult Add(int id)
        {
            var model = new ReviewFormModel
            {
                Review = new Review { ReviewerId = id },
                Reviewer = _reviewerManager.GetReviewerById(id),
                Books = _bookManager.GetBooks()
            };

            return View("Add", model);
        }

        // POST: ReviewController/Save
        [HttpPost]
        public ActionResult Save([FromForm]Review review)
        {
            _reviewManager.UpSert(review);
            return RedirectToAction("Details", "Book", new { id = review.BookId });
        }

        // GET: ReviewController/Edit/5
        public ActionResult Edit(int id)
        {
            var review = _reviewManager.GetReviewById(id);
            var model = new ReviewFormModel
            {
                Review = review,
                Reviewer = _reviewerManager.GetReviewerById(review.ReviewerId),
                Books = _bookManager.GetBooks()
            };

            return View("Edit", model);
        }

        // GET: ReviewController/Delete/5
        public ActionResult Delete(int id)
        {
            _reviewManager.DeleteById(id);
            return RedirectToAction("Index", "Book");
        }
    }
}
