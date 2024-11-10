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
    // State to store error messages
    const [error, setError] = useState<string | null>(null);

    const handleLogin = (e: React.FormEvent) => {
        e.preventDefault();

        api.post('/Auth/Login', { Email: email, Password: password })
            .then(response => {
                const data = response.data as LoginResponse;
                const token = data.token;
                // Store the token in localStorage or application state
                localStorage.setItem('token', token);
                // Redirect the user to the home page or another page
                navigate('/');
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error during logon:', error);
                setError('Incorrect login or password.');
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
            {/* Show error message if there is an error */}
            {error && <div style={{ color: 'red' }}>{error}</div>}
        </div>
    );
};

export default LoginPage;