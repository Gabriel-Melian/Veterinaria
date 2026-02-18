using Veterinaria.Data;
using Veterinaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria.Repositorios.API
{
    //Este repositorio se inyecta despues en los Controllers, y sera el que hable con EF Core directamente
    public class ClientRepositorio
    {
        private readonly AppDBContext _context;

        public ClientRepositorio(AppDBContext context)
        {
            _context = context;
        }

        //Obtener cliente por email
        public async Task<Cliente> GetByEmailAsync(string email)
        {
            return await _context.Clientes.FirstOrDefaultAsync(u => u.Email == email);
        }

        //Obtener por ID
        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }
        
        //Obtener todos
        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        //Registrar cliente
        public async Task CreateAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        //Actualizar
        /*public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }*/

        //Save
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}