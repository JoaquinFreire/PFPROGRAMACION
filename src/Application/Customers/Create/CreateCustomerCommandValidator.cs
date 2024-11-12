namespace Application.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
         // Valida que el campo 'Name' no esté vacío y no supere los 50 caracteres
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        // Valida que 'LastName' no esté vacío, no supere los 50 caracteres
        // y lo identifica como "Last Name" en mensajes de error
        RuleFor(r => r.LastName)
             .NotEmpty()
             .MaximumLength(50)
             .WithName("Last Name");

        // Valida que 'Email' no esté vacío, sea una dirección válida y tenga un máximo de 255 caracteres
        RuleFor(r => r.Email)
             .NotEmpty()
             .EmailAddress()
             .MaximumLength(255);
             
        // Valida que 'PhoneNumber' no esté vacío, tenga un máximo de 9 caracteres,
        // y lo etiqueta como "Phone Number" en mensajes de error
        RuleFor(r => r.PhoneNumber)
             .NotEmpty()
             .MaximumLength(9)
             .WithName("Phone Number");

        // Valida que 'Country' no esté vacío y tenga un máximo de 3 caracteres
        RuleFor(r => r.Country)
            .NotEmpty()
            .MaximumLength(3);

        // Valida que 'Line1' (dirección) no esté vacío y no supere los 20 caracteres,
        // lo etiqueta como "Addres Line 1" en mensajes de error
        RuleFor(r => r.Line1)
            .NotEmpty()
            .MaximumLength(20)
            .WithName("Addres Line 1");

        // Valida que 'Line2' tenga un máximo de 20 caracteres y lo etiqueta como "Addres Line 2"
        RuleFor(r => r.Line2)
            .MaximumLength(20)
            .WithName("Addres Line 2");

        // Valida que 'City' no esté vacío y no supere los 40 caracteres
        RuleFor(r => r.City)
            .NotEmpty()
            .MaximumLength(40);

        // Valida que 'State' no esté vacío y no supere los 40 caracteres
        RuleFor(r => r.State)
            .NotEmpty()
            .MaximumLength(40);

        // Valida que 'ZipCode' no esté vacío, tenga un máximo de 10 caracteres,
        // y lo etiqueta como "Zip Code" en mensajes de error
        RuleFor(r => r.ZipCode)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Zip Code");

        // Valida que 'IsVerified' no sea nulo y esté en un valor booleano (true o false),
        // con mensaje personalizado en caso de fallo
        RuleFor(r => r.IsVerified)
            .NotNull()
            .Must(x => x == true || x == false)
            .WithMessage("IsVerified must be true or false.");

    }
}