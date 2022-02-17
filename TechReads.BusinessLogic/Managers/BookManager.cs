using TechReads.Library.Data;
using TechReads.Library.Models;

namespace TechReads.BusinessLogic
{
    public class BookManager : IBookManager
    {
        private readonly TechReadsContext _context;
        private readonly IBookRepository _bookRepository;
        public BookManager(TechReadsContext context, IBookRepository bookRepo)
        {
            _context = context;
            _bookRepository = bookRepo;
            EnsureDatabaseSetupCompleted(_context);
        }
        private static void EnsureDatabaseSetupCompleted(TechReadsContext context)
        {
            context.Database.EnsureCreated();
            SeedData.Initialize(context);
        }

        public IEnumerable<Book> GetBooks()
        {
            //return _context.Books.Take(10).ToArray();
            return _bookRepository.GetBooksADO();
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.BookId == id);
        }

        public void DeleteById(int id)
        {
            var book = _context.Books.First(b => b.BookId == id);
            _context.Remove(book);
            _context.SaveChanges();
        }

        public void UpSert(Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
        }

        public double? GetAverageStarsById(int id)
        {
            var reviews = _context.Reviews.Where(r => r.BookId == id)
                .Select(r => r.Stars);

            if (reviews.Any())
            {
                return reviews.Average();
            } 
            else
            {
                return null;
            }
        }
    }
}