using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public interface IReviewerRepository
    {
        IEnumerable<Reviewer> GetReviewersADO();
        IEnumerable<Reviewer> GetReviewersDapper();
    }
}