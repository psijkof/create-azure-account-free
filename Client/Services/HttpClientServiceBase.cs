namespace BlazorApp.Client.Services
{
    public class HttpClientServiceBase
    {
        public HttpClient HttpClient;

        public string GetBaseUrl()
        {
            return HttpClient.BaseAddress.ToString();
        }
    }
}