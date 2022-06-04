namespace BlazorApp.Client.Services
{
    public class AzFunctionService : HttpClientServiceBase
    {
        public AzFunctionService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
