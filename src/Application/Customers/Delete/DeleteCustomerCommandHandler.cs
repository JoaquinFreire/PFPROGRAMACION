using Domain.Customers;
using Domain.Primitives;

namespace Application.Customers.Delete;

// Define un manejador para el comando DeleteCustomerCommand, encargado de eliminar un cliente.
// La clase implementa IRequestHandler para procesar el comando y devolver un resultado de tipo ErrorOr<Unit>.
internal sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

   // Constructor que inyecta el repositorio de clientes y la unidad de trabajo. Lanza una excepción si alguno es null.    
    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    // Método que maneja el comando de eliminación.
    public async Task<ErrorOr<Unit>> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        // Busca al cliente en el repositorio por su Id. Si no lo encuentra, retorna un error de tipo NotFound.
        if (await _customerRepository.GetByIdAsync(new CustomerId(command.Id)) is not Customer customer)
        {
            return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
        }
        // Si el cliente existe, procede a eliminarlo.

        _customerRepository.Delete(customer);

        // Confirma los cambios en la base de datos a través de la unidad de trabajo

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Retorna Unit.Value para indicar una operación exitosa sin retorno.

        return Unit.Value;
    }
}
