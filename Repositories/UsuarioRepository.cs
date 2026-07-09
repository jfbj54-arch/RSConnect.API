using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Models;

namespace RSConnect.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetById(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<Usuario> Create(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await GetById(id);
            if (usuario == null)
                return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
