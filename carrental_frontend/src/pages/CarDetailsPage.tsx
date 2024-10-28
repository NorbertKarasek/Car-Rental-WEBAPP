import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import api from '../api/axios';

interface Car {
    car_id: number;
    car_Brand: string;
    car_Model: string;
    car_Year: number;
    car_Mileage: number;
    car_Color: string;
    car_PricePerDay: number;
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
            <h1>{car.car_Brand} {car.car_Model}</h1>
            <p>Rok produkcji: {car.car_Year}</p>
            <p>Przebieg: {car.car_Mileage} km</p>
            <p>Kolor: {car.car_Color}</p>
            <p>Cena za dzień: {car.car_PricePerDay} PLN</p>
            {/* Put more informations if needed */}
        </div>
    );
};

export default CarDetailsPage;
