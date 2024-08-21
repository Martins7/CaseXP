using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Dominio.Interfaces.Servicos
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository)
        {
            _transacaoRepository = transacaoRepository;
        }

        public async Task<IEnumerable<Transacao>> GetTransacoesByInvestimentoIdAsync(int investimentoId)
        {
            return await _transacaoRepository.GetByInvestimentoIdAsync(investimentoId);
        }
    }
}
