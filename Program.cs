
using Microsoft.AspNetCore.Authentication.JwtBearer;//Api
using Microsoft.EntityFrameworkCore;//Api
using Microsoft.IdentityModel.Tokens;//Api
//using System.IdentityModel.Tokens;//Api
using System.Text;//Api
using Veterinaria.Services;//Api
using Veterinaria.Repositorios;//Api
using Veterinaria.Data;
using Veterinaria.Repositorios.API;

var builder = WebApplication.CreateBuilder(args);

//Configuracion de la conexion a bdd MySQL
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

//Configurar JWT para la API
builder.Services.AddScoped<JwtService>();
//Repositorios (Definir como se resuelve la inyeccion llegado ese momento)
builder.Services.AddScoped<UserRepositorio>();
//builder.Services.AddScoped<RepositorioMascota>();
//builder.Services.AddScoped<RepositorioCliente>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
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

// Add services to the container.
builder.Services.AddControllersWithViews();//Esto es para MVC + API (Controllers y Views)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();//(Habilita servir archivos desde el wwwroot)

app.UseRouting();

//Autenticacion y autorizacion (JWT)
app.UseAuthentication();
app.UseAuthorization();
////Mapear los endpoints de los controllers, para que se puedan usar las rutas definidas en cada uno
//(como el /api/auth/login del AuthController) y no solo las rutas del MVC tradicional (MapControllers)
app.MapControllers();

//Rutas MVC tradicionales para la web
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//Paquetes y dependencias utilizadas API:
//dotnet add package Microsoft.EntityFrameworkCore
//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.10//Use este que si es compatible
//dotnet add package Pomelo.EntityFrameworkCore.MySql
//dotnet add package MySql.Data
//dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
//dotnet add package System.IdentityModel.Tokens.Jwt
//dotnet add package BCrypt.Net-Next
//dotnet add package Swashbuckle.AspNetCore//No lo instale, pero creo que ya esta incluido el Swagger

//Para Debuggear, si se traba o algo, hacer esto:
//netstat -ano | findstr :5043 //taskkill /PID 22700 /F (Reemplazar 22700 con el numero que figuere a continuacion del LISTENING)

//Paquetes y dependencias utilizadas web MVC:
//dotnet add package Microsoft.AspNetCore.Authentication.Cookies
