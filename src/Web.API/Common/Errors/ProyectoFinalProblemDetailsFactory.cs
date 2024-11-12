using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.API.Common.Errors;

// Factory para crear instancias de ProblemDetails personalizadas
public class ProyectoFinalProblemDetailsFactory : ProblemDetailsFactory
{
    // Opciones de comportamiento de la API
    private readonly ApiBehaviorOptions _options;

    // Constructor que recibe las opciones de la API
    public ProyectoFinalProblemDetailsFactory(ApiBehaviorOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    // Método para crear detalles de problemas (errores) generales
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        // Si no se proporciona el código de estado, se asigna el 500 (Error Interno del Servidor)
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        // Aplica valores por defecto a los detalles del problema
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    // Método para crear detalles de problemas de validación
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        // Si el diccionario de estado del modelo es nulo, se lanza una excepción
        if (modelStateDictionary == null)
        {
            throw new ArgumentNullException(nameof(modelStateDictionary));
        }

        // Si no se proporciona el código de estado, se asigna el 400 (Bad Request)
        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        // Si se proporciona un título, se asigna
        if(title != null)
        {
            problemDetails.Title = title;
        }
        
        // Aplica valores por defecto a los detalles del problema
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    // Método privado para aplicar valores por defecto a los detalles del problema
    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
    {
        // Si el estado no está definido, se asigna el valor del código de estado
        problemDetails.Status ??= statusCode;

        // Si hay un mapeo de error del cliente para el código de estado, se asignan el título y el tipo
        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        // Asigna el traceId para seguimiento
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        if (traceId != null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }

        // Si existen errores en el contexto HTTP, se añaden a las extensiones de los detalles del problema
        var errors = httpContext?.Items[HttpContextItemKeys.Erros] as List<Error>;

        if (errors is not null)
        {
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
        }
    }
}
