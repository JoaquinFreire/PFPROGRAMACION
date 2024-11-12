// Define un record llamado "CreateCustomerCommand"…
// Los records son tipos de datos inmutables que se usan comúnmente para representar datos transferidos…
public record CreateCustomerCommand(
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
    bool IsVerified = false,
    // Declara las propiedades de "CreateCustomerCommand", representando los datos necesarios para crear un cliente… 
    // Incluye datos personales, dirección y el estado de verificación del cliente…
    // "IsVerified" se inicializa en false por defecto, y "Token" es opcional (por defecto es null)…
    string Token = null) : IRequest<ErrorOr<Guid>>;
    // Hereda de la interfaz "IRequest<ErrorOr<Guid>>"…
    // Esto indica que el comando se manejará como una solicitud que devuelve un resultado tipo "ErrorOr<Guid>"…
    // "ErrorOr<Guid>" significa que el resultado puede ser un error o un GUID (ID) del cliente creado…
