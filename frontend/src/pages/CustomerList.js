import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getCustomers, deleteCustomer } from '../services/customerService';
import '../Styles/Customers.css';  // Importa el archivo CSS

const CustomerList = () => {
  const [customers, setCustomers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    getCustomers()
      .then((response) => {
        setCustomers(response.data);
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
        await deleteCustomer(id);
        setCustomers(customers.filter((customer) => customer.id !== id));
      } catch (err) {
        setError('Error deleting customer');
      }
    }
  };

  const formatAddress = (address) => {
    return `${address.country}, ${address.state}, ${address.city}, ${address.line1}, ${address.zipCode}`;
  };

  if (loading) return <p className="loading">Loading...</p>;
  if (error) return <p className="error">{error}</p>;

  return (
    <div className="container">
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
            
            <div className="buttons">
              <button onClick={() => navigate(`/customers/edit/${customer.id}`)}>Editar</button>
              <button onClick={() => handleDelete(customer.id)}>Eliminar</button>
              <button onClick={() => navigate(`/customers/${customer.id}`)}>Ver Detalles</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CustomerList;
