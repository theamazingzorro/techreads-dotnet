using Xunit;
using Shouldly;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

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

        [Fact]  
        public async Task BookApi_ReturnsAValue()
        {
            var response = await _client.GetAsync("api/book");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            response.Content.ReadAsStringAsync().Result.ShouldBe("test val");
        }
    }
}
