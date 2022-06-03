namespace BlazorApp.Client.Services
{
    public class LocalhostService : HttpClientServiceBase
    {
        public LocalhostService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
