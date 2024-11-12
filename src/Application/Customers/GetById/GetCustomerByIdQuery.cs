using Customers.Common;

namespace Application.Customers.GetById;
// Se define un "record" llamado GetCustomerByIdQuery, que representa una consulta para obtener un cliente por su ID.
// Un record es una estructura de datos inmutable utilizada para almacenar datos
public record GetCustomerByIdQuery(Guid Id) : IRequest<ErrorOr<CustomerResponse>>;