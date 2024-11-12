using MediatR;

namespace Domain.Primitives;

// Representa un evento de dominio básico que implementa INotification.
public record DomainEvent(Guid Id) : INotification;
