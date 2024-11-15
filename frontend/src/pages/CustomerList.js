import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getCustomers, deleteCustomer } from '../services/customerService';
import '../Styles/CustomerList.css';  // Importa el archivo CSS

const CustomerList = () => {
  const [customers, setCustomers] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    setLoading(true);
    getCustomers()
      .then((response) => {
        console.log('Datos obtenidos de la base de datos:', response.data);
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
      <button class="agregar" onClick={() => navigate('/customers/new')}>Add new client</button>
      <ul>
        {customers.map((customer) => (
          <li key={customer.id}>
            <div><strong>Name:</strong> {customer.fullName}</div>
            <div><strong>Email:</strong> {customer.email}</div>
            <div><strong>Phone:</strong> {customer.phoneNumber}</div>
            <div><strong>Address:</strong> {formatAddress(customer.address)}</div>
            <div><strong>Verified:</strong> {customer.isVerified ? 'Yes' : 'No'}</div>
            
            <div className="buttons">
              <button class="edit" onClick={() => navigate(`/customers/edit/${customer.id}`)}>Edit</button>
              <button class="delete" onClick={() => handleDelete(customer.id)}>Delete</button>
              <button class="details" onClick={() => navigate(`/customers/${customer.id}`)}>Details</button>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default CustomerList;
