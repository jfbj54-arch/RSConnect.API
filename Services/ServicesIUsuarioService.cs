using RSConnect.API.Models;

namespace RSConnect.API.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario?> GetById(int id);
        Task<Usuario?> GetByEmail(string email);
        Task<Usuario?> Login(string email, string senha);
        Task<Usuario> Create(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task<bool> Delete(int id);
    }
}
