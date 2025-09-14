using Microsoft.EntityFrameworkCore;
using TaskMate.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskMate.Infrastructure.Services;
using Microsoft.OpenApi.Models;
using TaskMate.Application.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//adiciona autentcicação de cookies
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Caminho para a página de login
    options.LogoutPath = "/Account/Logout"; // Caminho para a página de logout
    options.AccessDeniedPath = "/Account/AccessDenied"; // Caminho para a página de acesso negado
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Tempo de expiração do cookie
});

builder.Services.AddAuthorization();

//adiciona o razorpages
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddDbContext<TaskMateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Injeção de dependência
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<ITokenService, TokenService>();


// Configuração de JWT Authentication (somente UMA vez)
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


// Controllers (para API)
builder.Services.AddControllers();

// Swagger com autenticação
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskMate API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu token JWT}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();
app.UseDeveloperExceptionPage();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskMate API V1");
    });
}

app.UseHttpsRedirection();

// Autenticação antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
