namespace BlazorApp.Client.Services
{
    public class AzFunctionHttpClient : HttpClientBase
    {
        public AzFunctionHttpClient(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }
    }
}
