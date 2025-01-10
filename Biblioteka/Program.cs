using Microsoft.EntityFrameworkCore;
using Biblioteka.Models;

var builder = WebApplication.CreateBuilder(args);

// Dodaj konfiguracj� kontekstu bazy danych
builder.Services.AddDbContext<BibliotekaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BibliotekaConnection")));



builder.Services.AddControllersWithViews();

var app = builder.Build();

// Obs�uga b��d�w i routingu
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();