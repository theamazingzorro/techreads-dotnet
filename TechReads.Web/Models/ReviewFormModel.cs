using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Web.Models
{
    public class ReviewFormModel
    {
        public Review Review { get; init; }
        public Reviewer Reviewer { get; init; }
        public IEnumerable<Book> Books { get; init; }
    }
}
