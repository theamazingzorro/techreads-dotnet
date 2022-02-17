using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooksADO();

        IEnumerable<Book> GetBooksDapper();
    }
}