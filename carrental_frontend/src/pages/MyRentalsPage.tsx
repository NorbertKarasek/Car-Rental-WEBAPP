import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { useNavigate } from 'react-router-dom';

interface Rental {
    rentalId: number;
    car: Car;
    client: Client;
    rentalDate: string;
    returnDate: string;
    rentalPrice: number;
    discount: number;
    additionalFees: number;
    isReturned: boolean;
    returnDateActual?: string;
}

interface Car {
    brand: string;
    model: string;
    // ... other fields
}

interface Client {
    firstName: string;
    surname: string;
    email: string;
    phoneNumber: string;
    // ... other fields
}

const MyRentalsPage: React.FC = () => {
    const navigate = useNavigate();
    // State to store the list of rentals
    const [rentals, setRentals] = useState<Rental[]>([]);
    // State to store error messages
    const [error, setError] = useState<string | null>(null);

    // Fetch the list of rentals when the component mounts
    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {
            alert('You have to be logged in to see your rentals.');
            navigate('/login');
            return;
        }

        api.get('/Rental/MyRental')
            .then(response => {
                // Set the list of rentals in state
                setRentals(response.data as Rental[]);
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during downloading your rentals', error);
                setError('Failed to load your rentals. Please try again later.');
            });
    }, [navigate]);

    // Show error message if there is an error
    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h1>Moje Wynajmy</h1>
            {rentals.length === 0 ? (
                <p>Nie masz żadnych wynajmów.</p>
            ) : (
                <table>
                    <thead>
                    <tr>
                        <th>ID Wynajmu</th>
                        <th>Wynajmujący</th>
                        <th>Numer Telefonu</th>
                        <th>Samochód</th>
                        <th>Data Wynajmu</th>
                        <th>Data Zwrotu</th>
                        <th>Cena</th>
                        <th>Zniżka</th>
                        <th>Dodatkowe Opłaty</th>
                        <th>Zwrócony</th>
                    </tr>
                    </thead>
                    <tbody>
                    {rentals.map(rental => (
                        <tr key={rental.rentalId}>
                            <td>{rental.rentalId}</td>
                            <td>{rental.client.firstName} {rental.client.surname}</td>
                            <td>{rental.client.phoneNumber}</td>
                            <td>{rental.car.brand} {rental.car.model}</td>
                            <td>{new Date(rental.rentalDate).toLocaleDateString()}</td>
                            <td>{new Date(rental.returnDate).toLocaleDateString()}</td>
                            <td>{rental.rentalPrice} PLN</td>
                            <td>{(rental.discount * 100).toFixed(0)}%</td>
                            <td>{rental.additionalFees} PLN</td>
                            <td>{rental.isReturned ? 'Tak' : 'Nie'}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            )}
        </div>
    );
};

export default MyRentalsPage;