using Microsoft.AspNetCore.Mvc;
using TechReads.BusinessLogic;
using TechReads.Library.Models;
using TechReads.Web.Models;

namespace TechReads.Web.Controllers
{
    public class ReviewerController : Controller
    {
        private readonly IReviewerManager _reviewerManager;
        public ReviewerController(IReviewerManager manager)
        {
            _reviewerManager = manager;
        }

        // GET: ReviewerController
        public ActionResult Index()
        {
            var model = new ReviewerListModel { Reviewers = _reviewerManager.GetReviewers() };
            return View("Index", model);
        }

        // GET: ReviewerController/Add
        public ActionResult Add()
        {
            var model = new ReviewerModel { Reviewer = new Reviewer() };
            return View("Add", model);
        }


        // GET: ReviewerController/Edit/5
        public ActionResult Edit(int id)
        {
            var reviewer = _reviewerManager.GetReviewerById(id);

            if (reviewer == null)
            {
                return RedirectToAction("Index");
            }

            var model = new ReviewerModel {  Reviewer = reviewer };
            return View("Edit", model);
        }

        // POST: ReviewerController/Edit/5
        [HttpPost]
        public ActionResult Save([FromForm]Reviewer reviewer)
        {
            _reviewerManager.UpSert(reviewer);
            return RedirectToAction("Index", "Reviewer");
        }

        // GET: ReviewerController/Delete/5
        public ActionResult Delete(int id)
        {
            _reviewerManager.DeleteById(id);
            return RedirectToAction("Index", "Reviewer");
        }

    }
}
