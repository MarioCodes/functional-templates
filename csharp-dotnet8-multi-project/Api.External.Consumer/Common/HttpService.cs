using Api.Core.Configuration;
using Api.External.Consumer.Common.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using System.Text;

namespace Api.External.Consumer.Common
{
    public class HttpService(IOptions<ExternalApiConfig> _apiOptions,
        IOptions<AuthConfig> _authOptions) : IHttpService
    {
        private ExternalApiConfig _apiConfig => _apiOptions.Value;
        private AuthConfig _authConfig => _authOptions.Value;

        public async Task<string> HttpCallAsync(HttpClient client, Func<HttpRequestMessage> requestFactory)
        {
            int retryCount = _apiConfig.RetryAttempts;
            int retryTime = _apiConfig.RetryTimespanInSeconds;

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(
                    retryCount: retryCount,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(retryTime),
                    onRetryAsync: async (exception, timespan, retryAttempt, context) =>
                    {
                        Console.WriteLine($"Retry attempt {retryAttempt} of {retryCount} after {timespan.TotalSeconds} seconds due to: {exception.Message}");
                    });

            return await retryPolicy.ExecuteAsync(async () =>
            {
                using (HttpRequestMessage request = requestFactory())
                {
                    HttpResponseMessage response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            });
        }

        public HttpRequestMessage SetUpGet(string url)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            return AddBasicAuthorization(request);
        }

        public HttpRequestMessage SetUpPost(string url, object payload)
        {
            if (payload is null)
                throw new ArgumentNullException(nameof(payload), $"Cannot create a post with null payload url '{url}'");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            request.Content = new StringContent(JsonConvert.SerializeObject(payload, Formatting.Indented, settings), Encoding.UTF8, "application/json");
            return AddBasicAuthorization(request);
        }

        private HttpRequestMessage AddBasicAuthorization(HttpRequestMessage request)
        {
            // I do it like this (take auth options from appsettings) to keep the code test KISS
            // otherwise I'd use Secrets Manager -> https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#secret-manager
            var authByteArray = Encoding.UTF8.GetBytes($"{_authConfig.User}:{_authConfig.Password}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(authByteArray));
            return request;
        }

    }
}
