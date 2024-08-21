namespace Case.Dominio.Interfaces.Servicos
{
    public interface INotificationService
    {
        Task EnviaNotificacaoDiariaProdutoExpiradoAsync();
    }
}
