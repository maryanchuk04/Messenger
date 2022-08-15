using Messenger.db.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<MessengerContext>(options =>
{
    Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
    //"data source=MAKSPC;initial catalog=master;trusted_connection=true; Database = WayToDev"
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableServiceProviderCaching(false);
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
;

app.Run();