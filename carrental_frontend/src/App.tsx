import React from 'react';
import { Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import CarsPage from './pages/CarsPage';
import CarDetailsPage from './pages/CarDetailsPage';
import EmployeesPage from './pages/EmployeesPage';
import ContactPage from './pages/ContactPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import Navbar from "./components/Navbar";
import PrivateRoute from './components/PrivateRoute';
import UnauthorizedPage from './pages/UnauthorizedPage';
import ClientsPage from './pages/ClientsPage';
import ProfilePage from './pages/ProfilePage';
import RentalsPage from './pages/RentalsPage';
import MyRentalsPage from './pages/MyRentalsPage';


function App() {
  return (
      <div className="App">
          <Navbar />
          <Routes>
              {/* Public routes */}
              <Route path="/" element={<HomePage />} />
              <Route path="/cars" element={<CarsPage />} />
              <Route path="/cars/:id" element={<CarDetailsPage />} />
              <Route path="/contact" element={<ContactPage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/register" element={<RegisterPage />} />
              <Route path="/unauthorized" element={<UnauthorizedPage />} />

              {/* Private routes */}
              <Route
                  path="/myrentals"
                  element={
                      <PrivateRoute>
                          <MyRentalsPage />
                      </PrivateRoute>
                  }
              />
              <Route
                  path="/employees"
                  element={
                      <PrivateRoute roles={['Employee', 'Administrator']}>
                          <EmployeesPage />
                      </PrivateRoute>
                  }
              />
              <Route
                  path="/clients"
                  element={
                      <PrivateRoute roles={['Employee', 'Administrator']}>
                          <ClientsPage />
                      </PrivateRoute>
                  }
              />
              <Route
                  path="/profile"
                  element={
                      <PrivateRoute>
                          <ProfilePage />
                      </PrivateRoute>
                  }
              />
              <Route
                  path="/rentals"
                  element={
                      <PrivateRoute roles={['Employee', 'Administrator']}>
                          <RentalsPage />
                      </PrivateRoute>
                  }
                />
              {/* Add more private routes */}
          </Routes>
        {/* You can add components here, ex. Footer */}
      </div>
  );
}

export default App;
