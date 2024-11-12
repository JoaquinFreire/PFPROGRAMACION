namespace Customers.Common;

// Respuesta que representa la información del cliente.
public record CustomerResponse(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber, 
    AddressResponse Address, // Dirección del cliente.
    /* bool Active */ // se usaba antes para indicar si el cliente estaba activo.
    bool IsVerified
);

// Respuesta que representa la información de la dirección del cliente.
public record AddressResponse(
    string Country, 
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode 
);
