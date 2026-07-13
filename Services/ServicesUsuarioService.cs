using RSConnect.API.Models;
using RSConnect.API.Repositories;

namespace RSConnect.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Usuario>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<Usuario?> GetById(int id)
        {
            return _repository.GetById(id);
        }

        // 🔥 NOVO MÉTODO PARA LOGIN
        public Task<Usuario?> GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public Task<Usuario> Create(Usuario usuario)
        {
            return _repository.Create(usuario);
        }

        public Task<Usuario> Update(Usuario usuario)
        {
            return _repository.Update(usuario);
        }

        public Task<bool> Delete(int id)
        {
            return _repository.Delete(id);
        }
    }
}
