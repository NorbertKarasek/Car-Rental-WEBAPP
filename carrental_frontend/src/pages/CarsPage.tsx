import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { Link } from 'react-router-dom';

interface Car {
    car_id: number;
    brand: string;
    model: string;
    // Add more fields
}

const CarsPage: React.FC = () => {
    const [cars, setCars] = useState<Car[]>([]);

    useEffect(() => {
        api.get('/Cars')
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
                    <li key={car.car_id}>
                        <Link to={`/cars/${car.car_id}`}>
                            {car.brand} {car.model}
                        </Link>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default CarsPage;
