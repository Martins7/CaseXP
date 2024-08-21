using Case.Data;
using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Case.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _dbContext.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task AddAsync(Usuario usuario)
        {
            _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _dbContext.Usuarios.Update(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _dbContext.Usuarios.Remove(usuario);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Usuario> GetByCpfCnpjAsync(string cpfCnpj)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.CpfCnpj == cpfCnpj);
        }
    }
}
