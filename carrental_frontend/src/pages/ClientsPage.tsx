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
    const [clients, setClients] = useState<Client[]>([]);

    useEffect(() => {
        api.get('/Clients')
            .then(response => {
                setClients(response.data as Client[]);
            })
            .catch(error => {
                console.error('Błąd podczas pobierania listy klientów:', error);
            });
    }, []);

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
