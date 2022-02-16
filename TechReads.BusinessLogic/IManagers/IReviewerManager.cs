using TechReads.Library.Models;

namespace TechReads.BusinessLogic
{
    public interface IReviewerManager
    {
        void DeleteById(int id);
        Reviewer? GetReviewerById(int id);
        IEnumerable<Reviewer> GetReviewers();
        void UpSert(Reviewer reviewer);
    }
}