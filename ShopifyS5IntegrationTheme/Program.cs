var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "See the \"ShopifyThemeFolders\" folder for the theme files exported from Shopify");

app.Run();
