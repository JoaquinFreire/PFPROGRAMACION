namespace Domain.ValueObjects;

// Representa un objeto de valor para una dirección.
public partial record Address
{
    // Constructor que inicializa las propiedades de la dirección.
    public Address(string country, string line1, string line2, string city, string state, string zipCode)
    {
        Country = country;
        Line1 = line1;
        Line2 = line2;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    // Propiedades de solo inicialización que describen la dirección.
    public string Country { get; init; }
    public string Line1 { get; init; }
    public string Line2 { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }

    // Método estático para crear una dirección validando que los campos no estén vacíos o nulos.
    public static Address? Create(string country, string line1, string line2, string city, string state, string zipCode)
    {
        // Devuelve null si alguno de los campos requeridos está vacío o nulo.
        if (string.IsNullOrEmpty(country) || string.IsNullOrEmpty(line1) ||
            string.IsNullOrEmpty(line2) || string.IsNullOrEmpty(city) ||
            string.IsNullOrEmpty(state) || string.IsNullOrEmpty(zipCode))
        {
            return null;
        }

        // Crea y retorna una nueva instancia de Address con los datos proporcionados.
        return new Address(country, line1, line2, city, state, zipCode);
    }
}
