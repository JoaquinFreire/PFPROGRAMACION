using Customers.Common;
using Domain.Customers;

namespace Application.Customers.GetById;

internal sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ErrorOr<CustomerResponse>>
{
    private readonly ICustomerRepository _customerRepository;

    // Constructor que recibe el repositorio de clientes.
    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    // Maneja la solicitud de obtener un cliente por su Id.
    public async Task<ErrorOr<CustomerResponse>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        // Si no se encuentra el cliente, se devuelve un error de "No encontrado".
        if (await _customerRepository.GetByIdAsync(new CustomerId(query.Id)) is not Customer customer)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provided Id was not found.");
        }

        // Mapea los datos del cliente a la respuesta, excluyendo el token.
        return new CustomerResponse(
            customer.Id.Value,
            customer.FullName,
            customer.Email,
            customer.PhoneNumber.Value,
            new AddressResponse(customer.Address.Country,
                customer.Address.Line1,
                customer.Address.Line2,
                customer.Address.City,
                customer.Address.State,
                customer.Address.ZipCode),
            /* customer.Active); */
            customer.IsVerified
        );
    }
}
