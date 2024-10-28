import React from 'react';
import { Navigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

interface PrivateRouteProps {
    children: JSX.Element;
    roles?: string[]; // Acceptable roles
}

interface DecodedToken {
    exp: number;
    [key: string]: any;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children, roles }) => {
    const token = localStorage.getItem('token');

    if (!token) {
        return <Navigate to="/login" />;
    }

    try {
        const decoded: DecodedToken = jwtDecode(token);

        // Check if the token has not expired
        if (decoded.exp * 1000 < Date.now()) {
            localStorage.removeItem('token');
            return <Navigate to="/login" />;
        }

        // Check user roles
        if (roles) {
            const userRoles = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
            if (Array.isArray(userRoles)) {
                const hasRole = roles.some(role => userRoles.includes(role));
                if (!hasRole) {
                    return <Navigate to="/unauthorized" />;
                }
            } else {
                if (!roles.includes(userRoles)) {
                    return <Navigate to="/unauthorized" />;
                }
            }
        }

        return children;
    } catch (error) {
        localStorage.removeItem('token');
        return <Navigate to="/login" />;
    }
};

export default PrivateRoute;