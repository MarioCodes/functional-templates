namespace Api.External.Consumer.Common.Interfaces
{
    public interface IHttpService
    {
        /// <summary>
        /// Executes an HTTP call with built-in retry logic for increased realiability.
        /// If the request fails due to an <see cref="HttpRequestException"/>, <see cref="TaskCanceledException"/> or <see cref="TimeoutException"/> the request will be retried a configurable number of times before failing. 
        /// </summary>
        /// <param name="client">A <see cref="HttpClient"/> is used to send the HTTP request.</param>
        /// <param name="requestFactory">A factory function that creates a new <see cref="HttpRequestMessage"/> for each retry attempt.</param>
        /// <returns>
        /// It returns the response content as a string is everything goes okay.
        /// </returns>
        /// <remarks>
        /// The retry mechanism ensures that temporary errors, such as network timeouts do not immediately result in failure.
        /// </remarks>
        /// <exception cref="HttpRequestException">Thrown if the HTTP request fails after all retry attempts.</exception>
        /// <exception cref="TaskCanceledException">Thrown if the task is canceled during execution.</exception>
        /// <exception cref="TimeoutException">Thrown if request times out.</exception>
        Task<string> HttpCallAsync(HttpClient client, Func<HttpRequestMessage> requestFactory);
        
        /// <summary>
        /// Sets up an HTTP GET request to the specified URL using Basic Authorization
        /// </summary>
        /// <param name="url">The URL to use</param>
        /// <returns>An <see cref="HttpRequestMessage"/> object representing the GET request with Basic Authorization.</returns>
        HttpRequestMessage SetUpGet(string url);

        /// <summary>
        /// Sets up an HTTP POST request to the specified URL and provided payload, using Basic Authorization
        /// </summary>
        /// <param name="url">The URL to use</param>
        /// <param name="payload">The payload to send</param>
        /// <returns>An <see cref="HttpRequestError"/> object which represents the POST request with Basic Authorization</returns>
        HttpRequestMessage SetUpPost(string url, object payload);
    }
}
