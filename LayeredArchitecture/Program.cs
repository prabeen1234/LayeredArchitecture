//using LayeredArchitecture.Data;
//using LayeredArchitecture.Data.Repositories;
//using LayeredArchitecture.Models;
//using LayeredArchitecture.Services;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

//var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ClockSkew = TimeSpan.Zero
//    };
//});

//builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
//    .AddEntityFrameworkStores<AppDbContext>()
//    .AddDefaultTokenProviders();
//builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddControllers();

//builder.Services.AddScoped<IJwtService, JwtService>();
//builder.Services.AddScoped<ProductService>();
//builder.Services.AddScoped<ProductRepo>();
//builder.Services.AddScoped<AuthService>();
//builder.Services.AddScoped<AuthRepo>();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using FluentValidation.AspNetCore;
using FluentValidation;
using LayeredArchitecture.Data;
using LayeredArchitecture.Data.Repositories;
using LayeredArchitecture.Models;
using LayeredArchitecture.Services;
using LayeredArchitecture.Services.CustomValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

// Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthRepo>();

// JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

// Add Services
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidation>();
builder.Services.AddFluentValidationAutoValidation(); // Enables automatic validation

var app = builder.Build();

// Middleware
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();