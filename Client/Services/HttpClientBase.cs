namespace BlazorApp.Client.Services
{
    public class HttpClientBase
    {
        public HttpClient? HttpClient;

        public string? GetBaseUrl()
        {
            return HttpClient?.BaseAddress?.ToString();
        }
    }
}