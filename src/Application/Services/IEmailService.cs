namespace Application.Services;


    // Se define la interfaz IEmailService.
    // Esta interfaz declara un contrato para los servicios de envío de correos electrónicos.
    // Cualquier clase que implemente esta interfaz debe proporcionar una implementación del método SendEmailAsync.
public interface IEmailService
{

    // Metodo asincrono para enviar un correo electrónico.
    // Recibe como parámetros la dirección de correo electrónico del destinatario (email), el asunto (subject) y el cuerpo del mensaje (body).

    Task SendEmailAsync(string email, string subject, string body);
}
