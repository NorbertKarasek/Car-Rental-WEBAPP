// src/utils/getUserRole.ts
import { jwtDecode } from 'jwt-decode';

function getUserRole(): string | null {
    const token = localStorage.getItem('token');
    if (!token) return null;

    const decoded: any = jwtDecode(token);
    // Add console.log to see the decoded token
    console.log('Decoded token:', decoded);

    const roles = decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    const userRoles = Array.isArray(roles) ? roles : [roles];

    if (userRoles.includes('Employee') || userRoles.includes('Administrator')) {
        return 'Employee';
    } else if (userRoles.includes('Client')) {
        return 'Client';
    } else {
        return null;
    }
}

export default getUserRole;