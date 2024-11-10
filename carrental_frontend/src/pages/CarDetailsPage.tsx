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
    // Get the car ID from the URL parameters
    const { id } = useParams<{ id: string }>();
    // State to store car details
    const [car, setCar] = useState<Car | null>(null);
    // State to store rental start date
    const [RentalDate, setRental_date] = useState('');
    // State to store rental end date
    const [ReturnDate, setReturn_date] = useState('');
    // State to store calculated rental price
    const [rentalPrice, setRentalPrice] = useState(0);
    // State to store error messages
    const [error, setError] = useState<string | null>(null);

    // Fetch car details when the component mounts or the car ID changes
    useEffect(() => {
        api.get(`/Car/${id}`)
            .then(response => {
                // Set the car details in state
                setCar(response.data as Car);
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during downloading information about a car', error);
                setError('Failed to load car details. Please try again later.');
            });
    }, [id]);

    // Calculate rental price when rental dates or car details change
    useEffect(() => {
        if (RentalDate && ReturnDate) {
            const start = new Date(RentalDate);
            const end = new Date(ReturnDate);
            // Calculate the number of rental days
            const days = (end.getTime() - start.getTime()) / (1000 * 3600 * 24);
            if (days > 0 && car) {
                // Set the rental price based on the number of days and car price per day
                setRentalPrice(days * car.pricePerDay);
            } else {
                setRentalPrice(0);
            }
        }
    }, [RentalDate, ReturnDate, car]);

    // Handle car rental
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

        // Send rental request to the server
        api.post('/Rental/RentACar', rentalData)
            .then(response => {
                alert('Car rented successfully!');
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during car rental', error);
                setError('Failed to rent the car. Please try again later.');
            });
    };

    // Show error message if there is an error
    if (error) {
        return <div>Error: {error}</div>;
    }

    // Show loading message if car details are not yet loaded
    if (!car) {
        return <div>Ładowanie...</div>;
    }

    // Get today's date in YYYY-MM-DD format
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