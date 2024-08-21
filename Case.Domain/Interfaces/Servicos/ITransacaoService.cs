using Case.Dominio.Entidades;

namespace Case.Dominio.Interfaces.Servicos
{
    public interface ITransacaoService
    {
        Task<IEnumerable<Transacao>> GetTransacoesByInvestimentoIdAsync(int investimentoId);
    }
}
