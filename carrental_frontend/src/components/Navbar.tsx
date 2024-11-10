import React, { useEffect, useState } from 'react';
import api from '../api/axios';

interface Employee {
    employeeId: string;
    firstName: string;
    surname: string;
    position: string;
    // Add more fields
}

const EmployeesPage: React.FC = () => {
    // State to store the list of employees
    const [employees, setEmployees] = useState<Employee[]>([]);
    // State to store error messages
    const [error, setError] = useState<string | null>(null);

    // Fetch the list of employees when the component mounts
    useEffect(() => {
        api.get('/Employee')
            .then(response => {
                // Set the list of employees in state
                setEmployees(response.data as Employee[]);
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during downloading employees list', error);
                setError('Failed to load employees. Please try again later.');
            });
    }, []);

    // Show error message if there is an error
    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h1>Nasz zespół</h1>
            <ul>
                {employees.map(emp => (
                    <li key={emp.employeeId}>
                        {emp.firstName} {emp.surname} - {emp.position}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default EmployeesPage;