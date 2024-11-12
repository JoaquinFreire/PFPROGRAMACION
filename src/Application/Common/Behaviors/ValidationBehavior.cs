namespace Application.Common.Behaviors;
// Define un espacio de nombres llamado "Application.Common.Behaviors"…
// Es una convención para organizar clases relacionadas en proyectos de software…

// Define una clase genérica "ValidationBehavior" con los tipos "TRequest" y "TResponse"… 
// Implementa la interfaz "IPipelineBehavior", usada en mediadores como MediatR… 
// para permitir agregar comportamientos (como validación) antes y después de manejar una solicitud…
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>

// Restringe "TRequest" para que sea de tipo "IRequest" y "TResponse" debe ser de tipo "IErrorOr"… 
// Esto asegura que solo se usen tipos compatibles en la clase "ValidationBehavior"…
where TRequest : IRequest<TResponse>
where TResponse : IErrorOr
{
    // Declara un campo privado y opcional "IValidator" de tipo "TRequest"
// Este validador se usa para verificar si el "TRequest" es válido antes de procesarlo…
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    // Constructor de "ValidationBehavior" que permite inyectar un validador específico… 
// Si no se proporciona un validador, el campo "_validator" quedará como null…

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }
        // Método principal "Handle"…
        // "request" es la solicitud que se está procesando… 
        // "next" es el siguiente delegado en la cadena de responsabilidades… 
        // Si no hay validador, se omite la validación y se pasa al siguiente comportamiento…

        var validatorResult = await _validator.ValidateAsync(request, cancellationToken);
        // Si hay un validador, se ejecuta el método "ValidateAsync" para validar la solicitud… 
        // "validatorResult" contiene los resultados de la validación

        if (validatorResult.IsValid)
        {
            return await next();
        }
        // Si la validación es exitosa (es decir, "IsValid" es true), se continúa con el siguiente delegado en la cadena…

        var errors = validatorResult.Errors
                    .ConvertAll(validationFailure => Error.Validation(
                        validationFailure.PropertyName,
                        validationFailure.ErrorMessage
                    ));
        // Si hay errores de validación, se recopilan en una lista de errores
        // Cada error es convertido a un objeto de tipo "Error.Validation" usando el nombre de la propiedad y el mensaje de error…

        return (dynamic)errors;
        // Retorna la lista de errores
        // El uso de "(dynamic)" permite convertir la lista en "TResponse" de forma flexibl
    }
}
