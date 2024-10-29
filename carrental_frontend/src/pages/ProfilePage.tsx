import React, { useEffect, useState } from 'react';
import api from '../api/axios';
import { useNavigate } from 'react-router-dom';
import getUserRole from '../utils/getUserRole';

interface Client {
    Client_id: string;
    firstName: string;
    surname: string;
    email: string;
    phoneNumber: string;
    address?: string;
    city?: string;
    country?: string;
    dateOfBirth?: string;
    licenseNumber?: string;
    licenseIssueDate?: string;
    // ... other fields
}

interface Employee {
    Employee_id: string;
    firstName: string;
    surname: string;
    email: string;
    phoneNumber: string;
    address?: string;
    city?: string;
    country?: string;
    dateOfBirth?: string;
    position?: string;
    // ... other fields
}

const ProfilePage: React.FC = () => {
    const navigate = useNavigate();
    const [role, setRole] = useState<string | null>(null);
    const [profile, setProfile] = useState<Client | Employee | null>(null);
    const [editMode, setEditMode] = useState(false);

    useEffect(() => {
        const userRole = getUserRole();
        if (!userRole) {
            alert('You have to be logged in to get access to this page.');
            navigate('/login');
            return;
        }
        setRole(userRole);

        const endpoint = userRole === 'Client' ? '/Clients/MyProfile' : '/Employees/MyProfile';

        api.get(endpoint)
            .then(response => {
                setProfile(response.data as Client | Employee);
            })
            .catch(error => {
                console.error('Error occurred during downloading a profile:', error);
            });
    }, [navigate]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (profile) {
            setProfile({
                ...profile,
                [e.target.name]: e.target.value
            });
        }
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        const endpoint = role === 'Client' ? '/Clients/MyProfile' : '/Employees/MyProfile';
        api.put(endpoint, profile)
            .then(() => {
                alert('Profile was successfully updated!');
                setEditMode(false);
            })
            .catch(error => {
                console.error('Error occurred during updating profile', error);
                alert('Error occurred during updating profile');
            });
    };

    if (!profile) {
        return <div>Ładowanie...</div>;
    }

    return (
        <div>
            <h1>Mój Profil</h1>
            {editMode ? (
                <form onSubmit={handleSubmit}>
                    <div>
                        <label>Imię:</label>
                        <input type="text" name="firstName" value={profile.firstName} onChange={handleChange} required />
                    </div>
                    <div>
                        <label>Nazwisko:</label>
                        <input type="text" name="surname" value={profile.surname} onChange={handleChange} required />
                    </div>
                    <div>
                        <label>Telefon:</label>
                        <input type="text" name="phoneNumber" value={profile.phoneNumber || ''} onChange={handleChange} />
                    </div>
                    <div>
                        <label>Adres:</label>
                        <input type="text" name="address" value={profile.address || ''} onChange={handleChange} />
                    </div>
                    <div>
                        <label>Miasto:</label>
                        <input type="text" name="city" value={profile.city || ''} onChange={handleChange} />
                    </div>
                    <div>
                        <label>Kraj:</label>
                        <input type="text" name="country" value={profile.country || ''} onChange={handleChange} />
                    </div>
                    <div>
                        <label>Data urodzenia:</label>
                        <input type="date" name="dateOfBirth" value={profile.dateOfBirth ? profile.dateOfBirth.slice(0, 10) : ''} onChange={handleChange} />
                    </div>
                    {role === 'Client' && (
                        <>
                            <div>
                                <label>Numer prawa jazdy:</label>
                                <input type="text" name="licenseNumber" value={(profile as Client).licenseNumber || ''} onChange={handleChange} />
                            </div>
                            <div>
                                <label>Data wydania prawa jazdy:</label>
                                <input type="date" name="licenseIssueDate" value={(profile as Client).licenseIssueDate ? (profile as Client).licenseIssueDate!.slice(0, 10) : ''} onChange={handleChange} />
                            </div>
                        </>
                    )}
                    {role === 'Employee' && (
                        <>
                            <div>
                                <label>Stanowisko:</label>
                                <input type="text" name="position" value={(profile as Employee).position || ''} onChange={handleChange} />
                            </div>
                            {/* Add more specific fields for employees */}
                        </>
                    )}
                    <button type="submit">Zapisz</button>
                    <button type="button" onClick={() => setEditMode(false)}>Anuluj</button>
                </form>
            ) : (
                <div>
                    <p>Email: {profile.email}</p>
                    <p>Imię: {profile.firstName}</p>
                    <p>Nazwisko: {profile.surname}</p>
                    <p>Telefon: {profile.phoneNumber}</p>
                    <p>Adres: {profile.address}</p>
                    <p>Miasto: {profile.city}</p>
                    <p>Kraj: {profile.country}</p>
                    <p>Data urodzenia: {profile.dateOfBirth?.slice(0, 10)}</p>
                    {role === 'Client' && (
                        <>
                            <p>Numer prawa jazdy: {(profile as Client).licenseNumber}</p>
                            <p>Data wydania prawa jazdy: {(profile as Client).licenseIssueDate?.slice(0, 10)}</p>
                        </>
                    )}
                    {role === 'Employee' && (
                        <>
                            <p>Stanowisko: {(profile as Employee).position}</p>
                            {/* Wyświetl inne pola specyficzne dla pracowników */}
                        </>
                    )}
                    <button onClick={() => setEditMode(true)}>Edytuj Profil</button>
                </div>
            )}
        </div>
    );
};

export default ProfilePage;