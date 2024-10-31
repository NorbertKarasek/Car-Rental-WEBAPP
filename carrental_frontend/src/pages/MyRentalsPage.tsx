import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { useNavigate } from 'react-router-dom';

interface Rental {
    rental_id: number;
    car: Car;
    rental_date: string;
    return_date: string;
    rental_price: number;
    discount: number;
    additionalFees: number;
    isReturned: boolean;
    return_date_actual?: string;
}

interface Car {
    brand: string;
    model: string;
    // ... inne pola
}

const MyRentalsPage: React.FC = () => {
    const navigate = useNavigate();
    const [rentals, setRentals] = useState<Rental[]>([]);

    useEffect(() => {
        const token = localStorage.getItem('token');
        if (!token) {
            alert('You have to be logged in to see your rentals.');
            navigate('/login');
            return;
        }

        api.get('/Rentals/MyRentals')
            .then(response => {
                setRentals(response.data as Rental[]);
            })
            .catch(error => {
                console.error('Error occurred during downloading your rentals', error);
                alert('Error occurred during downloading your rentals');
            });
    }, [navigate]);

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
                        <tr key={rental.rental_id}>
                            <td>{rental.rental_id}</td>
                            <td>{rental.car.brand} {rental.car.model}</td>
                            <td>{new Date(rental.rental_date).toLocaleDateString()}</td>
                            <td>{new Date(rental.return_date).toLocaleDateString()}</td>
                            <td>{rental.rental_price} PLN</td>
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
