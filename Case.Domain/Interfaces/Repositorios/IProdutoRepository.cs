using Case.Dominio.Entidades;

namespace Case.Dominio.Interfaces.Repositorios
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetByVencimentoAsync(DateTime dataLimite);
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DeactivateAsync(int id);
    }
}
