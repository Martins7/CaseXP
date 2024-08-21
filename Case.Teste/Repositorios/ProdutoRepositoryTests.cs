using Case.Data;
using Case.Dominio.Entidades;
using Case.Repositorios;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ProdutoRepositoryTests
{
    private readonly ProdutoRepository _produtoRepository;
    private readonly ApplicationDbContext _context;

    public ProdutoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "ProdutoRepositoryTests")
            .Options;

        _context = new ApplicationDbContext(options);
        _produtoRepository = new ProdutoRepository(_context);
    }

    private void ClearDatabase()
    {
        _context.Produtos.RemoveRange(_context.Produtos);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddAsync_ProdutoValido_ProdutoAdicionado()
    {
        // Arrange
        var produto = new Produto { Id = 1, Nome = "Produto1", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(1) };

        // Act
        await _produtoRepository.AddAsync(produto);

        // Assert
        var produtoFromDb = await _context.Produtos.FindAsync(produto.Id);
        Assert.NotNull(produtoFromDb);
        Assert.Equal(produto.Nome, produtoFromDb.Nome);
        ClearDatabase();
    }

    [Fact]
    public async Task DeactivateAsync_ProdutoExistente_ProdutoDesativado()
    {
        // Arrange
        var produto = new Produto { Id = 1, Nome = "Produto1", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(1) };
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        // Act
        await _produtoRepository.DeactivateAsync(produto.Id);
        var produtoFromDb = await _context.Produtos.FindAsync(produto.Id);

        // Assert
        Assert.NotNull(produtoFromDb);
        Assert.False(produtoFromDb.Disponivel);
        ClearDatabase();
    }

    [Fact]
    public async Task GetAllAsync_ProdutosExistentes_RetornaTodosProdutos()
    {
        // Arrange
        var produto1 = new Produto { Id = 1, Nome = "Produto1", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(1) };
        var produto2 = new Produto { Id = 2, Nome = "Produto2", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(2) };

        _context.Produtos.AddRange(produto1, produto2);
        await _context.SaveChangesAsync();

        // Act
        var produtos = await _produtoRepository.GetAllAsync();

        // Assert
        Assert.Equal(2, produtos.Count());
        Assert.Contains(produtos, p => p.Id == produto1.Id);
        Assert.Contains(produtos, p => p.Id == produto2.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByIdAsync_ProdutoExistente_RetornaProduto()
    {
        // Arrange
        var produto = new Produto { Id = 1, Nome = "Produto1", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(1) };
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        // Act
        var produtoFromDb = await _produtoRepository.GetByIdAsync(produto.Id);

        // Assert
        Assert.NotNull(produtoFromDb);
        Assert.Equal(produto.Id, produtoFromDb.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByVencimentoAsync_ProdutosComDataVencimento_RetornaProdutosValidos()
    {
        // Arrange
        var produto1 = new Produto { Id = 1, Nome = "Produto1", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(1) };
        var produto2 = new Produto { Id = 2, Nome = "Produto2", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(-1) };

        _context.Produtos.AddRange(produto1, produto2);
        await _context.SaveChangesAsync();

        var dataLimite = DateTime.UtcNow;

        // Act
        var produtos = await _produtoRepository.GetByVencimentoAsync(dataLimite);

        // Assert
        Assert.Single(produtos);
        Assert.Contains(produtos, p => p.Id == produto1.Id);
        Assert.DoesNotContain(produtos, p => p.Id == produto2.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task UpdateAsync_ProdutoExistente_ProdutoAtualizado()
    {
        // Arrange
        var produto = new Produto { Id = 1, Nome = "Produto1", Disponivel = true, DataVencimento = DateTime.UtcNow.AddMonths(1) };
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        produto.Nome = "Produto Atualizado";

        // Act
        await _produtoRepository.UpdateAsync(produto);
        var produtoFromDb = await _context.Produtos.FindAsync(produto.Id);

        // Assert
        Assert.NotNull(produtoFromDb);
        Assert.Equal(produto.Nome, produtoFromDb.Nome);
        ClearDatabase();
    }
}
