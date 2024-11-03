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
              <Route path="/unauthorized" element={<UnauthorizedPage />} />

              {/* Private routes */}
              {/* Register */}
              <Route
                  path="/register"
                  element={
                      <PrivateRoute
                          allowFor={['Anonymous', 'Employee', 'Administrator']}
                          redirectTo="/unauthorized"
                      >
                          <RegisterPage />
                      </PrivateRoute>
                  }
              />
              {/* List of employees */}
              <Route
                  path="/employee"
                  element={
                      <PrivateRoute allowFor={['Employee', 'Administrator']}
                                    redirectTo="/unauthorized"
                      >
                          <EmployeesPage />
                      </PrivateRoute>
                  }
              />
              {/* List of clients */}
              <Route
                  path="/client"
                  element={
                      <PrivateRoute allowFor={['Employee', 'Administrator']}
                                    redirectTo="/unauthorized"
                      >
                          <ClientsPage />
                      </PrivateRoute>
                  }
              />
              {/* All Rentals */}
              <Route
                  path="/rental"
                  element={
                      <PrivateRoute allowFor={['Employee', 'Administrator']}
                                    redirectTo="/unauthorized"
                      >
                          <RentalsPage />
                      </PrivateRoute>
                  }
                />
              {/* My Profile */}
              <Route
                  path="/profile"
                  element={
                      <PrivateRoute>
                          <ProfilePage />
                      </PrivateRoute>
                  }
              />
              {/* My rentals */}
              <Route
                  path="/myrental"
                  element={
                      <PrivateRoute>
                          <MyRentalsPage />
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
