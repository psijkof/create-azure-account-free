namespace BlazorApp.Client.Services
{
    public class LocalhostHttpClient : HttpClientBase
    {
        public LocalhostHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
