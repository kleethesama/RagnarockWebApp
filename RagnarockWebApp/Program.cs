﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RagnarockWebApp.Data;
using RagnarockWebApp.Interfaces;
using RagnarockWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie(options =>
//{
//    options.LoginPath = new PathString("/Index");
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
//});

//builder.Services.AddMvc().AddRazorPagesOptions(options =>
//{
//    options.Conventions.AuthorizeFolder("/");
//    options.Conventions.AllowAnonymousToPage("/Index");
//});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IPwdHasher, PwdHasher>();
builder.Services.AddDbContext<RagnarockWebAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RagnarockWebAppContext") ?? throw new InvalidOperationException("Connection string 'RagnarockWebAppContext' not found.")));

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
