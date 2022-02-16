using Xunit;
using Shouldly;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Moq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace TechReads.Web.Tests
{
    public class BookApiControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        readonly WebApplicationFactory<Startup> _factory;
        readonly HttpClient _client;
        public BookApiControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        private HttpClient GetClientWithMock<T>(Mock mock) where T : class
        {
            var application = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton((T) mock.Object);
                });
            });

            return application.CreateClient();
        }

        [Fact]  
        public async Task BookApi_ReturnsAValue()
        {
            var response = await _client.GetAsync("api/book");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            response.Content.ReadAsStringAsync().Result.ShouldBe("test val");
        }
    }
}
