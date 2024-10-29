import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../api/axios';
import { jwtDecode } from 'jwt-decode';

interface Car {
    car_id: number;
    brand: string;
    model: string;
    year: number;
    mileage: number;
    color: string;
    pricePerDay: number;
    // Add more fields
}

const CarDetailsPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [car, setCar] = useState<Car | null>(null);
    const [startDate, setStartDate] = useState('');
    const [endDate, setEndDate] = useState('');

    useEffect(() => {
        api.get(`/Cars/${id}`)
            .then(response => {
                setCar(response.data as Car);
            })
            .catch(error => {
                console.error('Error occurred during downloading information about a car', error);
            });
    }, [id]);

    const handleRent = () => {
        const token = localStorage.getItem('token');
        if (!token) {
            alert('You have to be logged in to rent a car.');
            return;
        }

        const decoded: any = jwtDecode(token);
        const clientId = decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

        const rentalData = {
            carId: car?.car_id,
            clientId: clientId,
            startDate: startDate,
            endDate: endDate
        };

        api.post('/Rentals', rentalData)
            .then(response => {
                alert('Car rented successfully!');
                // You can redirect to another page here
            })
            .catch(error => {
                console.error('Error occured during car rental', error);
                alert('Error occurred during car rental. Please try again later.');
            });
    };

    if (!car) {
        return <div>Ładowanie...</div>;
    }

    return (
        <div>
            <h1>{car.brand} {car.model}</h1>
            <p>Rok: {car.year}</p>
            <p>Przebieg: {car.mileage} km</p>
            <p>Kolor: {car.color}</p>
            <p>Cena za dzień: {car.pricePerDay} PLN</p>
            {/* Add more car details here */}

            <h2>Wynajmij ten samochód</h2>
            <form onSubmit={(e) => { e.preventDefault(); handleRent(); }}>
                <div>
                    <label>Data rozpoczęcia:</label>
                    <input type="date" value={startDate} onChange={e => setStartDate(e.target.value)} required />
                </div>
                <div>
                    <label>Data zakończenia:</label>
                    <input type="date" value={endDate} onChange={e => setEndDate(e.target.value)} required />
                </div>
                <button type="submit">Wynajmij</button>
            </form>
        </div>
    );
};

export default CarDetailsPage;