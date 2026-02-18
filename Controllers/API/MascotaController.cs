using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Veterinaria.Repositorios.API;
using Veterinaria.Models;
using Veterinaria.Services;
using Veterinaria.DTOs;

namespace Veterinaria.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MascotaController : ControllerBase
    {
        private readonly MascotaRepositorio _repo;
        private readonly JwtService _jwtService;

        public MascotaController(MascotaRepositorio repo, JwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
        }

        //Endpoint para obtener los datos de la mascota por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mascota = await _repo.GetByIdAsync(id);

            if (mascota == null)
                return NotFound("Mascota no encontrada.");

            return Ok(mascota);
        }

        //Endpoint para obtener todas las mascotas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mascotas = await _repo.GetAllAsync();
            return Ok(mascotas);
        }

        //Endpoint para registrar mascota
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] MascotaDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mascota = new Mascota
            {
                Nombre = model.Nombre,
                Especie = model.Especie,
                Raza = model.Raza,
                FechaNac = model.FechaNac,
                Sexo = model.Sexo,
                Esterilizado = model.Esterilizado,
                IdCliente = model.IdCliente,
                Estado = 1
            };

            await _repo.CreateAsync(mascota);
            await _repo.SaveAsync();

            return Ok("Mascota registrada exitosamente.");
        }

        //Endpoint para obtener mascotas por cliente
        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetByClienteId(int clienteId)
        {
            var mascotas = await _repo.GetByClienteIdAsync(clienteId);
            return Ok(mascotas);
        }

        //Endpoint para editar mascota (DTO)
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] MascotaDTO datos)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var mascota = await _repo.GetByIdAsync(id);

            if (mascota == null)
                return NotFound("Mascota no encontrada.");

            mascota.Nombre = datos.Nombre;
            mascota.Especie = datos.Especie;
            mascota.Raza = datos.Raza;
            mascota.FechaNac = datos.FechaNac;
            mascota.Sexo = datos.Sexo;
            mascota.Esterilizado = datos.Esterilizado;
            mascota.IdCliente = datos.IdCliente;

            await _repo.SaveAsync();

            return Ok("Mascota actualizada exitosamente.");
        }

        //Endpoint para dar baja logica mascota (desactivar)
        [HttpPatch("desactivar/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var mascota = await _repo.GetByIdAsync(id);
            if (mascota == null)
                return NotFound("Mascota no encontrada.");

            mascota.Estado = 0;
            await _repo.SaveAsync();
            return Ok("Mascota desactivada exitosamente.");
        }

        //Endpoint para activar mascota
        [HttpPatch("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            var mascota = await _repo.GetByIdAsync(id);
            if (mascota == null)
                return NotFound("Mascota no encontrada.");

            mascota.Estado = 1;
            await _repo.SaveAsync();
            return Ok("Mascota activada exitosamente.");
        }

        
    }
}