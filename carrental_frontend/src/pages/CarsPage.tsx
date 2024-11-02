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
    const [cars, setCars] = useState<Car[]>([]);

    useEffect(() => {
        api.get('/Car')
            .then(response => {
                setCars(response.data as Car[]);
            })
            .catch(error => {
                console.error('Error occured during downloading cars list', error);
            });
    }, []);

    return (
        <div>
            <h1>Nasze samochody</h1>
            <ul>
                {cars.map(car => (
                    <li key={car.carId}>
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
