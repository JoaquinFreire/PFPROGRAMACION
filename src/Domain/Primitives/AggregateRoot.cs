namespace Domain.Primitives;

// Clase base abstracta para representar un agregado raíz en el dominio.
public abstract class AggregateRoot 
{
    // Lista privada para almacenar los eventos del dominio asociados al agregado.
    private readonly List<DomainEvent> _domainEvents = new();

    // Obtiene la colección de eventos de dominio registrados.
    public ICollection<DomainEvent> GetDomainEvents() => _domainEvents;

    // Registra un nuevo evento de dominio en la lista.
    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
