using TechReads.BusinessLogic.Filters;
using TechReads.Library.Data;
using TechReads.Library.Models;
using static TechReads.BusinessLogic.Filters.ReviewOrdering;

namespace TechReads.BusinessLogic
{
    public class ReviewManager : IReviewManager
    {
        private readonly TechReadsContext _context;
        public ReviewManager(TechReadsContext context)
        {
            _context = context;
            EnsureDatabaseSetupCompleted(_context);
        }
        private static void EnsureDatabaseSetupCompleted(TechReadsContext context)
        {
            context.Database.EnsureCreated();
            SeedData.Initialize(context);
        }

        public void DeleteById(int id)
        {
            var review = _context.Reviews.First(r => r.ReviewId == id);
            _context.Remove(review);
            _context.SaveChanges();
        }

        public Review? GetReviewById(int id)
        {
            return _context.Reviews.FirstOrDefault(r => r.ReviewId == id);
        }

        public IEnumerable<Review> GetReviewsForBook(Book book, ReviewOrdering? ordering, ReviewFilter? filter)
        {
            var reviews = _context.Reviews.Where(r => r.BookId == book.BookId);
            //reviews = from review in _context.Reviews
            //          where review.BookId == book.BookId
            //          select review;

            if(filter != null)
            {
                reviews = reviews.Where(r => r.Stars >= filter.MinRating && r.Stars <= filter.MaxRating);
            } 

            switch (ordering)
            {
                case RatingAsc: 
                    reviews = reviews.OrderBy(r => r.Stars);
                    break;
                case RatingDesc:
                    reviews = reviews.OrderByDescending(r => r.Stars);
                    break;
                case Username:
                    reviews = reviews.OrderBy(r => _context.Reviewers.First(viewer => viewer.ReviewerId == r.ReviewerId).DisplayName);
                    break;
                case None:
                default:
                    // None or null == unsorted
                    break;
            }

            return reviews;
        }

        public void UpSert(Review review)
        {
            if(DupedReview(review))
            {
                return;
            }

            _context.Update(review);
            _context.SaveChanges();
        }

        private bool DupedReview(Review review) => _context.Reviews.Where(r =>
                r.ReviewId != review.ReviewId
                && r.ReviewerId == review.ReviewerId
                && r.BookId == review.BookId)
                .Any();
    }
}
