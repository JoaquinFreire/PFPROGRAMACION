using System.Reflection;

namespace Application;

 // Se define la clase ApplicationAssemblyReference. 
// Esta clase no tiene métodos ni propiedades públicas, solo una propiedad estática interna.
public class ApplicationAssemblyReference
{

    // Se declara una propiedad estática interna que almacena una referencia al ensamblado actual.
    // Utiliza el tipo de la propia clase ApplicationAssemblyReference para obtener el ensamblado en el que se encuentra.
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}