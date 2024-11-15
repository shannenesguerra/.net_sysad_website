using Microsoft.AspNetCore.Authentication.Cookies; // Add this for cookie authentication
using Microsoft.EntityFrameworkCore;
using NUCCITWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/Login"; // Change to your login path
        options.AccessDeniedPath = "/User/AccessDenied"; // Change to your access denied path
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Optional: Set expiration time for the authentication cookie
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireMAA", policy => policy.RequireRole("MAA"));
    options.AddPolicy("RequireMWA", policy => policy.RequireRole("MWA"));
});

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Optional: Set session timeout
    options.Cookie.HttpOnly = true; // Make the cookie HTTP-only
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add authentication and authorization middleware
app.UseAuthentication(); // Add this line
app.UseAuthorization();

// Add session middleware
app.UseSession(); // Add this line

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
