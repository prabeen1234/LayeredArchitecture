using FluentValidation;
using FluentValidation.AspNetCore;
using LayeredArchitecture.Data;
using LayeredArchitecture.Data.Repositories;
using LayeredArchitecture.Models;
using LayeredArchitecture.Services;
using LayeredArchitecture.Services.CustomValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

// 2. Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
var hasher = new PasswordHasher<ApplicationUser>();
var user = new ApplicationUser { UserName = "admin@gmail.com" };
var password = "Admin123!";
var hashedPassword = hasher.HashPassword(user, password);
Console.WriteLine(hashedPassword);
// 3. JWT Authentication
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

// 4. Application Services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepo>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthRepo>();

// 5. FluentValidation
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidation>();
builder.Services.AddFluentValidationAutoValidation();

// 6. Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// 7. Middleware


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
