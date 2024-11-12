using Customers.Common;

namespace Application.Customers.GetAll;
// Se define un "record" llamado GetAllCustomersQuery, que representa una consulta para obtener todos los clientes.
// Un record es una estructura que se utiliza para almacenar datos inmutables de manera compacta.
public record GetAllCustomersQuery() : IRequest<ErrorOr<IReadOnlyList<CustomerResponse>>>;