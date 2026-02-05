
using Microsoft.EntityFrameworkCore;//Api
using Veterinaria.Data;//Api

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

//Configuracion de la conexion a la base de datos MySQL
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

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
