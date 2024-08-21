using Case.Data;
using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Repositorios
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransacaoRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Transacao transacao)
        {
            _dbContext.Transacoes.Add(transacao);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Transacao> GetByIdAsync(int id)
        {
            return await _dbContext.Transacoes.FindAsync(id);
        }

        public async Task<IEnumerable<Transacao>> GetByInvestimentoIdAsync(int investimentoId)
        {
            return await _dbContext.Transacoes
                          .Where(t => t.InvestimentoId == investimentoId)
                          .ToListAsync();
        }
    }
}
