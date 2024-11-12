using Domain.Primitives;

namespace Domain.Customers
{
    // Clase Customer representa un cliente en el dominio.
    public sealed class Customer : AggregateRoot
    {
        // Constructor principal para crear una instancia de Customer.
        public Customer(CustomerId id, string name, string lastName, string email,
            PhoneNumber phoneNumber, Address address, bool isVerified = false, string token = null)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber; // Asigna el número de teléfono.
            Address = address; // Asigna la dirección.
            IsVerified = isVerified; // Indica si el cliente está verificado; por defecto es false.
            Token = token;
        }

        // Constructor privado sin parámetros para cumplir con Entity Framework.
        private Customer() {}

        // Propiedades
        public CustomerId Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Propiedad de solo lectura que combina nombre y apellido.
        public string FullName => $"{Name} {LastName}";
        public string Email { get; private set; } = string.Empty;
        public PhoneNumber PhoneNumber { get; private set; }
        public Address Address { get; private set; }

        // Propiedad que indica si el cliente está verificado. Por defecto es false.
        public bool IsVerified { get; private set; } = false;

        // Token para almacenar el token de verificación único del cliente.
        public string Token { get; private set; } = string.Empty;

        // Crear una instancia de Customer con datos actualizados.
        public static Customer UpdateCustomer(Guid id, string name, string lastName, string email, 
            PhoneNumber phoneNumber, Address address, bool isVerified)
        {
            // Crea un nuevo Customer con los datos proporcionados.
            return new Customer(new CustomerId(id), name, lastName, email, phoneNumber, address, isVerified);
        }

        // Generar un token único, utilizando un GUID.
        private string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        // Verificar al cliente.
        public void Verify()
        {
            IsVerified = true; // Cambia el estado de verificación a true.
            Token = null; // Elimina el token ya que el cliente ha sido verificado.
        }
    }
}
