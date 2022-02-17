using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechReads.Library.Data;
using TechReads.Library.Models;

namespace TechReads.BusinessLogic
{
    public class ReviewerManager : IReviewerManager
    {
        private readonly TechReadsContext _context;
        private readonly IReviewerRepository _repository;
        public ReviewerManager(TechReadsContext context, IReviewerRepository reviewerRepo)
        {
            _context = context;
            _repository = reviewerRepo;
            EnsureDatabaseSetupCompleted(_context);
        }
        private static void EnsureDatabaseSetupCompleted(TechReadsContext context)
        {
            context.Database.EnsureCreated();
            SeedData.Initialize(context);
        }

        public void DeleteById(int id)
        {
            var reviewer = _context.Reviewers.First(r => r.ReviewerId == id);
            _context.Remove(reviewer);
            _context.SaveChanges();
        }

        public Reviewer? GetReviewerById(int id)
        {
            return _context.Reviewers.FirstOrDefault(r => r.ReviewerId == id);
        }

        public IEnumerable<Reviewer> GetReviewers()
        {
            //return _context.Reviewers.Take(10).ToArray();
            return _repository.GetReviewersADO();
        }

        public void UpSert(Reviewer reviewer)
        {
            _context.Update(reviewer);
            _context.SaveChanges();
        }
    }
}
