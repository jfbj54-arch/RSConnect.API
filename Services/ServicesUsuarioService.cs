using RSConnect.API.Models;
using RSConnect.API.Repositories;

namespace RSConnect.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Usuario>> GetAll() => _repo.GetAll();
        public Task<Usuario> GetById(int id) => _repo.GetById(id);
        public Task<Usuario> Create(Usuario usuario) => _repo.Create(usuario);
        public Task<Usuario> Update(Usuario usuario) => _repo.Update(usuario);
        public Task<bool> Delete(int id) => _repo.Delete(id);
    }
}
