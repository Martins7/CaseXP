using FluentEmail.Core;
using FluentEmail.Core.Models;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Case.Servicos.Tests
{
    public class EmailServiceTests
    {
        private readonly Mock<IFluentEmail> _mockEmail;
        private readonly EmailService _emailService;

        public EmailServiceTests()
        {
            _mockEmail = new Mock<IFluentEmail>();
            _emailService = new EmailService(_mockEmail.Object);
        }

        [Fact]
        public async Task EnviarEmailAsync_InformacoesValidas_EmailEnviado()
        {
            // Arrange
            var destinatario = "test@example.com";
            var assunto = "Test Subject";
            var mensagem = "Test Message";

            _mockEmail.Setup(e => e.To(destinatario)).Returns(_mockEmail.Object);
            _mockEmail.Setup(e => e.Subject(assunto)).Returns(_mockEmail.Object);
            _mockEmail.Setup(e => e.Body(mensagem, false)).Returns(_mockEmail.Object);
            // Act
            await _emailService.EnviarEmailAsync(destinatario, assunto, mensagem);

            // Assert
            _mockEmail.Verify(e => e.To(destinatario), Times.Once);
            _mockEmail.Verify(e => e.Subject(assunto), Times.Once);
            _mockEmail.Verify(e => e.Body(mensagem, false), Times.Once);
            _mockEmail.Verify(e => e.SendAsync(null), Times.Once);
        }
    }
}
