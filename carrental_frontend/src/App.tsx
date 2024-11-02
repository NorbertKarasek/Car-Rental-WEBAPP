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
              <Route path="/car" element={<CarsPage />} />
              <Route path="/car/:id" element={<CarDetailsPage />} />
              <Route path="/contact" element={<ContactPage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/register" element={<RegisterPage />} />
              <Route path="/unauthorized" element={<UnauthorizedPage />} />

              {/* Private routes */}
              <Route
                  path="/myrental"
                  element={
                      <PrivateRoute>
                          <MyRentalsPage />
                      </PrivateRoute>
                  }
              />
              <Route
                  path="/employee"
                  element={
                      <PrivateRoute roles={['Employee', 'Administrator']}>
                          <EmployeesPage />
                      </PrivateRoute>
                  }
              />
              <Route
                  path="/client"
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
                  path="/rental"
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
