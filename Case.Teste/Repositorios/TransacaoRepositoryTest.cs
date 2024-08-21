using Case.Data;
using Case.Dominio.Entidades;
using Case.Repositorios;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class TransacaoRepositoryTests
{
    private readonly TransacaoRepository _transacaoRepository;
    private readonly ApplicationDbContext _context;

    public TransacaoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TransacaoRepositoryTests")
            .Options;

        _context = new ApplicationDbContext(options);
        _transacaoRepository = new TransacaoRepository(_context);
    }

    private void ClearDatabase()
    {
        _context.Transacoes.RemoveRange(_context.Transacoes);
        _context.SaveChanges();
    }

    [Fact]
    public async Task AddAsync_TransacaoValida_TransacaoAdicionada()
    {
        // Arrange
        var transacao = new Transacao { Id = 1, InvestimentoId = 1, Preco = 100.0m, Data = DateTime.UtcNow };

        // Act
        await _transacaoRepository.AddAsync(transacao);

        // Assert
        var transacaoFromDb = await _context.Transacoes.FindAsync(transacao.Id);
        Assert.NotNull(transacaoFromDb);
        Assert.Equal(transacao.Preco, transacaoFromDb.Preco);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByIdAsync_TransacaoExistente_RetornaTransacao()
    {
        // Arrange
        var transacao = new Transacao { Id = 1, InvestimentoId = 1, Preco = 100.0m, Data = DateTime.UtcNow };
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();

        // Act
        var transacaoFromDb = await _transacaoRepository.GetByIdAsync(transacao.Id);

        // Assert
        Assert.NotNull(transacaoFromDb);
        Assert.Equal(transacao.Id, transacaoFromDb.Id);
        ClearDatabase();
    }

    [Fact]
    public async Task GetByInvestimentoIdAsync_TransacoesExistentes_RetornaTransacoes()
    {
        // Arrange
        var investimentoId = 1;
        var transacao1 = new Transacao { Id = 1, InvestimentoId = investimentoId, Preco = 100.0m, Data = DateTime.UtcNow };
        var transacao2 = new Transacao { Id = 2, InvestimentoId = investimentoId, Preco = 200.0m, Data = DateTime.UtcNow.AddDays(1) };

        _context.Transacoes.AddRange(transacao1, transacao2);
        await _context.SaveChangesAsync();

        // Act
        var transacoes = await _transacaoRepository.GetByInvestimentoIdAsync(investimentoId);

        // Assert
        Assert.Equal(2, transacoes.Count());
        Assert.Contains(transacoes, t => t.Id == transacao1.Id);
        Assert.Contains(transacoes, t => t.Id == transacao2.Id);
        ClearDatabase();
    }
}
