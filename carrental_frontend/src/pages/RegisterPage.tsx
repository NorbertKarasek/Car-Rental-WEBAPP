import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';
import api from '../api/axios';

const RegisterPage: React.FC = () => {
    const navigate = useNavigate();
    const token = localStorage.getItem('token');

    let userRoles: string[] = [];
    if (token) {
        const decoded: any = jwtDecode(token);
        const roles = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        userRoles = Array.isArray(roles) ? roles : [roles];
    }

    const isEmployeeOrAdmin = userRoles.includes('Employee') || userRoles.includes('Administrator');

    const [formData, setFormData] = useState({
        email: '',
        password: '',
        confirmPassword: '',
        firstName: '',
        lastName: '',
        phoneNumber: '',
        role: 'Client' // Default role for new users
    });

    const handleRegister = (e: React.FormEvent) => {
        e.preventDefault();

        api.post('/Auth/Register', formData)
            .then(response => {
                alert('Registration successful. You can now log in.');
                navigate('/login');
            })
            .catch(error => {
                console.error('Error occurred during registration.', error);
                alert('Error occurred during registration. Please try again.');
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
                    <input type="text" name="firstName" value={formData.firstName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Nazwisko:</label>
                    <input type="text" name="lastName" value={formData.lastName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Telefon:</label>
                    <input type="text" name="phoneNumber" value={formData.phoneNumber} onChange={handleChange} required />
                </div>
                <div>
                    <label>Email:</label>
                    <input type="email" name="email" value={formData.email} onChange={handleChange} required />
                </div>
                <div>
                    <label>Hasło:</label>
                    <input type="password" name="password" value={formData.password} onChange={handleChange} required />
                </div>
                <div>
                    <label>Potwierdź hasło:</label>
                    <input type="password" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} required />
                </div>
                {/* Show roles option only for admins and employees */}
                {isEmployeeOrAdmin ? (
                    <div>
                        <label>Rola:</label>
                        <select name="role" value={formData.role} onChange={handleChange}>
                            <option value="Client">Klient</option>
                            <option value="Employee">Pracownik</option>
                        </select>
                    </div>
                ) : (
                    // For regular users, set the role to 'Client' by default
                    <input type="hidden" name="role" value="Client" />
                )}
                <button type="submit">Zarejestruj się</button>
            </form>
        </div>
    );
};

export default RegisterPage;
