// Navbar.tsx
import React from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

const Navbar: React.FC = () => {
    const navigate = useNavigate();
    const token = localStorage.getItem('token');

    const handleLogout = () => {
        localStorage.removeItem('token');
        navigate('/login');
    };

    let userName = '';
    if (token) {
        const decoded: any = jwtDecode(token);
        userName = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    }

    return (
        <nav>
            <ul>
                <li><Link to="/">Strona główna</Link></li>
                <li><Link to="/cars">Samochody</Link></li>
                <li><Link to="/employees">Pracownicy</Link></li>
                <li><Link to="/contact">Kontakt</Link></li>
                {/* ...other links */}
                {token ? (
                    <>
                        <li>Witaj, {userName}!</li>
                        <li>
                            <button onClick={handleLogout}>Wyloguj się</button>
                        </li>
                    </>
                ) : (
                    <>
                        <li><Link to="/login">Logowanie</Link></li>
                        <li><Link to="/register">Rejestracja</Link></li>
                    </>
                )}
            </ul>
        </nav>
    );
};

export default Navbar;
