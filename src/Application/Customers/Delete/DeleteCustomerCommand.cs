namespace Application.Customers.Delete;

public record DeleteCustomerCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
// Define el comando para eliminar un cliente usando su ID único.
// Implementa la interfaz IRequest<ErrorOr<Unit>> para que pueda ser manejado por un mediador,
// y devuelve un resultado que puede ser un error o una unidad vacía.