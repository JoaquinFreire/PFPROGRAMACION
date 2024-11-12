namespace Application.Customers.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
 // Se define la clase UpdateCustomerCommandValidator, que valida los datos del comando UpdateCustomerCommand.
    // Esta clase extiende de AbstractValidator, que es una clase base para la validacion de objetos en FluentValidation.
    public UpdateCustomerCommandValidator()
    {
         // Constructor donde se definen las reglas de validación para las propiedades del comando.
         RuleFor(r => r.Id)
            .NotEmpty();

        // Regla de validación para el campo "Id": no puede estar vacío.
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.LastName)
             .NotEmpty()
             .MaximumLength(50)
             .WithName("Last Name");

        RuleFor(r => r.Email)
             .NotEmpty()
             .EmailAddress()
             .MaximumLength(255);

        RuleFor(r => r.PhoneNumber)
             .NotEmpty()
             .MaximumLength(9)
             .WithName("Phone Number");

        RuleFor(r => r.Country)
            .NotEmpty()
            .MaximumLength(3);

        RuleFor(r => r.Line1)
            .NotEmpty()
            .MaximumLength(20)
            .WithName("Addres Line 1");

        RuleFor(r => r.Line2)
            .MaximumLength(20)
            .WithName("Addres Line 2");

        RuleFor(r => r.City)
            .NotEmpty()
            .MaximumLength(40);

        RuleFor(r => r.State)
            .NotEmpty()
            .MaximumLength(40);

            // Regla para el campo "ZipCode": no puede estar vacío, longitud máxima de 10 caracteres, y se renombra a "Zip Code" en los mensajes de error.
        RuleFor(r => r.ZipCode)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Zip Code");

        /* RuleFor(r => r.Active)
            .NotNull(); */

            // Regla para el campo "IsVerified": no puede ser nulo.
            RuleFor(r => r.IsVerified).NotNull();
    }
}