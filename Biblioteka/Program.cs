using Biblioteka.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj konfiguracjê kontekstu bazy danych
builder.Services.AddDbContext<BibliotekaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BibliotekaConnection")));

// Dodaj us³ugi MVC i uwierzytelnianie
builder.Services.AddControllersWithViews();

// Dodaj obs³ugê uwierzytelniania i autoryzacji
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Logowanie"; // Œcie¿ka do logowania
        options.LogoutPath = "/Home/Wyloguj";  // Œcie¿ka do wylogowania
        options.AccessDeniedPath = "/Home/AccessDenied"; // Opcjonalnie
    });

builder.Services.AddAuthorization();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});


var app = builder.Build();

// Obs³uga b³êdów i routingu
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Dodaj obs³ugê uwierzytelniania i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
