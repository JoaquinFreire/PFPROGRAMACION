using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

 // Se define la interfaz IApplicationDbContext, que representa el contexto de la base de datos para la aplicacion.
// Un contexto de base de datos permite acceder y manipular las entidades dentro de la base de datos.
public interface IApplicationDbContext
{
     // Propiedad DbSet<Customer> que permite trabajar con la tabla de clientes en la base de datos.
    // Un DbSet representa una coleccion de entidades que se pueden consultar o modificar
    DbSet<Customer> Customers { get; set; }

// Metodo asincrono que guarda los cambios realizados en el contexto de la base de datos.
// El parametro CancellationToken se utiliza para cancelar la operacion si es necesario
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}