using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Case.Servicos;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class ClienteServiceTests
{
    private readonly Mock<IClienteRepository> _mockClienteRepository;
    private readonly ClienteService _clienteService;

    public ClienteServiceTests()
    {
        _mockClienteRepository = new Mock<IClienteRepository>();
        _clienteService = new ClienteService(_mockClienteRepository.Object);
    }

    [Fact]
    public async Task AdicionarClienteAsync_ClienteValido_RetornaCliente()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        _mockClienteRepository.Setup(repo => repo.AddAsync(cliente))
                              .Returns(Task.CompletedTask);

        // Act
        //var resultado = await _clienteService.AddAsync(cliente);

        //// Assert
        //Assert.NotNull(resultado);
        //Assert.Equal(cliente.CpfCnpj, resultado.CpfCnpj);
        //_mockClienteRepository.Verify(repo => repo.AddAsync(cliente), Times.Once);
    }

    [Fact]
    public async Task ObterClientePorIdAsync_ClienteExistente_RetornaCliente()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        _mockClienteRepository.Setup(repo => repo.GetByIdAsync(cliente.Id))
                              .ReturnsAsync(cliente);

        // Act
        var resultado = await _clienteService.GetByIdAsync(cliente.Id);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(cliente.Id, resultado.Id);
        _mockClienteRepository.Verify(repo => repo.GetByIdAsync(cliente.Id), Times.Once);
    }
}
