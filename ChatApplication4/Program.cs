 
using ChatApplication4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
//builder.Services.AddSession();
//Configuring Session Services in ASP.NET Core
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.Name = ".MySampleMVCWeb.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//builder.Services.AddDbContext<AppDbContext>(options =>
// options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectioString")));
 
builder.Services.AddControllersWithViews();

// Add SignalR services
builder.Services.AddSignalR(); // Add this line

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Use SignalR
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/ChatHub"); // Add this line
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Chat}/{action=Index}/{id?}");
});

app.Run();
