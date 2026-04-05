using HMS.Api;
using HMS.Api.Models;
using HMS.API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// =====================
// Add Services
// =====================

// Controllers
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Swagger / OpenAPI
builder.Services.AddOpenApi();

// Database
builder.Services.AddDbContext<DepartmentDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConString")
    ));

// Repository
builder.Services.AddScoped<IPatientRepository, PatientRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


// =====================
// JWT Authentication
// =====================

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
        )
    };
});


// =====================
// Build App
// =====================

var app = builder.Build();


// =====================
// Middleware
// =====================

// Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Swagger (Development Only)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Authentication
app.UseAuthentication();

// Authorization
app.UseAuthorization();

// Controllers
app.MapControllers();

app.Run();