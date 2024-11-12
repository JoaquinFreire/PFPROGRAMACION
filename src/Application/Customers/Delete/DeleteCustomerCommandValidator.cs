namespace Application.Customers.Delete;

    // Se define la clase DeleteCustomerCommandValidator, que extiende de AbstractValidator.
    // Esto se usa para validar el comando de eliminación de un cliente (DeleteCustomerCommand)
public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{

     // El constructor de la clase, donde se definen las reglas de validación
    public DeleteCustomerCommandValidator()
    {
        // La regla especifica que el "Id" no debe estar vacio
        RuleFor(r => r.Id)
            .NotEmpty();
    }
}