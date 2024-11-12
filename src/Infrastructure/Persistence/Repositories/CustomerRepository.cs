using Domain.Customers;

namespace Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    // Declaración de una instancia privada y de solo lectura del contexto de la base de datos
    private readonly ApplicationDbContext _context;

  
    public CustomerRepository(ApplicationDbContext context)
    {
        // Asigna el contexto recibido a la variable _context o lanza una excepción si es nulo
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Método para agregar eliminar actualizar un nuevo cliente al contexto
    public void Add(Customer customer) => _context.Customers.Add(customer);
    public void Delete(Customer customer) => _context.Customers.Remove(customer);
    public void Update(Customer customer) => _context.Customers.Update(customer);

    // Método asincrónico que verifica si un cliente existe en la base de datos por su ID
    public async Task<bool> ExistsAsync(CustomerId id) => await _context.Customers.AnyAsync(customer => customer.Id == id);

    // Método asincrónico que obtiene un cliente por su ID, retornando null si no se encuentra
    public async Task<Customer?> GetByIdAsync(CustomerId id) => await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);

    // Método asincrónico que obtiene todos los clientes de la base de datos como una lista
    public async Task<List<Customer>> GetAll() => await _context.Customers.ToListAsync();
}
