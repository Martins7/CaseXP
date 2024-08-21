using Case.Dominio.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Case.Dominio.Interfaces.Servicos
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> GetAllAsync();
        Task<Produto> GetByIdAsync(int id);
        Task<IEnumerable<Produto>> GetProdutosAVencer(int dias);
        Task AddAsync(Produto produto);
        Task UpdateAsync(Produto produto);
        Task DesativarProdutoAsync(int id);
    }
}
