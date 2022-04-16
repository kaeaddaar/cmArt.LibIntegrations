using cmArt.LibIntegrations.ClientControllerService;
using cmArt.Portal.Data;
using cmArt.Portal.Data.GenericSerialization;
using cmArt.Portal.Data.ShopifyData;
using cmArt.Shopify.TestingUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
//builder.RootComponents.Add<HeadOutlet>("head::after"); // Re-Add with .Net6.0

//builder.Logging.SetMinimumLevel(LogLevel.Debug);
//builder.Logging.AddProvider(new CustomLoggingProvider());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
Uri LocalDocumentUri = new Uri("http://localhost:7071");
string route = "/api/JsonDocument/";
string fKey = string.Empty;
builder.Services.AddSingleton<ClientController_Guid<Document_PK, Document>>
    (new ClientController_Guid<Document_PK, Document>(route, fKey ,LocalDocumentUri));
builder.Services.AddSingleton<GenericSerialization_BlazorClient<shopify_price_rule>>(new GenericSerialization_BlazorClient<shopify_price_rule>());
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
//});

//await builder.Build().RunAsync(); // re-add with .Net6.0
builder.Build().RunAsync();
