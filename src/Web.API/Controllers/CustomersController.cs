using Application.Customers.Create;
using Application.Customers.Update;
using Application.Customers.GetById;
using Application.Customers.Delete;
using Application.Customers.GetAll;

namespace Web.API.Controllers;

[Route("customers")] // Define la ruta para los endpoints relacionados con clientes
public class Customers : ApiController
{
    private readonly ISender _mediator; // Mediador para enviar comandos y consultas

    // Constructor que recibe una instancia de ISender (MediatR) para manejar las solicitudes
    public Customers(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator)); // Asegura que mediator no sea nulo
    }

    // Endpoint para obtener todos los clientes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customersResult = await _mediator.Send(new GetAllCustomersQuery()); // Envia la consulta para obtener todos los clientes

        return customersResult.Match(
            customers => Ok(customers), // Si la consulta tiene éxito, devuelve los clientes
            errors => Problem(errors)   // Si hay errores, los retorna como un problema
        );
    }

    // Endpoint para obtener un cliente por su ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var customerResult = await _mediator.Send(new GetCustomerByIdQuery(id)); // Envia la consulta para obtener el cliente por ID

        return customerResult.Match(
            customer => Ok(customer),   // Si la consulta tiene éxito, devuelve el cliente
            errors => Problem(errors)   // Si hay errores, los retorna como un problema
        );
    }

    // Endpoint para crear un nuevo cliente
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
    {
        var createResult = await _mediator.Send(command); // Envia el comando para crear el cliente

        return createResult.Match(
            customerId => Ok(customerId), // Si la creación es exitosa, devuelve el ID del nuevo cliente
            errors => Problem(errors)      // Si hay errores, los retorna como un problema
        );
    }

    // Endpoint para actualizar un cliente existente
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command)
    {
        if (command.Id != id) // Verifica que el ID en la URL coincida con el ID del cuerpo de la solicitud
        {
            List<Error> errors = new()
            {
                Error.Validation("Customer.UpdateInvalid", "The request Id does not match with the url Id.") // Error si los IDs no coinciden
            };
            return Problem(errors); // Retorna el error
        }

        var updateResult = await _mediator.Send(command); // Envia el comando para actualizar el cliente

        return updateResult.Match(
            customerId => NoContent(), // Si la actualización es exitosa, no devuelve contenido
            errors => Problem(errors)  // Si hay errores, los retorna como un problema
        );
    }

    // Endpoint para eliminar un cliente
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteResult = await _mediator.Send(new DeleteCustomerCommand(id)); // Envia el comando para eliminar el cliente

        return deleteResult.Match(
            customerId => NoContent(), // Si la eliminación es exitosa, no devuelve contenido
            errors => Problem(errors)  // Si hay errores, los retorna como un problema
        );
    }
}
