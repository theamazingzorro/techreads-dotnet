using System;
using System.Linq;
using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Web.Models
{
    public class BookListModel
    {
        public BookListModel(IEnumerable<TechReads.Library.Models.Book> books) => Books = books;
        public IEnumerable<TechReads.Library.Models.Book> Books { get; private set; }
    }
}