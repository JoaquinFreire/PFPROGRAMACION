namespace Domain.ValueObjects;

// Representa un objeto de valor para un número de teléfono.
public partial record PhoneNumber
{
    // Longitud por defecto para un número de teléfono válido.
    private const int DefaultLenght = 9;

    // Patrón de expresión regular para validar el formato del número de teléfono.
    private const string Pattern = @"^(?:-*\d-*){8}$";

    // Constructor privado que inicializa el número de teléfono con un valor válido.
    private PhoneNumber(string value) => Value = value;

    // Método de fábrica para crear un objeto PhoneNumber validando el formato y la longitud.
    public static PhoneNumber? Create(string value)
    {
        // Verifica si el valor es nulo o vacío, si no coincide con el patrón, o si la longitud no es la esperada.
        if (string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length != DefaultLenght)
        {
            Console.WriteLine(value); // Imprime el valor no válido (para depuración).
            return null; // Retorna null si el valor no es válido.
        }

        // Retorna una nueva instancia de PhoneNumber si el valor es válido.
        return new PhoneNumber(value);
    }

    // Propiedad que almacena el valor del número de teléfono.
    public string Value { get; init; }

    // Método generado automáticamente para construir la expresión regular a partir del patrón.
    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();
}
