using System.Globalization;
using AnotherBlazorGrid.Extensions;
using AnotherBlazorGrid.Sandbox.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ProductRepo>();

builder.Services.AddSimpleBlazorGrid(config =>
{
    config.CurrencySymbol = "$";
});

var app = builder.Build();

var cultureInfo = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();