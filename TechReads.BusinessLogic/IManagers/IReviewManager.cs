using TechReads.BusinessLogic.Filters;
using TechReads.Library.Models;

namespace TechReads.BusinessLogic
{
    public interface IReviewManager
    {
        void DeleteById(int id);
        Review? GetReviewById(int id);
        IEnumerable<Review> GetReviewsForBook(Book book, ReviewOrdering? order, ReviewFilter? filter);
        void UpSert(Review review);
    }
}