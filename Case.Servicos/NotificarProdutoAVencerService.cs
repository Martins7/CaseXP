using Case.Data;
using Case.Dominio.Enums;
using Case.Dominio.Interfaces.Servicos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Case.Servicos
{
    public class NotificarProdutoAVencerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public NotificarProdutoAVencerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var expirationThreshold = DateTime.UtcNow.AddDays(7);

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();
                        var _produtoService = scope.ServiceProvider.GetRequiredService<IProdutoService>();
                        var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        var produtosAVencer = await _produtoService.GetProdutosAVencer(7);

                        if (produtosAVencer.Any())
                        {
                            var usuarios = await _usuarioService.GetAllAsync();
                            var adminEmails = usuarios.Where(u => u.Papel == PapelUsuario.Admin)
                                 .Select(u => u.Email)
                                 .ToList();

                            var subject = "Notificação de Produtos Próximos do Vencimento";

                            var body = "Os seguintes produtos estão próximos do vencimento:\n" +
                                       string.Join("\n", produtosAVencer.Select(p => $"{p.Nome} - Vence em {p.DataVencimento:d}"));

                            foreach (var email in adminEmails)
                            {
                                await _emailService.EnviarEmailAsync(email, subject, body);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    
                }

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }
    }
}
