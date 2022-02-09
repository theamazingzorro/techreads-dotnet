using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechReads.Library.Models;
using TechReads.Library.Data;

namespace TechReads.Library.Tests
{
    public class ServiceBuilder
    {
        public ServiceBuilder()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<TechReadsContext>(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TechReadsDB_Test;Trusted_Connection=True;MultipleActiveResultSets=true"),
                ServiceLifetime.Transient);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }

    public class TechReadsContextTests: IClassFixture<ServiceBuilder>
    {
        public IServiceProvider _serviceProvider = null;

        public TechReadsContextTests(ServiceBuilder builder)
        {
            _serviceProvider = builder.ServiceProvider;
        }

        [Fact]
        public void ContextCanConnectToDatabase()
        {
            using (var context = GetDefaultDbContext())
            {
                context.Database.EnsureCreated();
                Assert.True(context.Database.CanConnect(), "Cannot connect to database");
            }
        }

        [Fact]
        public async void SeedDataClearedDatabaseHasNoBooks()
        {
            var isCleared = SeedData.ClearDatabase(_serviceProvider);
            Assert.True(isCleared, "Database could not be cleared.");

            using (var context = GetDefaultDbContext())
            {
                Assert.False(await context.Books.AnyAsync(), "Unexpected Books found");
                Assert.False(await context.Reviewers.AnyAsync(), "Unexpected Reviewers found");
            }
        }

        [Fact]
        public async void SeedDataCreatesDataInDatabase()
        {
            var isCleared = SeedData.ClearDatabase(_serviceProvider);
            Assert.True(isCleared, "Database could was not cleared.");
            
            using (var context = GetDefaultDbContext())
            {
                SeedData.Initialize(context);
                var hasAnyBooks = await context.Books.AnyAsync();
                Assert.True(hasAnyBooks);
            }
        }

        private TechReadsContext GetDefaultDbContext()
        {
            // var options = _serviceProvider.GetRequiredService<DbContextOptions<TechReadsContext>>();
            // var result = new TechReadsContext(options);
            var result = _serviceProvider.GetRequiredService<TechReadsContext>();
            return result;
        }

        [Fact]
        public async void AddBookToDatabase()
        {
            var book = new Book { Title = "Skeeters", Authors = "Jeffrey Miller", ReleaseDate = DateTime.Parse("2017-11-15") };

            using(var dbContext = GetDefaultDbContext())
            {
                dbContext.Database.EnsureCreated();
                dbContext.Books.Add(book);
                dbContext.SaveChanges();
                var actual = await dbContext.Books.FirstOrDefaultAsync(b => b.Title == "Skeeters");
                Assert.NotNull(actual);
                Assert.Equal(book.Authors, actual.Authors);
                Assert.Equal(book.Title, actual.Title);
                Assert.NotEqual(0, actual.BookId);
            }
        }
    }
}
