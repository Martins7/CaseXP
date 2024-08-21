namespace Case.Servicos
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string destinatario, string assunto, string mensagem);
    }
}
