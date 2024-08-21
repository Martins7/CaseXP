using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using Case.Data;

namespace Case.Repositorios
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProdutoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Produto produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeactivateAsync(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);
            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            produto.Disponivel = false;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _dbContext.Produtos.FindAsync(id);
        }

        public async Task<IEnumerable<Produto>> GetByVencimentoAsync(DateTime dataLimite)
        {
            return await _dbContext.Produtos
             .Where(p => p.DataVencimento <= dataLimite)
             .ToListAsync();
        }

        public async Task UpdateAsync(Produto produto)
        {
            _dbContext.Produtos.Update(produto);
             await _dbContext.SaveChangesAsync();
        }
    }
}
