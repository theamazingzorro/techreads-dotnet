using System.Collections.Generic;
using TechReads.BusinessLogic.Filters;
using TechReads.Library.Models;

namespace TechReads.Web.Models
{
    public class BookDetailsModel
    {
        public double AverageStars { get; init; }
        public Book Book { get; init; }
        public IEnumerable<Review> Reviews { get; init; }
        public IEnumerable<Reviewer> Reviewers { get; init; }

        public ReviewOrdering Ordering { get; set; }
        public ReviewFilter Filter { get; init; }
    }
}
