using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Veterinaria.Repositorios.API;
using Veterinaria.Models;
using Veterinaria.Services;

namespace Veterinaria.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//Requiere autenticacion para acceder a los endpoints
    public class UserController : ControllerBase
    {
        private readonly UserRepositorio _repo;
        private readonly JwtService _jwtService;

        public UserController(UserRepositorio repo, JwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
        }

        //Endpoint para obtener los datos del usuario logueado
        [HttpGet("perfil")]
        public async Task<IActionResult> GetPerfil()
        {
            //Obtener email del token JWT
            var email = User.Identity?.Name;
            if (email == null) return Unauthorized("Token inválido o expirado.");

            var user = await _repo.GetByEmailAsync(email);
            if (user == null) return NotFound("Propietario no encontrado.");

            return Ok(user);
        }

        //Editar perfil del usuario autenticado
        [HttpPut("editar")]
        public async Task<IActionResult> EditarPerfil([FromBody] Usuario datos)
        {
            try
            {
                //Obtener email actual del token
                var emailToken = User.Identity?.Name;
                if (emailToken == null)
                    return Unauthorized("Token inválido o expirado.");

                var user = await _repo.GetByEmailAsync(emailToken);
                if (user == null)
                    return NotFound("Propietario no encontrado.");

                //Actualizar campos
                user.Nombre = datos.Nombre ?? user.Nombre;
                user.Apellido = datos.Apellido ?? user.Apellido;

                //*Para ver si modifico el email*
                bool emailCambiado = false;

                if (!string.IsNullOrWhiteSpace(datos.Email) && datos.Email != user.Email)
                {
                    user.Email = datos.Email;
                    emailCambiado = true;
                }

                await _repo.UpdateAsync(user);

                //Si cambio el email, genera un token nuevo
                if (emailCambiado)
                {
                    var nuevoToken = _jwtService.GenerateToken(user);
                    return Ok(new
                    {
                        message = "Perfil actualizado correctamente. Nuevo token generado.",
                        token = nuevoToken
                    });
                }

                return Ok(new { message = "Perfil actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al editar perfil: {ex.Message}");
            }
        }
    }
}