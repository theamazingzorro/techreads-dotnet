using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewsADO();
        IEnumerable<Review> GetReviewsDapper();
    }
}