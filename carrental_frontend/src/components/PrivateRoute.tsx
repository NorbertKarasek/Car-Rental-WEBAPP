import React from 'react';
import { Navigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

interface PrivateRouteProps {
    children: React.ReactNode;
    allowFor?: string[]; // Table of roles allowed to access the route
    redirectTo?: string; // Route to redirect to if user is not logged in or doesn't have required role
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children, allowFor, redirectTo = '/login' }) => {
    const token = localStorage.getItem('token');

    if (!token) {
        // User is not logged in
        if (allowFor && allowFor.includes('Anonymous')) {
            return <>{children}</>;
        } else {
            return <Navigate to={redirectTo} replace />;
        }
    }

    // Decode the token to get user roles
    const decoded: any = jwtDecode(token);
    const userRoles = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    const userRolesArray = Array.isArray(userRoles) ? userRoles : [userRoles];

    if (allowFor && !userRolesArray.some(role => allowFor.includes(role))) {
        // User is logged in, but doesn't have required role
        return <Navigate to="/unauthorized" replace />;
    }

    // User is logged in and has required role
    return <>{children}</>;
};

export default PrivateRoute;