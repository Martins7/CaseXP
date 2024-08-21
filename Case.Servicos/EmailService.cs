using FluentEmail.Core;


namespace Case.Servicos
{
    public class EmailService : IEmailService
    {

        private readonly IFluentEmail _email;

        public EmailService(IFluentEmail email)
        {
            _email = email;
        }

        public async Task EnviarEmailAsync(string destinatario, string assunto, string mensagem)
        {
            await _email
                .To(destinatario)
                .Subject(assunto)
                .Body(mensagem)
                .SendAsync();
        }
    }
}
