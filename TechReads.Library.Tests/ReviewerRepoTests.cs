using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechReads.Library.Data;
using Xunit;

namespace TechReads.Library.Tests
{
    public class ReviewerRepoTests
    {
        [Fact]
        public void ADOTester()
        {
            var repo = new ReviewerRepository();
            var reviewers = repo.GetReviewersADO();

            Assert.NotNull(reviewers);
        }

        [Fact]
        public void DapperTester()
        {
            var repo = new ReviewerRepository();
            var reviewers = repo.GetReviewersDapper();

            Assert.NotNull(reviewers);
        }
    }
}
