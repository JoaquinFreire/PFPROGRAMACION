namespace Application.Customers.Update;

public record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    string Country,
    string Line1,
    string Line2,
    string City,
    string State,
    string ZipCode,
    bool IsVerified,
    string? Token) : IRequest<ErrorOr<Unit>>; // Se agrega el Token como opcional
