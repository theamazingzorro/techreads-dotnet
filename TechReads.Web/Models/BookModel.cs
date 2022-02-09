using System;
using System.Linq;
using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Web.Models
{
    public class BookModel
    {
        public BookModel(Book book) => Book = book;
        public Book Book { get; private set; }
    }
}