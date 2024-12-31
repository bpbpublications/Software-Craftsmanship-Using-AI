using System.Net;

namespace NutritionAdvisor.Api.Tests.Smoke
{
    public class SmokeTest
    {
        // Test whether /health endpoint returns 200 OK. Base uri is https://localhost:7230
        [Fact]
        public async Task HealthEndpoint_ReturnsOk()
        {
            // Act
            var response = await TestHttpClient.Instance.GetAsync("health");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
