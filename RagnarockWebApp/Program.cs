using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RagnarockWebApp.Data;
using RagnarockWebApp.Interfaces;
using RagnarockWebApp.Models;
using System.Security.Claims;
using WebAppWithDatabase.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RegisteredUser", policy => policy.RequireClaim("User"));
});
builder.Services.AddTransient<IPwdHasher, PwdHasher>();
builder.Services.AddTransient<PwdVerifier>();
builder.Services.AddDbContext<RagnarockWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RagnarockWebAppContext") ?? throw new InvalidOperationException("Connection string 'RagnarockWebAppContext' not found.")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/Forbidden/";
    options.LoginPath = "/Users/Userlogin";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
