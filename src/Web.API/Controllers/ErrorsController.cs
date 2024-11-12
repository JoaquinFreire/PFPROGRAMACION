using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

public class ErrorsController : ControllerBase
{
    // Configura para que esta acción no aparezca en la documentación de la API (por ejemplo, Swagger)
    [ApiExplorerSettings(IgnoreApi = true)]
    
    // Define la ruta para acceder a este controlador cuando se presenta un error
    [Route("/error")]
    public IActionResult Error()
    {
        // Obtiene el detalle de la excepción desde el contexto HTTP si existe un manejador de excepciones
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        // Retorna una respuesta con el método Problem, que crea un resultado HTTP estándar para problemas
        return Problem();
    }
}
