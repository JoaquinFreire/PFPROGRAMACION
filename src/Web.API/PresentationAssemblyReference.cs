using System.Reflection;

namespace Web.API;

// Clase que sirve como referencia para la asamblea actual
public class PresentationAssemblyReference
{
    // Propiedad que guarda la referencia a la asamblea de la clase actual
    internal static readonly Assembly Assembly = typeof(PresentationAssemblyReference).Assembly;
}
