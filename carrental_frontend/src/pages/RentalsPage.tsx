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
    const [rentals, setRentals] = useState<Rental[]>([]);
    const [discountValues, setDiscountValues] = useState<{ [key: number]: number }>({});

    useEffect(() => {
        const role = getUserRole();
        if (role !== 'Employee' && role !== 'Administrator') {
            alert('Nie masz dostępu do tej strony.');
            navigate('/login');
            return;
        }

        api.get('/Rental/AllRental')
            .then(response => {
                setRentals(response.data as Rental[]);
            })
            .catch(error => {
                console.error('Błąd podczas pobierania wynajmów:', error);
                alert('Błąd podczas pobierania wynajmów.');
            });
    }, [navigate]);

    const handleConfirmReturn = (rentalId: number) => {
        api.put(`/Rental/${rentalId}/ConfirmReturn`)
            .then(() => {
                alert('Zwrot został zatwierdzony.');
                setRentals(rentals.map(rental =>
                    rental.rentalId === rentalId ? { ...rental, isReturned: true, returnDateActual: new Date().toISOString() } : rental
                ));
            })
            .catch(error => {
                console.error('Błąd podczas zatwierdzania zwrotu:', error);
                alert('Błąd podczas zatwierdzania zwrotu.');
            });
    };

    const handleApplyDiscount = (rentalId: number) => {
        const discount = discountValues[rentalId];
        if (discount < 0 || discount > 0.5) {
            alert('Zniżka musi być w zakresie od 0% do 50%.');
            return;
        }

        api.put(`/Rental/${rentalId}/ApplyDiscount`, { discount })
            .then(() => {
                alert('Zniżka została przyznana.');
                // Refresh rentals
                api.get('/Rental/AllRental')
                    .then(response => {
                        setRentals(response.data as Rental[]);
                    })
                    .catch(error => {
                        console.error('Błąd podczas odświeżania wynajmów:', error);
                    });
            })
            .catch(error => {
                console.error('Błąd podczas przyznawania zniżki:', error);
                alert('Błąd podczas przyznawania zniżki.');
            });
    };

    const handleDiscountChange = (rentalId: number, value: number) => {
        setDiscountValues(prev => ({
            ...prev,
            [rentalId]: value,
        }));
    };

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
                        <td>{(rental.discount * 100).toFixed(0)}%</td>
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
