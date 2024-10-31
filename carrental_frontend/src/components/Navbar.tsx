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
    let userRoles: string[] = [];
    if (token) {
        const decoded: any = jwtDecode(token);
        userName = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
        const roles = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        userRoles = Array.isArray(roles) ? roles : [roles];
    }

    const isEmployee = userRoles.includes('Employee') || userRoles.includes('Administrator');

    return (
        <nav>
            <ul>
                {/* Links visible depending on logon state */}
                {token ? (
                    <>
                        Witaj, {userName}!
                        <li><Link to="/profile">Mój Profil</Link></li>
                        <li><Link to="/myrentals">Moje Wynajmy</Link></li>
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
                {/* links visible for all */}
                <li><Link to="/">Strona główna</Link></li>
                <li><Link to="/cars">Samochody</Link></li>
                <li><Link to="/contact">Kontakt</Link></li>
                {/* Links visible only for employees */}
                {isEmployee && (
                    <li><Link to="/clients">Klienci</Link></li>
                )}
                {isEmployee && (
                    <li><Link to="/employees">Pracownicy</Link></li>
                )}
                {isEmployee && (
                    <li><Link to="/rentals">Wynajmy</Link></li>
                )}

            </ul>
        </nav>
    );
};

export default Navbar;