using Case.Dominio.Entidades;

namespace Case.Dominio.Interfaces.Servicos
{
    public interface IUsuarioService
    {
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByCpfCnpjAsync(string cpfCnpj);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task CreateAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
    }
}
