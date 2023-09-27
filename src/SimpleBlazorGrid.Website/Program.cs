using SimpleBlazorGrid.Extensions;
using SimpleBlazorGrid.Website.Data;
using SimpleBlazorGrid.Website.Pages.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<NavBarService>();
builder.Services.AddScoped<VideoGameRepo>();
builder.Services.AddSimpleBlazorGrid(conf =>
{
    conf.PrimaryColour = "#ff0a54";
    conf.SecondaryColour = "#ff477e";
    conf.GlyphColour = "#ffffff";
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();