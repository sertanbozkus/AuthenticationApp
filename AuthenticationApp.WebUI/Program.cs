using AuthenticationApp.Business.Managers;
using AuthenticationApp.Business.Services;
using AuthenticationApp.Data.Context;
using AuthenticationApp.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AuthenticationAppContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped<IUserService, UserManager>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/");
    options.LogoutPath = new PathString("/");
    options.AccessDeniedPath = new PathString("/");

    // giriþ - çýkýþ - eriþim reddi durumlarýnda ana sayfaya yönlendiriyorum.


});


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization(); // Bu projede yetkilendirme yok. Ondan konulmayabilir.

app.MapDefaultControllerRoute();

app.Run();
