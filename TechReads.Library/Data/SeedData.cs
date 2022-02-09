using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TechReads.Library.Models;

namespace TechReads.Library.Data
{
    public class SeedData
    {
        public static void Initialize(TechReadsContext context)
        {

            // Check for existing database records
            if (context.Books.Any())
            {
                return; // Database already initialized - do nothing
            }

            context.Books.AddRange(
                new Book { Title = "Pragmatic Programmer", Authors = "Dave Thomas, Andy Hunt",
                    ReleaseDate = DateTime.Parse("2001-01-01") },
                new Book { Title = "Never Eat Alone", Authors = "Keith Ferrazzi" },
                new Book { Title = "The Five Dysfunctions of a Team", Authors = "Patrick Lencioni" },
                new Book { Title = "Clean Code", Authors = "Robert C. Martin" }
            );

            context.SaveChanges();            
            
        }

        internal static bool ClearDatabase(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<TechReadsContext>>();

            try
            {
                using var context = new TechReadsContext(options);

                var isDeleted = context.Database.EnsureDeleted();
                var isCreated = context.Database.EnsureCreated();
                var result = isDeleted && isCreated;

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}