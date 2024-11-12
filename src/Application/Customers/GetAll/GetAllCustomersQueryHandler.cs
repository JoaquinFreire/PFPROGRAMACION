using Customers.Common;
using Domain.Customers;

namespace Application.Customers.GetAll;

internal sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ErrorOr<IReadOnlyList<CustomerResponse>>>
{
    private readonly ICustomerRepository _customerRepository;

    // Constructor que recibe el repositorio de clientes.
    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    // Maneja la solicitud de obtener todos los clientes.
    public async Task<ErrorOr<IReadOnlyList<CustomerResponse>>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
    {
        // Recupera la lista de clientes desde el repositorio.
        IReadOnlyList<Customer> customers = await _customerRepository.GetAll();

        // Mapea los clientes a una lista de respuestas
        return customers.Select(customer => new CustomerResponse(
                customer.Id.Value,
                customer.FullName,
                customer.Email,
                customer.PhoneNumber.Value,
                new AddressResponse(customer.Address.Country,  // Dirección del cliente
                    customer.Address.Line1,
                    customer.Address.Line2,
                    customer.Address.City,
                    customer.Address.State,
                    customer.Address.ZipCode),
                    /* customer.Active */ 
                    customer.IsVerified
            )).ToList();  // Convierte el resultado a una lista
    }
}
