using Veterinaria.Data;
using Veterinaria.Models;
using Microsoft.EntityFrameworkCore;

namespace Veterinaria.Repositorios.API
{
    //Este repositorio se inyecta despues en los Controllers, y sera el que hable con EF Core directamente
    public class UserRepositorio
    {
        private readonly AppDBContext _context;

        public UserRepositorio(AppDBContext context)
        {
            _context = context;
        }

        //Obtener usuario por email, para el login
        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        //Obtener por ID
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        //Actualizar
        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        //Obtener todos (Por las dudas)
        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        //Crear
        public async Task CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        /*public async Task AddAsync(Usuario user)
        {
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();
        }*/
    }
}