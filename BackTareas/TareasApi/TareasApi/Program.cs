using TareasApi.Application.Interfaces;
using TareasApi.Application.Services;
using TareasApi.Infrastructure.Auth;
using TareasApi.Infrastructure.Interfaces;
using TareasApi.Infrastructure.Logging;
using TareasApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DeportesApi.Infrastructure.Logging;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuración de SQL Server
builder.Services.AddDbContext<TareasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Configuración de JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]);
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
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        ValidateLifetime = true
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddScoped<ITareaService , TareaService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();


var loggingType = builder.Configuration["Logger:Type"];

if (loggingType == "Database")
{
    builder.Services.AddScoped<ILoggerService, DatabaseLoggerService>();
}
else
{
    builder.Services.AddSingleton<ILoggerService, FileLoggerService>();
}


var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();
app.Run();
