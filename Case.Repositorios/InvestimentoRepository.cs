using Case.Data;
using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Case.Repositorios
{
    public class InvestimentoRepository : IInvestimentoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvestimentoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Investimento> GetByIdAsync(int id)
        {
            return await _dbContext.Investimentos.FindAsync(id);
        }

        public async Task<IEnumerable<Investimento>> GetAllAsync()
        {
            return await _dbContext.Investimentos
                .Include(i => i.Produto)
                .Include(i => i.Cliente)
                .ToListAsync();
        }
        public async Task<IEnumerable<Investimento>> GetByClienteCpfCnpjAsync(string cpfCnpj)
        {
            return await _dbContext.Investimentos
                .Include(i => i.Produto)
                .Where(i => i.Cliente.CpfCnpj == cpfCnpj)
                .ToListAsync();
        }


        public async Task<int> AddAsync(Investimento investimento)
        {
            _dbContext.Investimentos.Add(investimento);
            await _dbContext.SaveChangesAsync();
            return investimento.Id;
        }

        public async Task UpdateAsync(Investimento investimento)
        {
            _dbContext.Investimentos.Update(investimento);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var investimento = await GetByIdAsync(id);
            if (investimento != null)
            {
                _dbContext.Investimentos.Remove(investimento);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
