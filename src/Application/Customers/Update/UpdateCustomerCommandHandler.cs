using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Application.Customers.Update;

internal sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        // Verificar si el cliente existe
        if (!await _customerRepository.ExistsAsync(new CustomerId(command.Id)))
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provided Id was not found.");
        }

        // Validar número de teléfono
        if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
        {
            return Error.Validation("Customer.PhoneNumber", "Phone number has not valid format.");
        }

        // Validar la dirección
        if (Address.Create(command.Country, command.Line1, command.Line2, command.City,
                    command.State, command.ZipCode) is not Address address)
        {
            return Error.Validation("Customer.Address", "Address is not valid.");
        }

        // Crear el cliente con los datos actualizados
        Customer customer = Customer.UpdateCustomer(command.Id, command.Name,
            command.LastName,
            command.Email,
            phoneNumber,
            address,
            command.IsVerified);

        // Lógica para la actualización del token si es necesario
        if (command.IsVerified)
        {
            // Si el cliente se verifica, se elimina el token.
            customer.Verify();  // El método Verify eliminará el token y marcará al cliente como verificado.
        }

        // Actualizar el cliente en el repositorio
        _customerRepository.Update(customer);

        // Guardar los cambios en la base de datos
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
