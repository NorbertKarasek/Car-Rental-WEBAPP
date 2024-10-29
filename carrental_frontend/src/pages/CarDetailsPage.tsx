import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../api/axios';

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

    useEffect(() => {
        api.get(`/Cars/${id}`)
            .then(response => {
                setCar(response.data as Car);
            })
            .catch(error => {
                console.error('Error occurred during getting car details:', error);
            });
    }, [id]);

    if (!car) {
        return <div>Ładowanie...</div>;
    }

    return (
        <div>
            <h1>{car.brand} {car.model}</h1>
            <p>Rok produkcji: {car.year}</p>
            <p>Przebieg: {car.mileage} km</p>
            <p>Kolor: {car.color}</p>
            <p>Cena za dzień: {car.pricePerDay} PLN</p>
            {/* Put more informations if needed */}
        </div>
    );
};

export default CarDetailsPage;
