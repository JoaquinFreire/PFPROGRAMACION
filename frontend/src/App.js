import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import CustomerList from './pages/CustomerList';
import CustomerForm from './pages/CustomerForm';
import CustomerDetails from './pages/CustomerDetail';
const App = () => {
  return (
    <Router>
      <Routes>
        <Route path="/customers" element={<CustomerList />} />
        <Route path="/customers/new" element={<CustomerForm />} />
        <Route path="/customers/edit/:id" element={<CustomerForm />} />
        <Route path="/customers/:id" element={<CustomerDetails />} />
      </Routes>
    </Router>
  );
};

export default App;
