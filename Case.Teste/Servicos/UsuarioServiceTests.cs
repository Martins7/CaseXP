using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Case.Repositorios;
using Moq;
using Xunit;

namespace Case.Servicos.Tests
{
    public class UsuarioServiceTests
    {
        private readonly UsuarioService _usuarioService;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;

        public UsuarioServiceTests()
        {
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_mockUsuarioRepository.Object);
        }

        [Fact]
        public async Task GetByIdAsync_DeveRetornarUsuario()
        {
            // Arrange
            var usuarioId = 1;
            var usuario = new Usuario { Id = usuarioId, CpfCnpj = "12345678901" };
            _mockUsuarioRepository.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.GetByIdAsync(usuarioId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuarioId, result.Id);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarUsuarios()
        {
            // Arrange
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, CpfCnpj = "12345678901" },
                new Usuario { Id = 2, CpfCnpj = "09876543210" }
            };
            _mockUsuarioRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(usuarios);

            // Act
            var result = await _usuarioService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateAsync_DeveAdicionarUsuario()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, CpfCnpj = "12345678901" };
            _mockUsuarioRepository.Setup(repo => repo.AddAsync(usuario)).Returns(Task.CompletedTask);

            // Act
            await _usuarioService.CreateAsync(usuario);

            // Assert
            _mockUsuarioRepository.Verify(repo => repo.AddAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_DeveAtualizarUsuario()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, CpfCnpj = "12345678901" };
            _mockUsuarioRepository.Setup(repo => repo.UpdateAsync(usuario)).Returns(Task.CompletedTask);

            // Act
            await _usuarioService.UpdateAsync(usuario);

            // Assert
            _mockUsuarioRepository.Verify(repo => repo.UpdateAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeveRemoverUsuario()
        {
            // Arrange
            var usuarioId = 1;
            _mockUsuarioRepository.Setup(repo => repo.DeleteAsync(usuarioId)).Returns(Task.CompletedTask);

            // Act
            await _usuarioService.DeleteAsync(usuarioId);

            // Assert
            _mockUsuarioRepository.Verify(repo => repo.DeleteAsync(usuarioId), Times.Once);
        }

        [Fact]
        public async Task GetByCpfCnpjAsync_DeveRetornarUsuario()
        {
            // Arrange
            var cpfCnpj = "12345678901";
            var usuario = new Usuario { Id = 1, CpfCnpj = cpfCnpj };
            _mockUsuarioRepository.Setup(repo => repo.GetByCpfCnpjAsync(cpfCnpj)).ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.GetByCpfCnpjAsync(cpfCnpj);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cpfCnpj, result.CpfCnpj);
        }
    }
}
