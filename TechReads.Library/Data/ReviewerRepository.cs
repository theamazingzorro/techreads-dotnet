using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public class ReviewerRepository : IReviewerRepository
    {
        private string connString = "Server=(localdb)\\mssqllocaldb;Database=TechReadsDB_MVC;Trusted_Connection=True;MultipleActiveResultSets=true";

        public IEnumerable<Reviewer> GetReviewersADO()
        {
            var sql = @" SELECT * FROM Reviews
                         JOIN Reviewers on Reviews.ReviewerId = Reviewers.ReviewerId;";

            using var conn = new SqlConnection(connString);
            conn.Open();

            using var command = new SqlCommand(sql, conn);
            using var reader = command.ExecuteReader();

            var reviewers = new List<Reviewer>();
            while (reader.Read())
            {
                var review = new Review
                {
                    ReviewId = reader.GetInt32(reader.GetOrdinal("ReviewId")),
                    ReviewerId = reader.GetInt32(reader.GetOrdinal("ReviewerId")),
                    BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                    Stars = reader.GetInt32(reader.GetOrdinal("Stars")),
                    Comment = reader.GetString(reader.GetOrdinal("Comment"))
                };

                var reviewer = reviewers.FirstOrDefault(r => r.ReviewerId == review.ReviewerId);
                if (reviewer == null)
                {
                    reviewer = new Reviewer
                    {
                        ReviewerId = review.ReviewerId,
                        UserName = reader.GetString(reader.GetOrdinal("UserName")),
                        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
                        Reviews = new List<Review> { review }
                    };

                    reviewers.Add(reviewer);
                }
                else
                {
                    reviewer.Reviews.Add(review);
                }
                
            }

            return reviewers;
        }
    
        public IEnumerable<Reviewer> GetReviewersDapper()
        {
            var sql = @" SELECT * FROM Reviews
                         JOIN Reviewers on Reviews.ReviewerId = Reviewers.ReviewerId;";
            using var conn = new SqlConnection(connString);

            var reviewersDictionary = new Dictionary<int, Reviewer>();
            return conn.Query<Review, Reviewer, Reviewer>(sql,
                    (review, reviewer) =>
                    {
                        Reviewer entry;

                        if(!reviewersDictionary.TryGetValue(reviewer.ReviewerId, out entry))
                        {
                            entry = reviewer;
                            entry.Reviews = new List<Review>();
                            reviewersDictionary.Add(entry.ReviewerId, entry);
                        }

                        entry.Reviews.Add(review);

                        return entry;
                    },
                    splitOn: "ReviewerId")
                .Distinct();
        }
    }
}
