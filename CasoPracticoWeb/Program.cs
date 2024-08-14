using InfoBretesWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using InfoBretesWeb.Architecture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
LocalConfiguration.Register(builder.Services);
RepositoryConfiguration.Register(builder.Services);

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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
