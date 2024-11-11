import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5136/', // URL backend
});

export default api;
