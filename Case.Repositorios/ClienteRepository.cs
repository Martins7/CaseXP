using Case.Data;
using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Case.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClienteRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(int clienteId)
        {
            return await _dbContext.Clientes.FindAsync(clienteId);
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _dbContext.Clientes.AddAsync(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            _dbContext.Clientes.Update(cliente);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int clienteId)
        {
            var cliente = await GetByIdAsync(clienteId);
            if (cliente != null)
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Cliente> GetByCpfCnpjAsync(string cpfCnpj)
        {
            return await _dbContext.Clientes.FirstOrDefaultAsync(f => f.CpfCnpj == cpfCnpj);
        }
    }
}

