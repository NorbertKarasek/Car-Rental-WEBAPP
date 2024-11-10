import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../api/axios';

interface Car {
    carId: number;
    brand: string;
    model: string;
    year: number;
    mileage: number;
    color: string;
    pricePerDay: number;
    isAutomatic: boolean;
    class: string;
    description: string;
    // Add more fields
}

const CarDetailsPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const [car, setCar] = useState<Car | null>(null);
    const [RentalDate, setRental_date] = useState('');
    const [ReturnDate, setReturn_date] = useState('');
    const [rentalPrice, setRentalPrice] = useState(0);

    useEffect(() => {
        api.get(`/Car/${id}`)
            .then(response => {
                setCar(response.data as Car);
            })
            .catch(error => {
                console.error('Error occurred during downloading information about a car', error);
            });
    }, [id]);

    useEffect(() => {
        if (RentalDate && ReturnDate) {
            const start = new Date(RentalDate);
            const end = new Date(ReturnDate);

            const days = (end.getTime() - start.getTime()) / (1000 * 3600 * 24);
            if (days > 0 && car) {
                setRentalPrice(days * car.pricePerDay);
            } else {
                setRentalPrice(0);
            }
        }
    }, [RentalDate, ReturnDate, car]);

    const handleRent = () => {
        const token = localStorage.getItem('token');
        if (!token) {
            alert('You have to be logged in to rent a car.');
            return;
        }

        const rentalData = {
            CarId: car?.carId,
            RentalDate: RentalDate,
            ReturnDate: ReturnDate
        };

        api.post('/Rental/RentACar', rentalData)
            .then(response => {
                alert('Car rented successfully!');
                // You can redirect to another page here
            })
            .catch(error => {
                console.error('Error occurred during car rental', error);
                alert('Error occurred during car rental. Please try again later.');
            });
    };


    if (!car) {
        return <div>Ładowanie...</div>;
    }

    const today = new Date().toISOString().split('T')[0];

    return (
        <div>
            <h1>{car.brand} {car.model}</h1>
            <p>Rok: {car.year}</p>
            <p>Przebieg: {car.mileage} km</p>
            <p>Kolor: {car.color}</p>
            <p>Cena za dzień: {car.pricePerDay} PLN</p>
            <p>Klasa: {car.class}</p>
            <p>Opis: {car.description}</p>
            {/* Add more car details here */}

            <h2>Wynajmij ten samochód</h2>
            <form onSubmit={(e) => {
                e.preventDefault();
                handleRent();
            }}>
                <div>
                    <label>Data rozpoczęcia:</label>
                    <input type="date" value={RentalDate} onChange={e => setRental_date(e.target.value)} min={today} required/>
                </div>
                <div>
                    <label>Data zakończenia:</label>
                    <input type="date" value={ReturnDate} onChange={e => setReturn_date(e.target.value)} min={today} required/>
                </div>
                <p>Cena wynajmu: {rentalPrice} PLN</p>
                <button type="submit">Wynajmij</button>
            </form>
        </div>
    );
};

export default CarDetailsPage;