using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Moq;
using Xunit;

namespace Case.Servicos.Tests
{
    public class InvestimentoServiceTests
    {
        private readonly InvestimentoService _investimentoService;
        private readonly Mock<IInvestimentoRepository> _mockInvestimentoRepository;
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly Mock<ITransacaoRepository> _mockTransacaoRepository;

        public InvestimentoServiceTests()
        {
            _mockInvestimentoRepository = new Mock<IInvestimentoRepository>();
            _mockClienteRepository = new Mock<IClienteRepository>();
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _mockTransacaoRepository = new Mock<ITransacaoRepository>();

            _investimentoService = new InvestimentoService(
                _mockInvestimentoRepository.Object,
                _mockClienteRepository.Object,
                _mockProdutoRepository.Object,
                _mockTransacaoRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarInvestimentos()
        {
            // Arrange
            var investimentos = new List<Investimento>
            {
                new Investimento { Id = 1 },
                new Investimento { Id = 2 }
            };
            _mockInvestimentoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(investimentos);

            // Act
            var result = await _investimentoService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarInvestimento()
        {
            // Arrange
            var investimento = new Investimento { Id = 1 };
            _mockInvestimentoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(investimento);

            // Act
            var result = await _investimentoService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByClienteIdAsync_ClienteExistente_DeveRetornarInvestimentos()
        {
            // Arrange
            var cliente = new Cliente
            {
                Id = 1,
                Investimentos = new List<Investimento>
                {
                    new Investimento { Id = 1 },
                    new Investimento { Id = 2 }
                }
            };
            _mockClienteRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(cliente);

            // Act
            var result = await _investimentoService.GetByClienteIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task ComprarInvestimentoAsync_ProdutoExistente_DeveAdicionarInvestimentoETransacao()
        {
            // Arrange
            var produto = new Produto { Id = 1 };
            var investimento = new Investimento { Id = 1 };
            _mockProdutoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);
            _mockInvestimentoRepository.Setup(repo => repo.AddAsync(It.IsAny<Investimento>())).ReturnsAsync(investimento.Id);
            _mockTransacaoRepository.Setup(repo => repo.AddAsync(It.IsAny<Transacao>())).Returns(Task.CompletedTask);

            // Act
            await _investimentoService.ComprarInvestimentoAsync(1, 1, 10, 100);

            // Assert
            _mockInvestimentoRepository.Verify(repo => repo.AddAsync(It.IsAny<Investimento>()), Times.Once);
            _mockTransacaoRepository.Verify(repo => repo.AddAsync(It.IsAny<Transacao>()), Times.Once);
        }

        [Fact]
        public async Task VenderInvestimentoAsync_InvestimentoExistente_ComQuantidadeSuficiente_DeveAtualizarInvestimentoEAdicionarTransacao()
        {
            // Arrange
            var investimento = new Investimento { Id = 1, Quantidade = 20 };
            _mockInvestimentoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(investimento);
            _mockInvestimentoRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Investimento>())).Returns(Task.CompletedTask);
            _mockTransacaoRepository.Setup(repo => repo.AddAsync(It.IsAny<Transacao>())).Returns(Task.CompletedTask);

            // Act
            await _investimentoService.VenderInvestimentoAsync(1, 10, 50);

            // Assert
            Assert.Equal(10, investimento.Quantidade);
            _mockInvestimentoRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Investimento>()), Times.Once);
            _mockTransacaoRepository.Verify(repo => repo.AddAsync(It.IsAny<Transacao>()), Times.Once);
        }

        [Fact]
        public async Task VenderInvestimentoAsync_QuantidadeInsuficiente_DeveLancarExcecao()
        {
            // Arrange
            var investimento = new Investimento { Id = 1, Quantidade = 5 };
            _mockInvestimentoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(investimento);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _investimentoService.VenderInvestimentoAsync(1, 10, 50));
        }

        [Fact]
        public async Task GetByClienteCpfCnpjAsync_DeveRetornarInvestimentos()
        {
            // Arrange
            var investimentos = new List<Investimento>
            {
                new Investimento { Id = 1 },
                new Investimento { Id = 2 }
            };
            _mockInvestimentoRepository.Setup(repo => repo.GetByClienteCpfCnpjAsync("12345678901")).ReturnsAsync(investimentos);

            // Act
            var result = await _investimentoService.GetByClienteCpfCnpjAsync("12345678901");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
