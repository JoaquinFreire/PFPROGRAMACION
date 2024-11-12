import api from './api';

export const getCustomers = () => api.get('/customers').catch(handleError);
export const getCustomerById = (id) => api.get(`/customers/${id}`).catch(handleError);
export const createCustomer = (data) => api.post('/customers', data).catch(handleError);
export const updateCustomer = (id, data) => api.put(`/customers/${id}`, data).catch(handleError);
export const deleteCustomer = (id) => api.delete(`/customers/${id}`).catch(handleError);


// Función que maneja los errores y los pasa al frontend
const handleError = (error) => {
    if (error.response) {
        // Si hay una respuesta de la API
        console.error('Error:', error.response.data); // Muestra los detalles del error
        alert(`Error: ${error.response.data.title}`); // Puedes mostrar el error al usuario
    } else if (error.request) {
        // Si no hubo respuesta (por ejemplo, sin conexión)
        console.error('Error:', error.request);
        alert('Error: No response from server');
    } else {
        // Si hubo un error en la configuración de la solicitud
        console.error('Error:', error.message);
        alert(`Error: ${error.message}`);
    }
    // Aquí puedes retornar el error o procesarlo según sea necesario
    return Promise.reject(error); // Rechaza la promesa para manejarlo en el componente que hizo la solicitud
};