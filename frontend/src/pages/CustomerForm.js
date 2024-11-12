import React, { useState, useEffect } from 'react';
import { createCustomer, updateCustomer, getCustomerById } from '../services/customerService';
import { useParams, useNavigate } from 'react-router-dom';
import '../Styles/CustomerForm.css'; // Importa el archivo CSS

const CustomerForm = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [customer, setCustomer] = useState({
        name: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        country: '',
        line1: '',
        line2: '',
        city: '',
        state: '',
        zipCode: '',
    });
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (id) {
            setLoading(true);
            getCustomerById(id)
                .then((response) => {
                    if (response.data) {
                        setCustomer(response.data);
                    } else {
                        setError('No customer data found');
                    }
                    setLoading(false);
                })
                .catch((err) => {
                    setError('Error loading customer data');
                    setLoading(false);
                });
        }
    }, [id]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCustomer((prevCustomer) => ({
            ...prevCustomer,
            [name]: value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            if (id) {
                await updateCustomer(id, customer);
            } else {
                await createCustomer(customer);
            }
            navigate('/customers');
        } catch (err) {
            console.error("Error al guardar datos del cliente:", err.response?.data || err.message);
            setError('Error saving customer data');
            window.location.reload(); // Recarga la página cuando hay un error
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <p className="loading">Loading...</p>;
    if (error) return <p className="error">{error}</p>;

    return (
        <div className="container">
            <h2>{id ? 'Edit Customer' : 'Create Customer'}</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    Name:
                    <input
                        type="text"
                        name="name"
                        value={customer.name}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Last Name:
                    <input
                        type="text"
                        name="lastName"
                        value={customer.lastName}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Email:
                    <input
                        type="email"
                        name="email"
                        value={customer.email}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Phone Number:
                    <input
                        type="text"
                        name="phoneNumber"
                        value={customer.phoneNumber}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Country:
                    <input
                        type="text"
                        name="country"
                        value={customer.country}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Address Line 1:
                    <input
                        type="text"
                        name="line1"
                        value={customer.line1}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Address Line 2:
                    <input
                        type="text"
                        name="line2"
                        value={customer.line2}
                        onChange={handleChange}
                    />
                </label>
                <label>
                    City:
                    <input
                        type="text"
                        name="city"
                        value={customer.city}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    State:
                    <input
                        type="text"
                        name="state"
                        value={customer.state}
                        onChange={handleChange}
                        required
                    />
                </label>
                <label>
                    Zip Code:
                    <input
                        type="text"
                        name="zipCode"
                        value={customer.zipCode}
                        onChange={handleChange}
                        required
                    />
                </label>
                <button type="submit" disabled={loading}>
                    {loading ? 'Saving...' : 'Save'}
                </button>
                <button type="button" onClick={() => navigate('/customers/')}>Volver</button>
            </form>
        </div>
    );
};

export default CustomerForm;
