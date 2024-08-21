using Case.Data;
using Case.Dominio.Entidades;
using Case.Repositorios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class InvestimentoRepositoryTests
{
    private readonly InvestimentoRepository _investimentoRepository;
    private readonly ApplicationDbContext _context;

    public InvestimentoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "InvestimentoRepositoryTests")
            .Options;

        _context = new ApplicationDbContext(options);
        _investimentoRepository = new InvestimentoRepository(_context);
    }

    private void ClearDatabase()
    {
        _context.Investimentos.RemoveRange(_context.Investimentos);
        _context.Produtos.RemoveRange(_context.Produtos);
        _context.Clientes.RemoveRange(_context.Clientes);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddAsync_InvestimentoValido_InvestimentoAdicionado()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        var produto = new Produto { Id = 1, Nome = "Produto1" };
        var investimento = new Investimento { Id = 1, Cliente = cliente, Produto = produto, Quantidade = 10, Preco = 100.00M };

        _context.Clientes.Add(cliente);
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        // Act
        var investimentoId = await _investimentoRepository.AddAsync(investimento);

        // Assert
        var investimentoFromDb = await _context.Investimentos.FindAsync(investimentoId);
        Assert.NotNull(investimentoFromDb);
        Assert.Equal(investimento.Quantidade, investimentoFromDb.Quantidade);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByIdAsync_InvestimentoExistente_RetornaInvestimento()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        var produto = new Produto { Id = 1, Nome = "Produto1" };
        var investimento = new Investimento { Id = 1, Cliente = cliente, Produto = produto, Quantidade = 10, Preco = 100.00M };

        _context.Clientes.Add(cliente);
        _context.Produtos.Add(produto);
        _context.Investimentos.Add(investimento);
        await _context.SaveChangesAsync();

        // Act
        var investimentoFromDb = await _investimentoRepository.GetByIdAsync(investimento.Id);

        // Assert
        Assert.NotNull(investimentoFromDb);
        Assert.Equal(investimento.Id, investimentoFromDb.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task UpdateAsync_InvestimentoExistente_InvestimentoAtualizado()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        var produto = new Produto { Id = 1, Nome = "Produto1" };
        var investimento = new Investimento { Id = 1, Cliente = cliente, Produto = produto, Quantidade = 10, Preco = 100.00M };

        _context.Clientes.Add(cliente);
        _context.Produtos.Add(produto);
        _context.Investimentos.Add(investimento);
        await _context.SaveChangesAsync();

        investimento.Quantidade = 20;

        // Act
        await _investimentoRepository.UpdateAsync(investimento);
        var investimentoFromDb = await _context.Investimentos.FindAsync(investimento.Id);

        // Assert
        Assert.NotNull(investimentoFromDb);
        Assert.Equal(investimento.Quantidade, investimentoFromDb.Quantidade);
        ClearDatabase();
    }

    [Fact]
    public async Task DeleteAsync_InvestimentoExistente_InvestimentoRemovido()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        var produto = new Produto { Id = 1, Nome = "Produto1" };
        var investimento = new Investimento { Id = 1, Cliente = cliente, Produto = produto, Quantidade = 10, Preco = 100.00M };

        _context.Clientes.Add(cliente);
        _context.Produtos.Add(produto);
        _context.Investimentos.Add(investimento);
        await _context.SaveChangesAsync();

        // Act
        await _investimentoRepository.DeleteAsync(investimento.Id);
        var investimentoFromDb = await _context.Investimentos.FindAsync(investimento.Id);

        // Assert
        Assert.Null(investimentoFromDb);
        ClearDatabase();
    }

    [Fact]
    public async Task GetAllAsync_InvestimentosExistentes_RetornaTodosInvestimentos()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        var produto1 = new Produto { Id = 1, Nome = "Produto1" };
        var produto2 = new Produto { Id = 2, Nome = "Produto2" };
        var investimento1 = new Investimento { Id = 1, Cliente = cliente, Produto = produto1, Quantidade = 10, Preco = 100.00M };
        var investimento2 = new Investimento { Id = 2, Cliente = cliente, Produto = produto2, Quantidade = 20, Preco = 200.00M };

        _context.Clientes.Add(cliente);
        _context.Produtos.AddRange(produto1, produto2);
        _context.Investimentos.AddRange(investimento1, investimento2);
        await _context.SaveChangesAsync();

        // Act
        var investimentos = await _investimentoRepository.GetAllAsync();

        // Assert
        Assert.Equal(2, investimentos.Count());
        Assert.Contains(investimentos, i => i.Id == investimento1.Id);
        Assert.Contains(investimentos, i => i.Id == investimento2.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByClienteCpfCnpjAsync_ClienteExistente_RetornaInvestimentos()
    {
        // Arrange
        var cliente = new Cliente { Id = 1, CpfCnpj = "12345678901" };
        var produto1 = new Produto { Id = 1, Nome = "Produto1" };
        var produto2 = new Produto { Id = 2, Nome = "Produto2" };
        var investimento1 = new Investimento { Id = 1, Cliente = cliente, Produto = produto1, Quantidade = 10, Preco = 100.00M };
        var investimento2 = new Investimento { Id = 2, Cliente = cliente, Produto = produto2, Quantidade = 20, Preco = 200.00M };

        _context.Clientes.Add(cliente);
        _context.Produtos.AddRange(produto1, produto2);
        _context.Investimentos.AddRange(investimento1, investimento2);
        await _context.SaveChangesAsync();

        // Act
        var investimentos = await _investimentoRepository.GetByClienteCpfCnpjAsync(cliente.CpfCnpj);

        // Assert
        Assert.Equal(2, investimentos.Count());
        Assert.Contains(investimentos, i => i.Id == investimento1.Id);
        Assert.Contains(investimentos, i => i.Id == investimento2.Id);
        ClearDatabase();
    }
}
