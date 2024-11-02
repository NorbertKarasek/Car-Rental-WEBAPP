# CarRental_Backend API Documentation

## Table of Contents

1. [Introduction](#introduction)
2. [Authentication](#authentication)
3. [Error Handling](#error-handling)
4. [API Endpoints](#api-endpoints)
   - [AuthController](#authcontroller)
     - [Register User](#register-user)
     - [Login User](#login-user)
   - [CarController](#carcontroller)
     - [Get All Cars](#get-all-cars)
     - [Get Car By ID](#get-car-by-id)
   - [ClientController](#clientcontroller)
     - [Get All Clients](#get-all-clients)
     - [Get Client By ID](#get-client-by-id)
     - [Get My Profile (Client)](#get-my-profile-client)
     - [Update My Profile (Client)](#update-my-profile-client)
   - [EmployeeController](#employeecontroller)
     - [Get All Employees](#get-all-employees)
     - [Get Employee By ID](#get-employee-by-id)
     - [Get My Profile (Employee)](#get-my-profile-employee)
     - [Update My Profile (Employee)](#update-my-profile-employee)
   - [RentalController](#rentalcontroller)
     - [Rent a Car](#rent-a-car)
     - [Confirm Return](#confirm-return)
     - [Apply Discount](#apply-discount)
     - [Get My Rentals](#get-my-rentals)
     - [Get All Rentals](#get-all-rentals)
5. [Data Models](#data-models)
6. [Examples](#examples)
7. [Notes](#notes)
8. [Contact](#contact)

---

## Introduction

The **CarRental_Backend API** provides endpoints for managing car rentals, users (clients and employees), and authentication. This documentation details the available endpoints, their usage, required authentication, and expected inputs and outputs.

---

## Authentication

The API uses **JWT (JSON Web Tokens)** for authentication. After a successful login, you'll receive a token that must be included in the `Authorization` header of subsequent requests that require authentication.

### Authorization Header Format

```
Authorization: Bearer {token}
```

Replace `{token}` with your actual JWT token.

---

## Error Handling

The API uses standard HTTP status codes to indicate the success or failure of requests.

- **200 OK**: The request was successful.
- **201 Created**: A resource was successfully created.
- **204 No Content**: The request was successful, but there's no content to return.
- **400 Bad Request**: The request was invalid or cannot be served.
- **401 Unauthorized**: Authentication is required and has failed or not been provided.
- **403 Forbidden**: The request is valid, but the server is refusing action.
- **404 Not Found**: The requested resource could not be found.
- **500 Internal Server Error**: An error occurred on the server.

Error responses typically include a message explaining the reason for the error.

---

## API Endpoints

### AuthController

#### Register User

Registers a new user (either a **Client** or an **Employee**).

- **URL**: `/api/Auth/Register`
- **Method**: `POST`
- **Authentication**: None
- **Request Body**:

  ```json
  {
    "email": "string",
    "password": "string",
    "confirmPassword": "string",
    "firstName": "string",
    "lastName": "string",
    "phoneNumber": "string",
    "role": "Client" | "Employee"
  }
  ```

- **Response**:
  - **201 Created**: User created successfully.

    ```json
    "User created successfully!"
    ```

  - **400 Bad Request**: Validation errors.

    ```json
    {
      "errors": {
        "fieldName": ["Error message"]
      }
    }
    ```

---

#### Login User

Logs in a user and returns a JWT token.

- **URL**: `/api/Auth/Login`
- **Method**: `POST`
- **Authentication**: None
- **Request Body**:

  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```

- **Response**:
  - **200 OK**: Login successful.

    ```json
    {
      "token": "string"
    }
    ```

  - **401 Unauthorized**: Invalid login attempt.

    ```json
    "Invalid login attempt."
    ```

---

### CarController

#### Get All Cars

Retrieves a list of all cars.

- **URL**: `/api/Car`
- **Method**: `GET`
- **Authentication**: None
- **Response**:
  - **200 OK**: List of cars.

    ```json
    [
      {
        "carId": 1,
        "brand": "string",
        "model": "string",
        // ... other car properties
      },
      // ... other cars
    ]
    ```

---

#### Get Car By ID

Retrieves details of a specific car by its ID.

- **URL**: `/api/Car/{id}`
- **Method**: `GET`
- **Authentication**: None
- **URL Parameters**:
  - `id` (integer): The ID of the car.
- **Response**:
  - **200 OK**: Car details.

    ```json
    {
      "carId": 1,
      "brand": "string",
      "model": "string",
      // ... other car properties
    }
    ```

  - **404 Not Found**: Car not found.

---

### ClientController

#### Get All Clients

Retrieves a list of all clients.

- **URL**: `/api/Client`
- **Method**: `GET`
- **Authentication**: Required (Roles: **Employee**, **Administrator**)
- **Response**:
  - **200 OK**: List of clients.

    ```json
    [
      {
        "clientId": "string",
        "firstName": "string",
        "surname": "string",
        // ... other client properties
      },
      // ... other clients
    ]
    ```

---

#### Get Client By ID

Retrieves details of a client by their ID.

- **URL**: `/api/Client/ById/{id}`
- **Method**: `GET`
- **Authentication**: Required (Roles: **Employee**, **Administrator**)
- **URL Parameters**:
  - `id` (string): The ID of the client.
- **Response**:
  - **200 OK**: Client details.

    ```json
    {
      "clientId": "string",
      "firstName": "string",
      "surname": "string",
      // ... other client properties
    }
    ```

  - **404 Not Found**: Client not found.

---

#### Get My Profile (Client)

Retrieves the profile of the logged-in client.

- **URL**: `/api/Client/MyProfile`
- **Method**: `GET`
- **Authentication**: Required (Role: **Client**)
- **Response**:
  - **200 OK**: Client profile.

    ```json
    {
      "clientId": "string",
      "firstName": "string",
      "surname": "string",
      // ... other client properties
    }
    ```

  - **404 Not Found**: Client not found.

---

#### Update My Profile (Client)

Updates the profile of the logged-in client.

- **URL**: `/api/Client/MyProfile`
- **Method**: `PUT`
- **Authentication**: Required (Role: **Client**)
- **Request Body**:

  ```json
  {
    "firstName": "string",
    "surname": "string",
    "phoneNumber": "string",
    "address": "string",
    "city": "string",
    "country": "string",
    "dateOfBirth": "2023-01-01T00:00:00Z",
    "licenseNumber": "string",
    "licenseIssueDate": "2023-01-01T00:00:00Z"
  }
  ```

- **Response**:
  - **204 No Content**: Profile updated successfully.
  - **404 Not Found**: Client not found.

---

### EmployeeController

#### Get All Employees

Retrieves a list of all employees.

- **URL**: `/api/Employee`
- **Method**: `GET`
- **Authentication**: Required
- **Response**:
  - **200 OK**: List of employees.

    ```json
    [
      {
        "employeeId": "string",
        "firstName": "string",
        "surname": "string",
        // ... other employee properties
      },
      // ... other employees
    ]
    ```

---

#### Get Employee By ID

Retrieves details of an employee by their ID.

- **URL**: `/api/Employee/ById/{id}`
- **Method**: `GET`
- **Authentication**: Required
- **URL Parameters**:
  - `id` (string): The ID of the employee.
- **Response**:
  - **200 OK**: Employee details.

    ```json
    {
      "employeeId": "string",
      "firstName": "string",
      "surname": "string",
      // ... other employee properties
    }
    ```

  - **404 Not Found**: Employee not found.

---

#### Get My Profile (Employee)

Retrieves the profile of the logged-in employee.

- **URL**: `/api/Employee/MyProfile`
- **Method**: `GET`
- **Authentication**: Required (Roles: **Employee**, **Administrator**)
- **Response**:
  - **200 OK**: Employee profile.

    ```json
    {
      "employeeId": "string",
      "firstName": "string",
      "surname": "string",
      // ... other employee properties
    }
    ```

  - **404 Not Found**: Employee not found.

---

#### Update My Profile (Employee)

Updates the profile of the logged-in employee.

- **URL**: `/api/Employee/MyProfile`
- **Method**: `PUT`
- **Authentication**: Required (Roles: **Employee**, **Administrator**)
- **Request Body**:

  ```json
  {
    "firstName": "string",
    "surname": "string",
    "phoneNumber": "string",
    "address": "string",
    "city": "string",
    "country": "string",
    "dateOfBirth": "2023-01-01T00:00:00Z",
    "position": "string"
  }
  ```

- **Response**:
  - **204 No Content**: Profile updated successfully.
  - **404 Not Found**: Employee not found.

---

### RentalController

#### Rent a Car

Creates a new rental (client rents a car).

- **URL**: `/api/Rental/RentACar`
- **Method**: `POST`
- **Authentication**: Required
- **Request Body**:

  ```json
  {
    "carId": 1,
    "rentalDate": "2023-01-01T00:00:00Z",
    "returnDate": "2023-01-05T00:00:00Z"
  }
  ```

- **Response**:
  - **200 OK**: Rental created successfully.

    ```json
    {
      "rentalId": 1,
      "carId": 1,
      "clientId": "string",
      "rentalDate": "2023-01-01T00:00:00Z",
      "returnDate": "2023-01-05T00:00:00Z",
      "rentalPrice": 100.0,
      "isReturned": false
    }
    ```

  - **400 Bad Request**: Validation errors or car not available.

    ```json
    "Car is not available now."
    ```

  - **404 Not Found**: Car or client not found.

    ```json
    "Car not found!"
    ```

---

#### Confirm Return

Confirms the return of a car for a rental.

- **URL**: `/api/Rental/{id}/ConfirmReturn`
- **Method**: `PUT`
- **Authentication**: Required (Roles: **Employee**, **Administrator**)
- **URL Parameters**:
  - `id` (integer): The ID of the rental.
- **Response**:
  - **200 OK**: Return confirmed.

    ```json
    "Car return is approved."
    ```

  - **400 Bad Request**: Car has already been returned.

    ```json
    "Car has already been returned."
    ```

  - **404 Not Found**: Rental not found.

    ```json
    "Could not find rental."
    ```

---

#### Apply Discount

Applies a discount to a rental.

- **URL**: `/api/Rental/{id}/ApplyDiscount`
- **Method**: `PUT`
- **Authentication**: Required (Roles: **Employee**, **Administrator**)
- **URL Parameters**:
  - `id` (integer): The ID of the rental.
- **Request Body**:

  ```json
  {
    "discount": 0.1 // Decimal between 0 and 0.5
  }
  ```

- **Response**:
  - **200 OK**: Discount applied.

    ```json
    "Discount granted"
    ```

  - **400 Bad Request**: Invalid discount value.

    ```json
    "Discount has to be between 0% and 50%."
    ```

  - **404 Not Found**: Rental not found.

    ```json
    "Rental not found!"
    ```

---

#### Get My Rentals

Retrieves rentals associated with the logged-in user (client or employee).

- **URL**: `/api/Rental/MyRental`
- **Method**: `GET`
- **Authentication**: Required
- **Response**:
  - **200 OK**: List of rentals.

    ```json
    [
      {
        "rentalId": 1,
        "rentalDate": "2023-01-01T00:00:00Z",
        "returnDate": "2023-01-05T00:00:00Z",
        "rentalPrice": 100.0,
        "discount": 0.1,
        "additionalFees": 0.0,
        "isReturned": false,
        "returnDateActual": null,
        "car": {
          "carId": 1,
          "brand": "string",
          "model": "string"
          // ... other car properties
        }
      },
      // ... other rentals
    ]
    ```

  - **404 Not Found**: User not found.

    ```json
    "User not found."
    ```

---

#### Get All Rentals

Retrieves all rentals.

- **URL**: `/api/Rental/AllRental`
- **Method**: `GET`
- **Authentication**: Required (Roles: **Employee**, **Administrator**)
- **Response**:
  - **200 OK**: List of all rentals.

    ```json
    [
      {
        "rentalId": 1,
        "rentalDate": "2023-01-01T00:00:00Z",
        "returnDate": "2023-01-05T00:00:00Z",
        "rentalPrice": 100.0,
        "discount": 0.1,
        "additionalFees": 0.0,
        "isReturned": false,
        "returnDateActual": null,
        "car": {
          "carId": 1,
          "brand": "string",
          "model": "string"
          // ... other car properties
        },
        "client": {
          "clientId": "string",
          "firstName": "string",
          "surname": "string",
          "email": "string",
          "phoneNumber": "string"
          // ... other client properties
        }
      },
      // ... other rentals
    ]
    ```

---

## Data Models

### RegisterModel

```json
{
  "email": "string",
  "password": "string",
  "confirmPassword": "string",
  "firstName": "string",
  "lastName": "string",
  "phoneNumber": "string",
  "role": "Client" | "Employee"
}
```

---

### LoginModel

```json
{
  "email": "string",
  "password": "string"
}
```

---

### CreateRentalModel

```json
{
  "carId": 1,
  "rentalDate": "2023-01-01T00:00:00Z",
  "returnDate": "2023-01-05T00:00:00Z"
}
```

---

### UpdateClientProfileDto

```json
{
  "firstName": "string",
  "surname": "string",
  "phoneNumber": "string",
  "address": "string",
  "city": "string",
  "country": "string",
  "dateOfBirth": "2023-01-01T00:00:00Z",
  "licenseNumber": "string",
  "licenseIssueDate": "2023-01-01T00:00:00Z"
}
```

---

### UpdateEmployeeProfileDto

```json
{
  "firstName": "string",
  "surname": "string",
  "phoneNumber": "string",
  "address": "string",
  "city": "string",
  "country": "string",
  "dateOfBirth": "2023-01-01T00:00:00Z",
  "position": "string"
}
```

---

### DiscountModel

```json
{
  "discount": 0.1 // Decimal between 0 and 0.5
}
```

---

## Examples

### Register a New Client

**Request**

```http
POST /api/Auth/Register
Content-Type: application/json

{
  "email": "john.doe@example.com",
  "password": "Password123!",
  "confirmPassword": "Password123!",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "123456789",
  "role": "Client"
}
```

**Response**

```http
HTTP/1.1 201 Created

"User created successfully!"
```

---

### Login

**Request**

```http
POST /api/Auth/Login
Content-Type: application/json

{
  "email": "john.doe@example.com",
  "password": "Password123!"
}
```

**Response**

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

### Rent a Car

**Request**

```http
POST /api/Rental/RentACar
Authorization: Bearer {token}
Content-Type: application/json

{
  "carId": 1,
  "rentalDate": "2023-01-01T00:00:00Z",
  "returnDate": "2023-01-05T00:00:00Z"
}
```

**Response**

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "rentalId": 1,
  "carId": 1,
  "clientId": "string",
  "rentalDate": "2023-01-01T00:00:00Z",
  "returnDate": "2023-01-05T00:00:00Z",
  "rentalPrice": 400.0,
  "isReturned": false
}
```

---

## Notes

- **Date Formats**: All dates should be in ISO 8601 format (e.g., `"2023-01-01T00:00:00Z"`).
- **Decimal Values**: Decimal numbers should use a dot `.` as the decimal separator.
- **Authentication**: Endpoints that require authentication will return `401 Unauthorized` if the token is missing or invalid.
- **Authorization**: Endpoints that require specific roles will return `403 Forbidden` if the user does not have the required role.
- **Data Validation**: Ensure that all required fields are provided and valid when making requests.

---

## Contact

For any questions or issues, please contact me at [norbert.karasek94@gmail.com].

---

**Note**: This documentation is generated based on the current state of the controllers. Please ensure that the API endpoints and models match the latest implementation in your project.
**Date**: 2024-11-03

---