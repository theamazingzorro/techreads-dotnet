using System;
using System.Linq;
using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Web.Models
{
    public class BookListModel
    {
        public IEnumerable<Book> Books { get; init; }
    }
}