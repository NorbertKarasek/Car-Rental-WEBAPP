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

    const isEmployeeOrAdmin = userRoles.includes('Employee') || userRoles.includes('Administrator');
    const isClient = userRoles.includes('Client');

    return (
        <nav>
            <ul>
                {token ? (
                    <>
                        Witaj, {userName}!
                        <li><Link to="/profile">Mój Profil</Link></li>
                        <li><Link to="/myrental">Moje Wynajmy</Link></li>
                        {/* Show register link for logged in admins and employees */}
                        {isEmployeeOrAdmin && <li><Link to="/register">Rejestracja</Link></li>}
                        <li>
                            <button onClick={handleLogout}>Wyloguj się</button>
                        </li>
                    </>
                ) : (
                    <>
                        <li><Link to="/login">Logowanie</Link></li>
                        {/* Show register link to everyone */}
                        <li><Link to="/register">Rejestracja</Link></li>
                    </>
                )}
                {/* Links visible for all */}
                <li><Link to="/">Strona główna</Link></li>
                <li><Link to="/car">Samochody</Link></li>
                <li><Link to="/contact">Kontakt</Link></li>
                {/* Links visible only for employees or admins */}
                {isEmployeeOrAdmin && (
                    <>
                        <li><Link to="/client">Klienci</Link></li>
                        <li><Link to="/employee">Pracownicy</Link></li>
                        <li><Link to="/rental">Wynajmy</Link></li>
                    </>
                )}
            </ul>
        </nav>
    );
};

export default Navbar;
