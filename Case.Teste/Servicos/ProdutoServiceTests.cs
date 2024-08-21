using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Case.Servicos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Case.Servicos.Tests
{
    public class ProdutoServiceTests
    {
        private readonly ProdutoService _produtoService;
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;

        public ProdutoServiceTests()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_mockProdutoRepository.Object);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarProdutos()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Id = 1 },
                new Produto { Id = 2 }
            };
            _mockProdutoRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produtos);

            // Act
            var result = await _produtoService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarProduto()
        {
            // Arrange
            var produto = new Produto { Id = 1 };
            _mockProdutoRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

            // Act
            var result = await _produtoService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task AddAsync_DeveAdicionarProduto()
        {
            // Arrange
            var produto = new Produto { Id = 1 };

            // Act
            await _produtoService.AddAsync(produto);

            // Assert
            _mockProdutoRepository.Verify(repo => repo.AddAsync(produto), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarProduto()
        {
            // Arrange
            var produto = new Produto { Id = 1 };

            // Act
            await _produtoService.UpdateAsync(produto);

            // Assert
            _mockProdutoRepository.Verify(repo => repo.UpdateAsync(produto), Times.Once);
        }

        [Fact]
        public async Task DesativarProdutoAsync_DeveDesativarProduto()
        {
            // Arrange
            var produtoId = 1;

            // Act
            await _produtoService.DesativarProdutoAsync(produtoId);

            // Assert
            _mockProdutoRepository.Verify(repo => repo.DeactivateAsync(produtoId), Times.Once);
        }

        [Fact]
        public async Task GetProdutosAVencer_DeveRetornarProdutosAVencer()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, DataVencimento = DateTime.Now.AddDays(5) },
                new Produto { Id = 2, DataVencimento = DateTime.Now.AddDays(15) }
            };
            var dias = 7;
            var dataLimite = DateTime.Now.AddDays(dias);
            _mockProdutoRepository.Setup(repo => repo.GetByVencimentoAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(produtos.Where(p => p.DataVencimento <= dataLimite));

            // Act
            var result = await _produtoService.GetProdutosAVencer(dias);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            Assert.Equal(1, result.First().Id);
        }
    }
}
