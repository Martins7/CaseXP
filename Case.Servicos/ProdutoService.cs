using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Case.Dominio.Interfaces.Servicos;

namespace Case.Servicos
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            return await _produtoRepository.GetAllAsync();
        }

        public async Task<Produto> GetByIdAsync(int id)
        {
            return await _produtoRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Produto produto)
        {
            await _produtoRepository.AddAsync(produto);
        }

        public async Task UpdateAsync(Produto produto)
        {
            await _produtoRepository.UpdateAsync(produto);
        }

        public async Task DesativarProdutoAsync(int id)
        {
            await _produtoRepository.DeactivateAsync(id);
        }

        public async Task<IEnumerable<Produto>> GetProdutosAVencer(int dias)
        {
            var dataLimite = DateTime.Now.AddDays(dias);
            return await _produtoRepository.GetByVencimentoAsync(dataLimite);
        }
    }
}
