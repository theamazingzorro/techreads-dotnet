using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public class BookRepository : IBookRepository
    {
        private string connString = "Server=(localdb)\\mssqllocaldb;Database=TechReadsDB_MVC;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public IEnumerable<Book> GetBooksADO()
        {
            var sql = "select * from Books;";

            using var conn = new SqlConnection(connString);
            conn.Open();

            using var command = new SqlCommand(sql, conn);
            using var reader = command.ExecuteReader();

            var books = new List<Book>();
            while(reader.Read())
            {
                var isbn = reader.GetValue(reader.GetOrdinal("ISBN"));
                if (isbn == DBNull.Value)
                {
                    isbn = "";
                }

                var rDate = reader.GetValue(reader.GetOrdinal("ReleaseDate"));
                if (rDate == DBNull.Value)
                {
                    rDate = null;
                }

                var book = new Book
                {
                    BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Authors = reader.GetString(reader.GetOrdinal("Authors")),
                    ISBN = (string) isbn,
                    ReleaseDate = (DateTime?) rDate   
                };

                books.Add(book);
            }

            return books;
        }

        public IEnumerable<Book> GetBooksDapper()
        {
            var sql = "select * from Books";

            using var conn = new SqlConnection(connString);

            return conn.Query<Book>(sql);
        }
    }
}
