using Microsoft.EntityFrameworkCore;
using TaskMate.Data;
using TaskMate.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Tasks"); //todos logados podem ver
    options.Conventions.AuthorizeFolder("/Reports", "AdminOnly"); //só os Role = "Admin" pode ver
    //options.Conventions.AllowAnonymousToFolder("/Account/Login"); //login liberado
    options.Conventions.AllowAnonymousToPage("/Account/Login");
    options.Conventions.AllowAnonymousToPage("/Account/Register");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; //redireciona para login
        options.LogoutPath = "/Account/Logout"; //redireciona para logout
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});


// Conexão com o banco de dados
builder.Services.AddDbContext<TaskMateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
//builder.Services.AddRazorPages();

// Serviços
builder.Services.AddScoped<PasswordService>();

builder.Services.AddAuthorization();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TaskMateDbContext>();
    var pwdSvc = services.GetRequiredService<PasswordService>();

    // só cria se não existir
    if (!context.Users.Any(u => u.Email == "admin@local"))
    {
        pwdSvc.CreatePasswordHash("SenhaForte@123", out var hash, out var salt);

        var admin = new TaskMate.Domain.Entities.User
        {
            Nome = "Administrador",
            Email = "admin@local",
            PasswordHash = hash,
            PasswordSalt = salt,
            Role = "Admin"
        };

        context.Users.Add(admin);
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
