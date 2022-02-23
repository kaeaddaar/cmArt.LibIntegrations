using cmArt.LibIntegrations.ClientControllerService;
using cmArt.Portal.Data.InventoryData;
using cmArt.Shopify.TestingUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Cors;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Logging.SetMinimumLevel(LogLevel.Debug);
//builder.Logging.AddProvider(new CustomLoggingProvider());
string ControllerRoute = "/api/WebInventory_Controller";
string FunctionKey = "";
Uri BaseAddressUri = new Uri("http://localhost:7071");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddScoped(x => new ClientController_int<WebInventory, WebInventory>());
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
//});

await builder.Build().RunAsync();
