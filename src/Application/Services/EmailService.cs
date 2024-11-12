using System.Net.Mail;

namespace Application.Services;


// Se define la clase EmailService que implementa la interfaz IEmailService.
// Esta clase es responsable de enviar correos electrónicos a través de un cliente SMTP
public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;  // Ejemplo de cliente SMTP, esto puede variar

        // Constructor que recibe un SmtpClient como dependencia.
        // El cliente SMTP se utiliza para enviar correos electrónicos

        public EmailService(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

         // Metodo asincrono para enviar un correo electrónico.
        // Recibe como parámetros la dirección de correo, el asunto y el cuerpo del mensaje

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            // Se crea un nuevo objeto MailMessage que representa el correo a enviar.
            // El remitente es "noreply@escmbf.com", el destinatario es el parámetro "email", y se incluyen el asunto y el cuerpo.
            var mailMessage = new MailMessage("noreply@escmbf.com", email, subject, body);
            
            try
            {
                 // Se envía el correo de forma asincrónica usando el cliente SMTP.
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)

            {
                // Si ocurre un error, se lanza una excepción con un mensaje de error específico.
                // La excepción original se incluye para poder diagnosticar el problema.
                // Manejo de errores
                throw new InvalidOperationException("No se pudo enviar el correo", ex);
            }
        }
    }