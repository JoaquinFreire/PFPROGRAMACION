public record CreateCustomerCommand(
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
    bool IsVerified = false,
    string Token = null) : IRequest<ErrorOr<Guid>>;
