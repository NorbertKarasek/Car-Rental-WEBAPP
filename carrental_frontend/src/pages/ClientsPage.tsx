import React, { useEffect, useState } from 'react';
import api from '../api/axios';

interface Client {
    clientId: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    address: string;
    city: string;
    country: string;
    dateOfBirth: string;
    licenseNumber: string;
    licenseIssueDate: string;
    rentals: string[];
    applicationUser: string;
    // add more fields if needed
}

const ClientsPage: React.FC = () => {
    // State to store the list of clients
    const [clients, setClients] = useState<Client[]>([]);
    // State to store error messages
    const [error, setError] = useState<string | null>(null);

    // Fetch the list of clients when the component mounts
    useEffect(() => {
        api.get('/Client')
            .then(response => {
                // Set the list of clients in state
                setClients(response.data as Client[]);
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during downloading clients list:', error);
                setError('Failed to load clients list. Please try again later.');
            });
    }, []);

    // Show error message if there is an error
    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h1>Lista Klientów</h1>
            <ul>
                {clients.map(client => (
                    <li key={client.clientId}>
                        {client.firstName} {client.lastName} - {client.email} - {client.phoneNumber} - {client.address} - {client.city} - {client.country} - {client.dateOfBirth} - {client.licenseNumber} - {client.licenseIssueDate}
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default ClientsPage;