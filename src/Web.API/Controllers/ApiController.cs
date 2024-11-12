using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.API.Common.Errors;

namespace Web.API.Controllers;

// Controlador base para manejar respuestas de errores en la API
[ApiController]
public class ApiController : ControllerBase
{
    // Método para devolver un problema (error) con una lista de errores
    protected IActionResult Problem(List<Error> errors)
    {
        // Si no hay errores, se devuelve un problema genérico
        if(errors.Count is 0)
        {
            return Problem();
        }

        // Si todos los errores son de validación, se devuelve un problema de validación
        if(errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        // Almacena los errores en el contexto HTTP para posibles futuros usos
        HttpContext.Items[HttpContextItemKeys.Erros] = errors;

        // Devolución de un problema con el primer error de la lista
        return Problem(errors[0]);
    }

    // Método privado para devolver un problema (error) con un solo error
    private IActionResult Problem(Error error)
    {
        // Determina el código de estado basado en el tipo de error
        var statusCode = error.Type switch 
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict, // Conflicto
            ErrorType.Validation => StatusCodes.Status400BadRequest, // Validación fallida
            ErrorType.NotFound => StatusCodes.Status404NotFound, // No encontrado
            _ => StatusCodes.Status500InternalServerError, // Error interno por defecto
        };

        // Devuelve el problema con el código de estado y la descripción del error
        return Problem(statusCode: statusCode, title: error.Description);
    }

    // Método privado para devolver un problema de validación con una lista de errores
    private IActionResult ValidationProblem(List<Error> errors)
    {
        // Crea un diccionario de estado del modelo para añadir los errores de validación
        var modelStateDictionary = new ModelStateDictionary();

        // Añade cada error de validación al diccionario
        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        // Devuelve el problema de validación con el diccionario de errores
        return ValidationProblem(modelStateDictionary);
    }
}
