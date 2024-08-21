using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Case.Dominio.Interfaces.Servicos;
using Moq;
using Xunit;

namespace Case.Servicos.Tests
{
    public class TransacaoServiceTests
    {
        private readonly TransacaoService _transacaoService;
        private readonly Mock<ITransacaoRepository> _mockTransacaoRepository;

        public TransacaoServiceTests()
        {
            _mockTransacaoRepository = new Mock<ITransacaoRepository>();
            _transacaoService = new TransacaoService(_mockTransacaoRepository.Object);
        }

        [Fact]
        public async Task GetTransacoesByInvestimentoIdAsync_DeveRetornarTransacoes()
        {
            // Arrange
            var investimentoId = 1;
            var transacoes = new List<Transacao>
            {
                new Transacao { InvestimentoId = investimentoId, Quantidade = 10, Preco = 100.0M },
                new Transacao { InvestimentoId = investimentoId, Quantidade = 5, Preco = 50.0M }
            };
            _mockTransacaoRepository.Setup(repo => repo.GetByInvestimentoIdAsync(investimentoId)).ReturnsAsync(transacoes);

            // Act
            var result = await _transacaoService.GetTransacoesByInvestimentoIdAsync(investimentoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.All(result, t => Assert.Equal(investimentoId, t.InvestimentoId));
        }
    }
}
