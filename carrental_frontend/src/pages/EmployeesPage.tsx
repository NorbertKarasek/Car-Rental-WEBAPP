import React, { useEffect, useState } from 'react';
import api from '../api/axios';

interface Employee {
    employee_id: string;
    firstName: string;
    surname: string;
    position: string;
    // Add more fields
}

const EmployeesPage: React.FC = () => {
    const [employees, setEmployees] = useState<Employee[]>([]);

    useEffect(() => {
        api.get('/Employees')
            .then(response => {
                setEmployees(response.data as Employee[]);
            })
            .catch(error => {
                console.error('Error occured during downloading employees list', error);
            });
    }, []);

    return (
        <div>
            <h1>Nasz zespół</h1>
            <ul>
                {employees.map(emp => (
                    <li key={emp.employee_id}>
                        {emp.firstName} {emp.surname} - {emp.position}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default EmployeesPage;
