using Business;
using Business.GenericRepository;
using Business.GenericService;
using Identity.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Service.Entities.Identity;
using Service.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            //services.AddDbContext
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<IMealCategoryService, MealCategoryService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<IMealService, MealService>();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //   {
            //       o.SaveToken = true;// save token in authen session after login succesfull
            //       o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //       {
            //do we want to validate a signing key
            //ValidateIssuerSigningKey = true,
            //           ValidateIssuer = true,
            //           ValidateAudience = true,
            //           ValidateLifetime = true,
            //           ClockSkew = TimeSpan.Zero, //=> saying no additional time

            //           //provide values for things need to validate
            //           ValidIssuer = configuration["JwtSettings:Issuer"],
            //           ValidAudience = configuration["JwtSettings:Audience"],
            //           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            //       };
            //   });
            // bear token should be evaluate , o is assert for options


            services.AddAuthentication()
            .AddCookie(options =>
            {
                //options.LoginPath = "/auth/unauthorized";
                //options.AccessDeniedPath = "/auth/forbidden";
                options.Cookie.Name = "HoangHaCookie";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero, //=> saying no additional time
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });
            //If we use [Authorize] attribute without any parameters, it is going to use the default authorization policy. Let’s define it in the Program.cs:

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            //these provide us with the params that will govern how out token will be validated
            return services;
        }
    }
}
