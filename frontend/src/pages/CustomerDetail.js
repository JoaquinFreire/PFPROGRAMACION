import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getCustomerById, deleteCustomer } from '../services/customerService';
import '../Styles/CustomerDatails.css'; // Importa el archivo CSS

const CustomerDetails = () => {
  const { id } = useParams();
  const [customer, setCustomer] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

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
        await deleteCustomer(id);
        navigate('/customers');
      } catch (err) {
        setError('Error deleting customer');
      }
    }
  };

  if (loading) return <p className="loading">Loading...</p>;
  if (error) return <p className="error">{error}</p>;

  return (
    <div className="container">
      <h2>Customer Details</h2>
      {customer ? (
        <div>
          <p><strong>Name:</strong> {customer.fullName}</p>
          <p><strong>Email:</strong> {customer.email}</p>
          <p><strong>Phone:</strong> {customer.phoneNumber}</p>
          <p><strong>Country:</strong> {customer.address?.country}</p>
          <p><strong>Address:</strong> {customer.address?.line1}, {customer.address?.line2}, {customer.address?.city}, {customer.address?.state} {customer.address?.zipCode}</p>
          
          <div className="button-group">
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
