using Biblioteka.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Dodaj konfiguracj� kontekstu bazy danych
builder.Services.AddDbContext<BibliotekaContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BibliotekaConnection")));

// Dodaj us�ugi MVC i uwierzytelnianie
builder.Services.AddControllersWithViews();

// Dodaj obs�ug� uwierzytelniania i autoryzacji
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Logowanie"; // �cie�ka do logowania
        options.LogoutPath = "/Home/Wyloguj";  // �cie�ka do wylogowania
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

// Obs�uga b��d�w i routingu
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Dodaj obs�ug� uwierzytelniania i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
