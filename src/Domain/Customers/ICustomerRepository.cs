namespace Domain.Customers;

// Define la interfaz para el repositorio de clientes.
public interface ICustomerRepository
{
    // Obtiene una lista de todos los clientes.
    Task<List<Customer>> GetAll();

    // Obtiene un cliente por su identificador único de forma asíncrona.
    Task<Customer?> GetByIdAsync(CustomerId id);

    // Verifica si existe un cliente con el identificador proporcionado.
    Task<bool> ExistsAsync(CustomerId id);

    // Agrega un nuevo cliente al repositorio.
    void Add(Customer customer);

    // Actualiza un cliente existente.
    void Update(Customer customer);

    // Elimina un cliente del repositorio.
    void Delete(Customer customer);
}
