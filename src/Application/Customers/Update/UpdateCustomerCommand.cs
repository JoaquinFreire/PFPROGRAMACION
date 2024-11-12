namespace Application.Customers.Update;

 // Se define un "record" llamado UpdateCustomerCommand, que representa un comando para actualizar la informacion de un cliente.
// Un record es una estructura de datos inmutable que se usa para almacenar datos de manera compacta.
public record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode,
    bool IsVerified,
    string? Token) : IRequest<ErrorOr<Unit>>; // Se agrega el Token como opcional
    // El comando devuelve un resultado que puede ser un Error o un Unit (indica éxito sin retorno de datos).
