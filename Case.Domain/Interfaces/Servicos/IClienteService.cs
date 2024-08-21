using Case.Dominio.Entidades;

namespace Case.Dominio.Interfaces.Servicos
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> GetByIdAsync(int id);
        Task<Cliente> GetByCpfCnpjAsync(string cpfCnpj);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(int id);
    }
}
