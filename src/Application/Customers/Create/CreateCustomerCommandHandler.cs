using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using Domain.DomainErrors;

namespace Application.Customers.Create;

// Manejador para crear un nuevo cliente
public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Guid>>
{
    private readonly ICustomerRepository _customerRepository; // Repositorio para acceso a la entidad Customer.
    private readonly IUnitOfWork _unitOfWork; // Unidad de trabajo para transacciones de base de datos.

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    // Método que maneja la creación del cliente.
    public async Task<ErrorOr<Guid>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        // Validación del formato del número de teléfono.
        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            return Errors.Customer.PhoneNumberWithBadFormat; // Devuelve error si el formato es incorrecto.
        }

        // Validación de la dirección del cliente.
        if (Address.Create(command.Country, command.Line1, command.Line2, command.City,
                    command.State, command.ZipCode) is not Address address)
        {
          return Errors.Customer.AddressWithBadFormat; // Devuelve error si la dirección es inválida.
        }

        // Generación del token único para el cliente.
        string token = Guid.NewGuid().ToString(); // Crea un GUID como token de verificación.

        // Creación del objeto Customer
        var customer = new Customer(
            new CustomerId(Guid.NewGuid()), // Nuevo ID de cliente.
            command.Name,
            command.LastName, 
            command.Email,
            phoneNumber,
            address, // Dirección validada.
            false,
            token
        );

        // Agrega el cliente al repositorio.
        _customerRepository.Add(customer);

        // Guarda los cambios en la base de datos.
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Retorna el ID del cliente recién creado.
        return customer.Id.Value;
    }
}
