using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Case.Dominio.Interfaces.Servicos;

namespace Case.Servicos
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _clienteRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(Cliente cliente)
        {
            await _clienteRepository.AddAsync(cliente);
        }

        public async Task UpdateAsync(Cliente cliente)
        {
            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task DeleteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }

        public async Task<Cliente> GetByCpfCnpjAsync(string cpfCnpj)
        {
            return await _clienteRepository.GetByCpfCnpjAsync(cpfCnpj);
        }
    }
}
