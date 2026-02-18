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
    public class ClientController : ControllerBase
    {
        private readonly ClientRepositorio _repo;
        private readonly JwtService _jwtService;

        public ClientController(ClientRepositorio repo, JwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
        }

        //Endpoint de registro de cliente
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] Cliente model)
        {
            //Verificar si existe el email
            if (await _repo.GetByEmailAsync(model.Email) != null)
                return BadRequest("Email ya registrado.");

            await _repo.CreateAsync(model);
            return Ok("Registro exitoso.");
        }

        //Endpoint para obtener los datos del cliente por id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _repo.GetByIdAsync(id);

            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            return Ok(cliente);
        }

        //Endpoint para obtener todos los clientes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _repo.GetAllAsync();
            return Ok(clientes);
        }

        //Endpoint para editar cliente (DTO)
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] ClienteDTO datos)
        {
            var cliente = await _repo.GetByIdAsync(id);
            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            //Actualizar campos
            cliente.Nombre = datos.Nombre ?? cliente.Nombre;
            cliente.Apellido = datos.Apellido ?? cliente.Apellido;

            //Verificar si se quiere cambiar el email
            if (!string.IsNullOrWhiteSpace(datos.Email) && datos.Email != cliente.Email)
            {
                //Verificar que el nuevo email no esté registrado
                if (await _repo.GetByEmailAsync(datos.Email) != null)
                    return BadRequest("El nuevo email ya está registrado.");

                cliente.Email = datos.Email;
            }

            cliente.Telefono = datos.Telefono ?? cliente.Telefono;
            cliente.Direccion = datos.Direccion ?? cliente.Direccion;

            await _repo.SaveAsync();//Solo se guarda al final para evitar varias llamadas a la base de datos
            return Ok("Cliente actualizado.");
        }

        //HttpPatch sirve para actualizaciones parciales.
        [HttpPatch("desactivar/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var cliente = await _repo.GetByIdAsync(id);
            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            cliente.Estado = 0;

            await _repo.SaveAsync();

            return Ok("Cliente desactivado.");
        }

        [HttpPatch("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            var cliente = await _repo.GetByIdAsync(id);
            if (cliente == null)
                return NotFound("Cliente no encontrado.");

            cliente.Estado = 1;

            await _repo.SaveAsync();

            return Ok("Cliente activado.");
        }

        //En este caso si usariamos UpdateAsync porque la entidad no fue obtenida del contexto.
        //var cliente = new Cliente { Id = 5, Nombre = "Nuevo" };
        //_context.Clientes.Update(cliente);
        
    }
}