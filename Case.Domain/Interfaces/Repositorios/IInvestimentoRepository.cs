using Case.Dominio.Entidades;

namespace Case.Dominio.Interfaces.Repositorios
{
    public interface IInvestimentoRepository
    {
        Task<Investimento> GetByIdAsync(int id);
        Task<IEnumerable<Investimento>> GetAllAsync();
        Task<IEnumerable<Investimento>> GetByClienteCpfCnpjAsync(string cpfCnpj);
        Task<int> AddAsync(Investimento investimento);
        Task UpdateAsync(Investimento investimento);
        Task DeleteAsync(int id);
    }
}
