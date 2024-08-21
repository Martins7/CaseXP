using Case.Data;
using Case.Dominio.Entidades;
using Case.Repositorios;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ClienteRepositoryTests
{
    private readonly ClienteRepository _clienteRepository;
    private readonly ApplicationDbContext _context;

    public ClienteRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ClienteRepositoryTests")
            .Options;

        _context = new ApplicationDbContext(options);
        _clienteRepository = new ClienteRepository(_context);
    }
    private void ClearDatabase()
    {
        _context.Clientes.RemoveRange(_context.Clientes);
        _context.SaveChanges();
    }
    [Fact]
    public async Task AddAsync_ClienteValido_ClienteAdicionado()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };

        // Act
        await _clienteRepository.AddAsync(cliente);

        // Assert
        var clienteFromDb = await _context.Clientes.FindAsync(cliente.Id);
        Assert.NotNull(clienteFromDb);
        Assert.Equal(cliente.CpfCnpj, clienteFromDb.CpfCnpj);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByIdAsync_ClienteExistente_RetornaCliente()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Act
        var clienteFromDb = await _clienteRepository.GetByIdAsync(cliente.Id);

        // Assert
        Assert.NotNull(clienteFromDb);
        Assert.Equal(cliente.Id, clienteFromDb.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task UpdateAsync_ClienteExistente_ClienteAtualizado()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        cliente.CpfCnpj = "09876543210";

        // Act
        await _clienteRepository.UpdateAsync(cliente);
        var clienteFromDb = await _context.Clientes.FindAsync(cliente.Id);

        // Assert
        Assert.NotNull(clienteFromDb);
        Assert.Equal(cliente.CpfCnpj, clienteFromDb.CpfCnpj);

        ClearDatabase();

    }

    [Fact]
    public async Task DeleteAsync_ClienteExistente_ClienteRemovido()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Act
        await _clienteRepository.DeleteAsync(cliente.Id);
        var clienteFromDb = await _context.Clientes.FindAsync(cliente.Id);

        // Assert
        Assert.Null(clienteFromDb);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByCpfCnpjAsync_ClienteExistente_RetornaCliente()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Act
        var clienteFromDb = await _clienteRepository.GetByCpfCnpjAsync(cliente.CpfCnpj);

        // Assert
        Assert.NotNull(clienteFromDb);
        Assert.Equal(cliente.CpfCnpj, clienteFromDb.CpfCnpj);
        ClearDatabase();
    }
}
