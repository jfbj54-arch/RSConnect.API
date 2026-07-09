using RSConnect.API.Models;

namespace RSConnect.API.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario?> GetById(int id);
        Task<Usuario> Create(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task<bool> Delete(int id);
    }
}

