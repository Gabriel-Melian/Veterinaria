using Veterinaria.Data;
using Veterinaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria.Repositorios.API
{
    //Este repositorio se inyecta despues en los Controllers, y sera el que hable con EF Core directamente
    public class MascotaRepositorio
    {
        private readonly AppDBContext _context;

        public MascotaRepositorio(AppDBContext context)
        {
            _context = context;
        }

        //Obtener por ID
        public async Task<Mascota?> GetByIdAsync(int id)
        {
            return await _context.Mascotas
                .Include(m => m.Cliente)//Incluir al cliente
                .FirstOrDefaultAsync(m => m.Id == id);
        }
        
        //Obtener todos
        public async Task<List<Mascota>> GetAllAsync()
        {
            return await _context.Mascotas
                .Include(m => m.Cliente)
                .ToListAsync();
        }

        //Obtener por duenio
        public async Task<List<Mascota>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Mascotas
                .Where(m => m.IdCliente == clienteId)
                .ToListAsync();
        }

        //Registrar mascota
        public async Task CreateAsync(Mascota mascota)
        {
            await _context.Mascotas.AddAsync(mascota);
        }

        //Guardar
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}