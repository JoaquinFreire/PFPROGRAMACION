import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCustomerById, deleteCustomer } from '../services/customerService';

const CustomerDetails = () => {
  const { id } = useParams(); // Obtiene el ID desde la URL
  const [customer, setCustomer] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate(); // Hook para navegar entre rutas

  useEffect(() => {
    setLoading(true);
    getCustomerById(id)
      .then((response) => {
        setCustomer(response.data);
        setLoading(false);
      })
      .catch((err) => {
        setError('Error loading customer details');
        setLoading(false);
      });
  }, [id]);

  const handleDelete = async () => {
    if (window.confirm('Are you sure you want to delete this customer?')) {
      try {
        await deleteCustomer(id); // Eliminar cliente
        navigate('/customers'); // Redirigir a la lista de clientes
      } catch (err) {
        setError('Error deleting customer');
      }
    }
  };

  if (loading) return <p>Loading...</p>;
  if (error) return <p>{error}</p>;

  return (
    <div>
      <h2>Customer Details</h2>
      {customer ? (
        <div>
          <p><strong>Name:</strong> {customer.fullName}</p>
          <p><strong>Email:</strong> {customer.email}</p>
          <p><strong>Phone:</strong> {customer.phoneNumber}</p>
          <p><strong>Country:</strong> {customer.address?.country}</p>
          <p><strong>Address:</strong> {customer.address?.line1}, {customer.address?.line2}, {customer.address?.city}, {customer.address?.state} {customer.address?.zipCode}</p>
          
          {/* Botones debajo de los detalles */}
          <div>
            <button onClick={() => navigate('/customers')}>Volver</button>
            <button onClick={() => navigate(`/customers/edit/${customer.id}`)}>Editar</button>
            <button onClick={handleDelete}>Eliminar</button>
          </div>
        </div>
      ) : (
        <p>No customer found</p>
      )}
    </div>
  );
};

export default CustomerDetails;
