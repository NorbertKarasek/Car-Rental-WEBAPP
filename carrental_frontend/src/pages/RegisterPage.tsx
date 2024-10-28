import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';

const RegisterPage: React.FC = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        Email: '',
        Password: '',
        ConfirmPassword: '',
        FirstName: '',
        LastName: '',
        PhoneNumber: '',
        Role: 'Client' // By default we register as a customer
    });

    const handleRegister = (e: React.FormEvent) => {
        e.preventDefault();

        api.post('/Auth/Register', formData)
            .then(response => {
                alert('Registration was successful. You can log in now.');
                navigate('/login');
            })
            .catch(error => {
                console.error('Error during registration:', error);
                alert('An error occurred during registration.');
            });
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    return (
        <div>
            <h1>Rejestracja</h1>
            <form onSubmit={handleRegister}>
                <div>
                    <label>Imię:</label>
                    <input type="text" name="firstName" value={formData.FirstName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Nazwisko:</label>
                    <input type="text" name="lastName" value={formData.LastName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Telefon:</label>
                    <input type="text" name="phoneNumber" value={formData.PhoneNumber} onChange={handleChange} required />
                </div>
                <div>
                    <label>Email:</label>
                    <input type="email" name="email" value={formData.Email} onChange={handleChange} required />
                </div>
                <div>
                    <label>Hasło:</label>
                    <input type="password" name="password" value={formData.Password} onChange={handleChange} required />
                </div>
                <div>
                    <label>Potwierdź hasło:</label>
                    <input type="password" name="confirmPassword" value={formData.ConfirmPassword} onChange={handleChange} required />
                </div>
                {/* If you want to enable role selection (admins only), you can add a checkbox */}
                {/* <div>
          <label>Rola:</label>
          <select name="role" value={formData.role} onChange={handleChange}>
            <option value="Client">Klient</option>
            <option value="Employee">Pracownik</option>
          </select>
        </div> */}
                <button type="submit">Zarejestruj się</button>
            </form>
        </div>
    );
};

export default RegisterPage;
