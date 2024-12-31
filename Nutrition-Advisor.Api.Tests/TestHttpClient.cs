using Polly;

namespace NutritionAdvisor.Api.Tests
{
    public class TestHttpClient
    {
        private static readonly HttpClient _httpClient;

        static TestHttpClient()
        {
            // Retry policy
            var handler = new SocketsHttpHandler
            {
                // Optionally configure HttpClientHandler settings
            };

            // Define a Polly retry policy for transient errors
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(3, (exception, retryCount) =>
                {
                    // Log or handle the retry event
                });

            var httpClient = new HttpClient(new RetryPolicyDelegatingHandler(retryPolicy, handler))
            {
                BaseAddress = new Uri(TestConfiguration.BaseUri),
                Timeout = TimeSpan.FromSeconds(30)
            };

            // Configure PooledConnectionLifetime based on your requirements
            handler.PooledConnectionLifetime = TimeSpan.FromMinutes(2); // Example: Reuse connections for 2 minutes

            _httpClient = httpClient;
        }

        public static HttpClient Instance => _httpClient;
    }

    // Define a custom delegating handler to apply the Polly retry policy
    public class RetryPolicyDelegatingHandler : DelegatingHandler
    {
        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;

        public RetryPolicyDelegatingHandler(IAsyncPolicy<HttpResponseMessage> retryPolicy, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            _retryPolicy = retryPolicy ?? throw new ArgumentNullException(nameof(retryPolicy));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await _retryPolicy.ExecuteAsync(() => base.SendAsync(request, cancellationToken));
        }
    }
}