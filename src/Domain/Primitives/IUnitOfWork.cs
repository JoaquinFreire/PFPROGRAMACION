namespace Domain.Primitives;

// Define una interfaz para la unidad de trabajo (Unit of Work).
public interface IUnitOfWork
{
    // Guarda los cambios pendientes en el contexto de persistencia de forma asíncrona.
    // Devuelve el número de entidades afectadas.
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
