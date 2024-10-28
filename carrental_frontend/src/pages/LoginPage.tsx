import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/axios';

interface LoginResponse {
    token: string;
}

const LoginPage: React.FC = () => {
    const navigate = useNavigate();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = (e: React.FormEvent) => {
        e.preventDefault();

        api.post('/Auth/Login', { email, password })
            .then(response => {
                const data = response.data as LoginResponse;
                const token = data.token;
                // Store the token in localStorage or application state
                localStorage.setItem('token', token);
                // Redirect the user to the home page or another page
                navigate('/');
            })
            .catch(error => {
                console.error('Error during logon:', error);
                alert('Incorrect login or password.');
            });
    };

    return (
        <div>
            <h1>Logowanie</h1>
            <form onSubmit={handleLogin}>
                <div>
                    <label>Email:</label>
                    <input type="email" value={email} onChange={e => setEmail(e.target.value)} required />
                </div>
                <div>
                    <label>Hasło:</label>
                    <input type="password" value={password} onChange={e => setPassword(e.target.value)} required />
                </div>
                <button type="submit">Zaloguj się</button>
            </form>
        </div>
    );
};

export default LoginPage;
