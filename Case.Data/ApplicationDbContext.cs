using Case.Dominio.Entidades;
using Case.Dominio.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Case.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Investimento> Investimentos { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUsuario(modelBuilder);
            ConfigureProduto(modelBuilder);
            ConfigureCliente(modelBuilder);
            ConfigureInvestimento(modelBuilder);
            ConfigureTransacao(modelBuilder);
            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public void Initialize()
        {
            Database.Migrate();
        }
        private void ConfigureUsuario(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.CpfCnpj)
                   .HasMaxLength(14)
                   .IsRequired();
                
                entity.Property(u => u.Senha)
                    .HasMaxLength(256)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .HasMaxLength(256)
                    .IsRequired();

                entity.Property(u => u.Papel)
                   .HasConversion<int>()
                   .IsRequired();
            });
        }

        private void ConfigureProduto(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produtos");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();

                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Valor)
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Disponivel)
                    .IsRequired();
            });
        }

        private void ConfigureCliente(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");

                entity.Property(c => c.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();

                entity.Property(c => c.CpfCnpj)
                    .IsRequired()
                    .HasMaxLength(14);

                entity.HasOne(c => c.Usuario)
                      .WithOne()
                      .HasForeignKey<Cliente>(c => c.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);


                entity.HasMany(c => c.Investimentos)
                   .WithOne()
                   .HasForeignKey(i => i.ClienteId)
                   .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureInvestimento(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Investimento>(entity =>
            {
                entity.ToTable("Investimentos");
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();

                entity.Property(e => e.Preco)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(i => i.Produto)
                    .WithMany()
                    .HasForeignKey(i => i.ProdutoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.Cliente)
                    .WithMany(c => c.Investimentos)
                    .HasForeignKey(i => i.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureTransacao(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.ToTable("Transacoes");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();

                entity.Property(t => t.Quantidade)
                    .HasColumnType("decimal(18,2)");

                entity.Property(t => t.Preco)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(t => t.Investimento)
                    .WithMany()
                    .HasForeignKey(t => t.InvestimentoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
        private void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { Id = 1, Email = "admin@seed.com", Senha="", Papel = PapelUsuario.Admin, CpfCnpj = "987654321", Nome = "Admin" },
                new Usuario { Id = 2, Email = "Operacao@seed.com", Senha = "", Papel = PapelUsuario.Operacao, CpfCnpj = "987654321", Nome = "Operacao" },
                new Usuario { Id = 3, Email = "cliente@seed.com", Senha = "", Papel = PapelUsuario.Cliente, CpfCnpj = "458787878", Nome = "Maria Doe" },
                new Usuario { Id = 4, Email = "cliente2@seed.com", Senha = "", Papel = PapelUsuario.Cliente, CpfCnpj = "125879875", Nome = "John Doe" }
            );
            modelBuilder.Entity<Cliente>().HasData(
                new Cliente { Id = 1, CpfCnpj = "458787878", UsuarioId = 3 },
                new Cliente { Id = 2, CpfCnpj = "125879875", UsuarioId = 4 }
            );
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "HGLG11", DataVencimento = DateTime.UtcNow.AddMonths(1), Disponivel = true, TipoProduto = TipoProduto.FIIs, Valor = 161.88M },
                new Produto { Id = 2, Nome = "MXRF11", DataVencimento = DateTime.UtcNow.AddMonths(2), Disponivel = true, TipoProduto = TipoProduto.FIIs, Valor = 10.42M },
                new Produto { Id = 3, Nome = "CVCB3", DataVencimento = DateTime.UtcNow.AddMonths(4), Disponivel = true, TipoProduto = TipoProduto.Acoes, Valor = 2.30M },
                new Produto { Id = 4, Nome = "PETZ3", DataVencimento = DateTime.UtcNow.AddMonths(2), Disponivel = true, TipoProduto = TipoProduto.Acoes, Valor = 5.16M },
                new Produto { Id = 5, Nome = "AMER3", DataVencimento = DateTime.UtcNow.AddMonths(2), Disponivel = true, TipoProduto = TipoProduto.Acoes, Valor = 0.9M }
            );
            modelBuilder.Entity<Investimento>().HasData(
                new Investimento { Id = 1, ProdutoId = 1, Quantidade = 10, Preco = 1618.80M, DataCompra = DateTime.UtcNow.AddMonths(-3), ClienteId = 1 },
                new Investimento { Id = 2, ProdutoId = 2, Quantidade = 50, Preco = 521.00M, DataCompra = DateTime.UtcNow.AddMonths(-2), ClienteId = 1 },
                new Investimento { Id = 3, ProdutoId = 3, Quantidade = 200, Preco = 460.00M, DataCompra = DateTime.UtcNow.AddMonths(-1), ClienteId = 1 },
                new Investimento { Id = 4, ProdutoId = 4, Quantidade = 100, Preco = 516.00M, DataCompra = DateTime.UtcNow.AddMonths(-1), ClienteId = 1 },
                new Investimento { Id = 5, ProdutoId = 1, Quantidade = 5, Preco = 809.40M, DataCompra = DateTime.UtcNow.AddMonths(-3), ClienteId = 2 },
                new Investimento { Id = 6, ProdutoId = 5, Quantidade = 300, Preco = 270.00M, DataCompra = DateTime.UtcNow.AddMonths(-2), ClienteId = 2 }
            );
            modelBuilder.Entity<Transacao>().HasData(
                new Transacao { Id = 1, InvestimentoId = 1, Quantidade = 5, Preco = 809.55M, Data = DateTime.UtcNow.AddDays(-2), Tipo = TipoTransacao.Compra },
                new Transacao { Id = 2, InvestimentoId = 1, Quantidade = 5, Preco = 809.55M, Data = DateTime.UtcNow.AddDays(-1), Tipo = TipoTransacao.Venda },
                new Transacao { Id = 3, InvestimentoId = 2, Quantidade = 20, Preco = 208.4M, Data = DateTime.UtcNow.AddMonths(-1), Tipo = TipoTransacao.Compra },
                new Transacao { Id = 4, InvestimentoId = 2, Quantidade = 10, Preco = 104.2M, Data = DateTime.UtcNow, Tipo = TipoTransacao.Venda },
                new Transacao { Id = 5, InvestimentoId = 3, Quantidade = 100, Preco = 230, Data = DateTime.UtcNow.AddMonths(-1), Tipo = TipoTransacao.Compra },
                new Transacao { Id = 6, InvestimentoId = 3, Quantidade = 50, Preco = 115, Data = DateTime.UtcNow, Tipo = TipoTransacao.Venda },
                new Transacao { Id = 7, InvestimentoId = 4, Quantidade = 50, Preco = 258, Data = DateTime.UtcNow, Tipo = TipoTransacao.Compra },
                new Transacao { Id = 8, InvestimentoId = 5, Quantidade = 2, Preco = 323.76M, Data = DateTime.UtcNow.AddMonths(-2), Tipo = TipoTransacao.Compra },
                new Transacao { Id = 9, InvestimentoId = 6, Quantidade = 100, Preco = 90, Data = DateTime.UtcNow.AddMonths(-1), Tipo = TipoTransacao.Compra }
            );
        }

    }
}
