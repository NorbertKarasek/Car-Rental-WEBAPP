import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { Link } from 'react-router-dom';

interface Car {
    carId: number;
    brand: string;
    model: string;
    // Add more fields
}

const CarsPage: React.FC = () => {
    // State to store the list of cars
    const [cars, setCars] = useState<Car[]>([]);
    // State to store error messages
    const [error, setError] = useState<string | null>(null);

    // Fetch the list of cars when the component mounts
    useEffect(() => {
        api.get('/Car')
            .then(response => {
                // Set the list of cars in state
                setCars(response.data as Car[]);
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during downloading cars list', error);
                setError('Failed to load cars list. Please try again later.');
            });
    }, []);

    // Show error message if there is an error
    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h1>Nasze samochody</h1>
            <ul>
                {cars.map(car => (
                    <li key={car.carId}>
                        {/* Link to the car details page */}
                        <Link to={`/Car/${car.carId}`}>
                            {car.brand} {car.model}
                        </Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CarsPage;