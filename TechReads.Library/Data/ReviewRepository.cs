using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using Dapper;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public class ReviewRepository : IReviewRepository
    {
        private string connString = "Server=(localdb)\\mssqllocaldb;Database=TechReadsDB_MVC;Trusted_Connection=True;MultipleActiveResultSets=true";

        public IEnumerable<Review> GetReviewsADO()
        {
            var sql = "select * from Reviews;";

            using var conn = new SqlConnection(connString);
            conn.Open();

            using var command = new SqlCommand(sql, conn);
            using var reader = command.ExecuteReader();

            var reviews = new List<Review>();
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

                reviews.Add(review);
            }

            return reviews;
        }

        public IEnumerable<Review> GetReviewsDapper()
        {
            var sql = "select * from Reviews";

            using var conn = new SqlConnection(connString);

            return conn.Query<Review>(sql);
        }
    }
}
