using Case.Data;
using Case.Dominio.Entidades;
using Case.Dominio.Enums;
using Case.Repositorios;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class UsuarioRepositoryTests
{
    private readonly UsuarioRepository _usuarioRepository;
    private readonly ApplicationDbContext _context;

    public UsuarioRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "UsuarioRepositoryTests")
            .Options;

        _context = new ApplicationDbContext(options);
        _usuarioRepository = new UsuarioRepository(_context);
    }

    private void ClearDatabase()
    {
        _context.Usuarios.RemoveRange(_context.Usuarios);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddAsync_UsuarioValido_UsuarioAdicionado()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, CpfCnpj = "12345678901", Nome = "Test User", Papel = PapelUsuario.Cliente, Email = "Teste@teste.com", Senha = "" };

        // Act
        await _usuarioRepository.AddAsync(usuario);

        // Assert
        var usuarioFromDb = await _context.Usuarios.FindAsync(usuario.Id);
        Assert.NotNull(usuarioFromDb);
        Assert.Equal(usuario.CpfCnpj, usuarioFromDb.CpfCnpj);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByIdAsync_UsuarioExistente_RetornaUsuario()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, CpfCnpj = "12345678901", Nome = "Test User", Papel = PapelUsuario.Cliente, Email = "Teste@teste.com", Senha = "" };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Act
        var usuarioFromDb = await _usuarioRepository.GetByIdAsync(usuario.Id);

        // Assert
        Assert.NotNull(usuarioFromDb);
        Assert.Equal(usuario.Id, usuarioFromDb.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task UpdateAsync_UsuarioExistente_UsuarioAtualizado()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, CpfCnpj = "12345678901", Nome = "Test User", Papel = PapelUsuario.Cliente, Email = "Teste@teste.com", Senha = "" };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        usuario.Nome = "Updated User";

        // Act
        await _usuarioRepository.UpdateAsync(usuario);
        var usuarioFromDb = await _context.Usuarios.FindAsync(usuario.Id);

        // Assert
        Assert.NotNull(usuarioFromDb);
        Assert.Equal(usuario.Nome, usuarioFromDb.Nome);
        ClearDatabase();
    }

    [Fact]
    public async Task DeleteAsync_UsuarioExistente_UsuarioRemovido()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, CpfCnpj = "12345678901", Nome = "Test User", Papel = PapelUsuario.Cliente, Email = "Teste@teste.com", Senha = "" };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Act
        await _usuarioRepository.DeleteAsync(usuario.Id);
        var usuarioFromDb = await _context.Usuarios.FindAsync(usuario.Id);

        // Assert
        Assert.Null(usuarioFromDb);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByCpfCnpjAsync_UsuarioExistente_RetornaUsuario()
    {
        // Arrange
        var usuario = new Usuario { Id = 1, CpfCnpj = "12345678901", Nome = "Test User", Papel = PapelUsuario.Cliente, Email = "Teste@teste.com", Senha = "" };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        // Act
        var usuarioFromDb = await _usuarioRepository.GetByCpfCnpjAsync(usuario.CpfCnpj);

        // Assert
        Assert.NotNull(usuarioFromDb);
        Assert.Equal(usuario.CpfCnpj, usuarioFromDb.CpfCnpj);
        ClearDatabase();
    }
}
