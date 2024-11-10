# Project Documentation - Car Rental Application

## Table of Contents

1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Installation](#installation)
4. [Running the Application](#running-the-application)
5. [Component Descriptions](#component-descriptions)
    - [api/axios.ts](#apiaxiosts)
    - [components/Navbar.tsx](#componentsnavbartsx)
    - [components/PrivateRoute.tsx](#componentsprivateroutetsx)
6. [Page Descriptions](#page-descriptions)
    - [HomePage](#homepage)
    - [CarsPage](#carspage)
    - [CarDetailsPage](#cardetailspage)
    - [LoginPage](#loginpage)
    - [RegisterPage](#registerpage)
    - [ProfilePage](#profilepage)
    - [MyRentalsPage](#myrentalspage)
    - [ClientsPage](#clientspage)
    - [EmployeesPage](#employeespage)
    - [RentalsPage](#rentalspage)
    - [ContactPage](#contactpage)
    - [UnauthorizedPage](#unauthorizedpage)
7. [Libraries Used](#libraries-used)
8. [Authors](#authors)
9. [Additional Notes](#additional-notes)
10. [Contact](#contact)

---

## Introduction

This is the frontend of a web application built with React and TypeScript. The application is designed for managing a car rental service, allowing users to browse available vehicles, rent them, and manage their profiles. Employees have additional privileges to manage clients and rentals.

---

## Project Structure

The project is organized as follows:

- **api/** - Axios configuration for communication with the backend API.
- **components/** - Shared components used across the application (e.g., navigation, private routes).
- **pages/** - Individual pages of the application (e.g., home page, login, registration).
- **utils/** - Helper functions (e.g., retrieving user role from a JWT token).

---

## Installation

To install the project locally, follow these steps:

1. **Clone the repository:**

   ```bash
   git clone [REPOSITORY_URL]
   ```

2. **Navigate to the project directory:**

   ```bash
   cd carrental_frontend
   ```

3. **Install dependencies:**

   ```bash
   npm install
   ```

   or

   ```bash
   yarn install
   ```

---

## Running the Application

To run the application in development mode:

1. **Set environment variables:**

   Ensure that the `.env` file contains the `REACT_APP_API_URL` variable pointing to the backend API.

2. **Start the application:**

   ```bash
   npm start
   ```

   or

   ```bash
   yarn start
   ```

3. **Open your browser:**

   The application will be available at [http://localhost:3000](http://localhost:3000).

---

## Component Descriptions

### api/axios.ts

Axios configuration for communicating with the backend.

- **BaseURL:** Sets the base URL to the value of `REACT_APP_API_URL` from the `.env` file.
- **Request Interceptor:** Automatically adds the JWT token from `localStorage` to the `Authorization` header for each request.
- **Response Interceptor:** If the response has a 401 status (unauthorized), it removes the token from `localStorage` and redirects the user to the login page.

### components/Navbar.tsx

Navigation component displayed on every page.

- **Dynamic Links:** Displays different links based on the user's role (e.g., client, employee, administrator).
- **Logout Functionality:** Allows users to log out by removing the token from `localStorage`.

### components/PrivateRoute.tsx

Component for protecting private routes within the application.

- **Authorization:** Checks if the user is logged in and has the appropriate role to access the route.
- **Redirection:** Redirects unauthorized users to the login page or an unauthorized access page.

---

## Page Descriptions

### HomePage

- **Path:** `/`
- **Description:** The application's home page, containing a welcome message and basic information about the rental service.

### CarsPage

- **Path:** `/car`
- **Description:** Displays a list of available cars for rent.
- **Functionality:** Fetches data from the API endpoint `/Car` and displays it as a list with links to detailed information.

### CarDetailsPage

- **Path:** `/Car/:id`
- **Description:** Shows detailed information about a selected car.
- **Functionality:**
    - Displays car details fetched from the API `/Car/{id}`.
    - Allows users to select rental dates and calculates the price based on the number of days.
    - Enables logged-in users to rent the car.

### LoginPage

- **Path:** `/login`
- **Description:** User login page.
- **Functionality:**
    - Allows users to log in using their email and password.
    - Upon successful login, saves the JWT token in `localStorage`.

### RegisterPage

- **Path:** `/register`
- **Description:** User registration page.
- **Functionality:**
    - Allows registration of a new client or, for employees and administrators, a new employee.
    - Sends registration data to the API `/Auth/Register`.

### ProfilePage

- **Path:** `/profile`
- **Description:** Profile page for the logged-in user.
- **Functionality:**
    - Displays user profile data fetched from the API (`/Client/MyProfile` or `/Employee/MyProfile`).
    - Allows users to edit their profile information and update it via the API.

### MyRentalsPage

- **Path:** `/myrental`
- **Description:** Shows a list of rentals for the logged-in client.
- **Functionality:**
    - Fetches rentals from the API `/Rental/MyRental`.
    - Displays details of each rental.

### ClientsPage

- **Path:** `/client`
- **Description:** Accessible by employees and administrators, displays a list of clients.
- **Functionality:**
    - Fetches client data from the API `/Client`.
    - Displays contact information and other client details.

### EmployeesPage

- **Path:** `/employee`
- **Description:** For employees and administrators, displays a list of employees.
- **Functionality:**
    - Fetches employee data from the API `/Employee`.
    - Displays information about each employee.

### RentalsPage

- **Path:** `/rental`
- **Description:** For employees and administrators, manages all rentals.
- **Functionality:**
    - Fetches all rentals from the API `/Rental/AllRental`.
    - Allows confirming car returns.
    - Enables granting discounts on rentals.

### ContactPage

- **Path:** `/contact`
- **Description:** Contact page with company information.
- **Functionality:**
    - Displays the rental service's address, phone number, and email address.

### UnauthorizedPage

- **Path:** `/unauthorized`
- **Description:** Informs the user that they do not have access to the requested page.

---

## Libraries Used

- **React** - A library for building user interfaces.
- **TypeScript** - A programming language that adds static typing to JavaScript.
- **Axios** - A library for making HTTP requests.
- **React Router** - Manages routing in a single-page application.
- **jwt-decode** - Decodes JWT tokens on the client side.

---

## Authors

- **[Norbert Karasek]**
- **[Co-author's Name]**

---

## Additional Notes

- **Security:** The JWT token is stored in `localStorage`, which can be susceptible to XSS attacks. Consider using HTTP-only cookies for enhanced security.
- **Form Validation:** Forms lack comprehensive data validation. It's recommended to add client-side validation.
- **Error Handling:** Currently, errors are handled using `alert`. For better user experience, implement more user-friendly error messages.

---

## Contact

If you have any questions or issues, please contact us at [norbert.karasek94@gmail.com](mailto:contact@carrental.com).

---