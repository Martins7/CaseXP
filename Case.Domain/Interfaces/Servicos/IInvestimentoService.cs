using Case.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Dominio.Interfaces.Servicos
{
    public interface IInvestimentoService
    {
        Task<IEnumerable<Investimento>> GetAllAsync();
        Task<Investimento> GetByIdAsync(int id);
        Task ComprarInvestimentoAsync(int produtoId, int clienteId, int quantidade, decimal preco);
        Task VenderInvestimentoAsync(int id, int quantidade, decimal preco);
        Task<IEnumerable<Investimento>> GetByClienteIdAsync(int clienteId);
        Task<IEnumerable<Investimento>> GetByClienteCpfCnpjAsync(string cpfCnpj);
    }
}
