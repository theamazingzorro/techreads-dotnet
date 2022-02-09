using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public class TechReadsContext: DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        public TechReadsContext(DbContextOptions<TechReadsContext> options): base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // add configuration code here if needed (connection string should probably be initialized elsewhere using dependency injection)
        }
    }

}