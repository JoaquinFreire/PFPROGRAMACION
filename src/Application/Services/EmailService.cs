using System.Net.Mail;

namespace Application.Services;

public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;  // Ejemplo de cliente SMTP, esto puede variar

        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var mailMessage = new MailMessage("noreply@escmbf.com", email, subject, body);
            
            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                throw new InvalidOperationException("No se pudo enviar el correo", ex);
            }
        }
    }