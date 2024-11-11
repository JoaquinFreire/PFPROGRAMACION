import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getCustomers, deleteCustomer } from '../services/customerService';

const CustomerList = () => {
  const [customers, setCustomers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    getCustomers()
      .then((response) => {
        setCustomers(response.data);  // Asigna los clientes al estado
        setLoading(false);
      })
      .catch((err) => {
        setError('Error loading customers');
        setLoading(false);
      });
  }, []);

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this customer?')) {
      try {
        await deleteCustomer(id); // Llamamos al servicio de eliminación
        setCustomers(customers.filter((customer) => customer.id !== id)); // Actualizamos el estado para eliminar el cliente de la lista
      } catch (err) {
        setError('Error deleting customer');
      }
    }
  };

  // Función para formatear la dirección
  const formatAddress = (address) => {
    return `${address.country}, ${address.state}, ${address.city}, ${address.line1}, ${address.zipCode}`;
  };

  // Si está cargando o hay error
  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <h2>Customer List</h2>
      <button onClick={() => navigate('/customers/new')}>Agregar nuevo cliente</button>
      <ul>
        {customers.map((customer) => (
          <li key={customer.id}>
            <div><strong>Name:</strong> {customer.fullName}</div>
            <div><strong>Email:</strong> {customer.email}</div>
            <div><strong>Phone:</strong> {customer.phoneNumber}</div>
            <div><strong>Address:</strong> {formatAddress(customer.address)}</div>
            <div><strong>Active:</strong> {customer.active ? 'Yes' : 'No'}</div>
            
            {/* Botón para editar */}
            <button onClick={() => navigate(`/customers/edit/${customer.id}`)}>Editar</button>
            
            {/* Botón para eliminar */}
            <button onClick={() => handleDelete(customer.id)}>Eliminar</button>
            
            {/* Nuevo botón para ver los detalles */}
            <button onClick={() => navigate(`/customers/${customer.id}`)}>Ver Detalles</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CustomerList;
