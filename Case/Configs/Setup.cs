using Case.Data;
using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Repositorios;
using Case.Dominio.Interfaces.Servicos;
using Case.Repositorios;
using Case.Servicos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Case.Configs
{
    public static class Setup
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connections = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    connections.DBConnection,
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
                ));


            var smtpSettings = configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            services
                .AddFluentEmail("brunomartins311@live.com")
                .AddSmtpSender(smtpSettings.Host, smtpSettings.Port, smtpSettings.UserName, smtpSettings.Password);

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IInvestimentoRepository, InvestimentoRepository>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IInvestimentoService, InvestimentoService>();
            services.AddScoped<ITransacaoService, TransacaoService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddHostedService<NotificarProdutoAVencerService>();
        }
    }
}
