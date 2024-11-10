import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { useNavigate } from 'react-router-dom';
import getUserRole from '../utils/getUserRole';

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

const RentalsPage: React.FC = () => {
    const navigate = useNavigate();
    // State to store the list of rentals
    const [rentals, setRentals] = useState<Rental[]>([]);
    // State to store discount values for each rental
    const [discountValues, setDiscountValues] = useState<{ [key: number]: number }>({});
    // State to store error messages
    const [error, setError] = useState<string | null>(null);

    // Fetch the list of rentals when the component mounts
    useEffect(() => {
        const role = getUserRole();
        if (role !== 'Employee' && role !== 'Administrator') {
            alert('You have no access to this site.');
            navigate('/login');
            return;
        }

        api.get('/Rental/AllRental')
            .then(response => {
                // Set the list of rentals in state
                setRentals(response.data as Rental[]);
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during downloading rentals:', error);
                setError('Failed to load rentals. Please try again later.');
            });
    }, [navigate]);

    // Handle confirming the return of a rental
    const handleConfirmReturn = (rentalId: number) => {
        api.put(`/Rental/${rentalId}/ConfirmReturn`)
            .then(() => {
                alert('Return accepted.');
                // Update the rental state to mark it as returned
                setRentals(rentals.map(rental =>
                    rental.rentalId === rentalId ? { ...rental, isReturned: true, returnDateActual: new Date().toISOString() } : rental
                ));
                setError(null); // Clear any previous errors
            })
            .catch(error => {
                console.error('Error occurred during returning a car', error);
                setError('Failed to confirm return. Please try again later.');
            });
    };

    // Handle applying a discount to a rental
    const handleApplyDiscount = (rentalId: number) => {
        const discount = discountValues[rentalId];
        if (discount < 0 || discount > 0.5) {
            alert('The discount must be between 0% and 50%.');
            return;
        }

        api.put(`/Rental/${rentalId}/ApplyDiscount`, { discount })
            .then(() => {
                alert('The discount has been granted.');
                // Refresh rentals
                api.get('/Rental/AllRental')
                    .then(response => {
                        setRentals(response.data as Rental[]);
                        setError(null); // Clear any previous errors
                    })
                    .catch(error => {
                        console.error('Error refreshing rentals:', error);
                        setError('Failed to refresh rentals. Please try again later.');
                    });
            })
            .catch(error => {
                console.error('Error while granting discount:', error);
                setError('Failed to apply discount. Please try again later.');
            });
    };

    // Handle discount value change
    const handleDiscountChange = (rentalId: number, value: number) => {
        setDiscountValues(prev => ({
            ...prev,
            [rentalId]: value,
        }));
    };

    // Show error message if there is an error
    if (error) {
        return <div>Error: {error}</div>;
    }

    return (
        <div>
            <h1>Wszystkie Wynajmy</h1>
            <table>
                <thead>
                <tr>
                    <th>ID Wynajmu</th>
                    <th>Samochód</th>
                    <th>Klient</th>
                    <th>Numer Telefonu</th>
                    <th>Data Wynajmu</th>
                    <th>Data Zwrotu</th>
                    <th>Cena</th>
                    <th>Zniżka</th>
                    <th>Dodatkowe Opłaty</th>
                    <th>Zwrócony</th>
                    <th>Akcje</th>
                </tr>
                </thead>
                <tbody>
                {rentals.map(rental => (
                    <tr key={rental.rentalId}>
                        <td>{rental.rentalId}</td>
                        <td>{rental.car.brand} {rental.car.model}</td>
                        <td>{rental.client.firstName} {rental.client.surname}</td>
                        <td>{rental.client.phoneNumber}</td>
                        <td>{new Date(rental.rentalDate).toLocaleDateString()}</td>
                        <td>{new Date(rental.returnDate).toLocaleDateString()}</td>
                        <td>{rental.rentalPrice} PLN</td>
                        <td>{(Math.min(rental.discount, 0.5) * 100).toFixed(0)}%</td>
                        <td>{rental.additionalFees} PLN</td>
                        <td>{rental.isReturned ? 'Tak' : 'Nie'}</td>
                        <td>
                            {!rental.isReturned && (
                                <button onClick={() => handleConfirmReturn(rental.rentalId)}>Zatwierdź Zwrot</button>
                            )}
                            <div>
                                <input
                                    type="number"
                                    step="0.01"
                                    min="0"
                                    max="0.5"
                                    placeholder="Zniżka (np. 0.1)"
                                    value={discountValues[rental.rentalId] || ''}
                                    onChange={e => handleDiscountChange(rental.rentalId, parseFloat(e.target.value))}
                                />
                                <button onClick={() => handleApplyDiscount(rental.rentalId)}>Przyznaj Zniżkę</button>
                            </div>
                        </td>
                    </tr>
                ))}
                </tbody>
            </table>
        </div>
    );
};

export default RentalsPage;