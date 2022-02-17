using TechReads.Library.Models;

namespace TechReads.BusinessLogic
{
    public interface IBookManager
    {
        void DeleteById(int id);
        Book? GetBookById(int id);
        IEnumerable<Book> GetBooks();
        void UpSert(Book book);

        double? GetAverageStarsById(int id);
    }
}