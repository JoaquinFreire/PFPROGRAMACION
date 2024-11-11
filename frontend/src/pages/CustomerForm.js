import React, { useState, useEffect } from 'react';
import { createCustomer, updateCustomer, getCustomerById } from '../services/customerService';
import { useParams, useNavigate } from 'react-router-dom';

const CustomerForm = () => {
    const { id } = useParams(); // Captura el ID de la URL para edición
    const navigate = useNavigate(); // Para redireccionar después de guardar
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

    // Cargar datos del cliente si existe un ID (modo edición)
    useEffect(() => {
        if (id) {
            setLoading(true);
            getCustomerById(id)
                .then((response) => {
                    // Verifica si hay datos antes de establecer el estado
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

        console.log("Datos del cliente a enviar:", customer); // Muestra los datos antes de hacer el update

        try {
            if (id) {
                // Modo de actualización
                await updateCustomer(id, customer);
            } else {
                // Modo de creación
                await createCustomer(customer);
            }
            navigate('/customers'); // Redirecciona a la lista después de guardar
        } catch (err) {
            console.error("Error al guardar datos del cliente:", err.response?.data || err.message);
            setError('Error saving customer data');
        } finally {
            setLoading(false);
        }
    };

    if (loading) return <p>Loading...</p>;
    if (error) return <p>{error}</p>;

    return (
        <div>
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
                <br />
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
                <br />
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
                <br />
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
                <br />
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
                <br />
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
                <br />
                <label>
                    Address Line 2:
                    <input
                        type="text"
                        name="line2"
                        value={customer.line2}
                        onChange={handleChange}
                    />
                </label>
                <br />
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
                <br />
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
                <br />
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
                <br />
                <button type="submit" disabled={loading}>
                    {loading ? 'Saving...' : 'Save'}
                </button>
                <button onClick={() => navigate(`/customers/`)}>Volver</button>
            </form>
        </div>
    );
};

export default CustomerForm;
