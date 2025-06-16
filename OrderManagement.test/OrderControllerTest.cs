using Microsoft.AspNetCore.Mvc.Testing;

namespace OrderManagement.test
{
    public class OrderControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public OrderControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient(); // starts the full app in-memory
        }

        [Fact]
        public async Task GetOrderAnalytics_ReturnsSuccessStatusCode()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/api/order/analytics");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
