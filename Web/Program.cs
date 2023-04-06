using Business;
using Business.GenericRepository;
using Business.GenericService;
using Identity;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contexts;
using Service.Entities;
using Service.Entities.Identity;
using Service.Repositories;
using Service.UnitOfWorks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web.CartServiceSession;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDefaultIdentity<IdentityUser>()
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>()
    .AddSignInManager<CustomSignInManager<ApplicationUser>>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});


builder.Services.AddIdentityServices( builder.Configuration);
//builder.Services.AddAuthentication()
//        .AddIdentityServerJwt();
builder.Services.AddTransient<CartService>();

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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization(); ;

app.Run();
