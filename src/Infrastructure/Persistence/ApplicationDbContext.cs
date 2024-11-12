using Application.Data;
using Domain.Customers;
using Domain.Primitives;

namespace Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    // Declaración de un campo privado y de solo lectura para el publicador de eventos de dominio
    private readonly IPublisher _publisher;

    // Constructor de la clase que recibe opciones de configuración del DbContext y un publicador de eventos de dominio
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        // Asigna el publicador recibido o lanza una excepción si es nulo
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    // Define una tabla de la base de datos para entidades de tipo Customer
    public DbSet<Customer> Customers { get; set; }

    // Configuración del modelo de base de datos utilizando el método OnModelCreating
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica las configuraciones desde los ensamblados de ApplicationDbContext
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    // Sobrescritura de SaveChangesAsync para manejar eventos de dominio cuando se guardan los cambios
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // Obtiene los eventos de dominio de las entidades que heredan de AggregateRoot en el ChangeTracker
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity) // Selecciona cada entidad de tipo AggregateRoot
            .Where(e => e.GetDomainEvents().Any()) // Filtra las entidades que tienen eventos de dominio
            .SelectMany(e => e.GetDomainEvents()); // Extrae los eventos de dominio de cada entidad

        // Llama al método base para guardar los cambios en la base de datos y obtiene el resultado
        var result = await base.SaveChangesAsync(cancellationToken);

        // Publica cada evento de dominio a través del publicador, de forma asincrónica
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        // Retorna el resultado de la operación de guardado
        return result;
    }
}
