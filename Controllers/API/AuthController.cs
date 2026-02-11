using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veterinaria.Data;
using Veterinaria.Models;
using Veterinaria.Services;
using Veterinaria.Repositorios.API;
using BCrypt.Net;

namespace Veterinaria.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDBContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDBContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] Usuario model)
        {
            //Verificar si existe el email
            if (await _context.Usuarios.AnyAsync(u => u.Email == model.Email))
                return BadRequest("Email ya registrado.");

            //Encriptar clave
            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();
            return Ok("Registro exitoso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginView model)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return Unauthorized("Credenciales incorrectas.");
            
            var token = _jwtService.GenerateToken(user);

            return Ok(token);
        }

        [HttpGet("hash-test")]//Hashear claves para pruebas
        public IActionResult HashTest()
        {
            var hash = BCrypt.Net.BCrypt.HashPassword("0311nov");
            return Ok(hash);
        }
    }
}