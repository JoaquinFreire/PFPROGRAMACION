using Domain.Customers;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

// Configuración de la entidad Customer para la persistencia en base de datos.
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers"); // Asigna el nombre de la tabla.

        builder.HasKey(c => c.Id); // Define la clave primaria.

        // Configura la propiedad Id
        builder.Property(c => c.Id).HasConversion(
            customerId => customerId.Value,
            value => new CustomerId(value));

        // Configuración de las propiedades
        builder.Property(c => c.Name).HasMaxLength(50);
        builder.Property(c => c.LastName).HasMaxLength(50);

        // Ignora la propiedad calculada FullName, que no se guarda en la base de datos.
        builder.Ignore(c => c.FullName);

        builder.Property(c => c.Email).HasMaxLength(255);
        builder.HasIndex(c => c.Email).IsUnique();

        // Conversión para persistir el valor y una longitud máxima de 9.
        builder.Property(c => c.PhoneNumber).HasConversion(
            phoneNumber => phoneNumber.Value,
            value => PhoneNumber.Create(value)!)
            .HasMaxLength(9);

        // Configuración de la propiedad Address como un objeto propio dentro de Customer.
        builder.OwnsOne(c => c.Address, addressBuilder =>
        {
            addressBuilder.Property(a => a.Country).HasMaxLength(3);
            addressBuilder.Property(a => a.Line1).HasMaxLength(20);
            addressBuilder.Property(a => a.Line2).HasMaxLength(20).IsRequired(false);
            addressBuilder.Property(a => a.City).HasMaxLength(40);
            addressBuilder.Property(a => a.State).HasMaxLength(40);
            addressBuilder.Property(a => a.ZipCode).HasMaxLength(10).IsRequired(false);
        });

        // Configuración de la propiedad IsVerified con valor predeterminado false.
        builder.Property(c => c.IsVerified)
            .HasDefaultValue(false)
            .HasColumnType("bit")
            .IsRequired();

        // Configuración de la propiedad Token para almacenar un token único en formato string.
        builder.Property(c => c.Token)
            .HasMaxLength(36) // Longitud para almacenar un GUID como string.
            .IsRequired(false); // Permitimos que sea nulo, ya que puede eliminarse al verificarse.
    }
}
