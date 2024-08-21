using Case.Dominio.Entidades;

namespace Case.Dominio.Interfaces.Repositorios
{
    public interface ITransacaoRepository
    {
        Task<Transacao> GetByIdAsync(int id);
        Task<IEnumerable<Transacao>> GetByInvestimentoIdAsync(int investimentoId);
        Task AddAsync(Transacao transacao);
    }
}
