using BlazorApp.Client;
using BlazorApp.Client.Interop;
using BlazorApp.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<LocalhostService>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

//builder.Services.AddScoped(sp => new HttpClient { }
builder.Services.AddScoped<SharedState>();
builder.Services.AddMudServices();

//var provider = new FileExtensionContentTypeProvider();
////provider.Mappings.Add(".json", "application/json");
//builder.Services.Configure<StaticFileOptions>(options =>
//{
//    options.ContentTypeProvider = provider;
//});

await builder.Build().RunAsync();