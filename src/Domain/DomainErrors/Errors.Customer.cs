using ErrorOr;

namespace Domain.DomainErrors;

// Clase estática que define errores específicos del dominio.
public static partial class Errors
{
    // Subclase para errores relacionados con Customer.
    public static class Customer
    {
        // Error para formato inválido de número de teléfono.
        public static Error PhoneNumberWithBadFormat => 
            Error.Validation("Customer.PhoneNumber", "Phone number has not valid format.");

        // Error para formato inválido de dirección.
        public static Error AddressWithBadFormat => 
            Error.Validation("Customer.Address", "Address is not valid.");
    }
}
