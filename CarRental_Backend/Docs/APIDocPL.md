# Dokumentacja API CarRental_Backend

## Spis Treści

1. [Wprowadzenie](#wprowadzenie)
2. [Autentykacja](#autentykacja)
3. [Obsługa Błędów](#obsługa-błędów)
4. [Endpointy API](#endpointy-api)
   - [AuthController](#authcontroller)
     - [Rejestracja Użytkownika](#rejestracja-użytkownika)
     - [Logowanie Użytkownika](#logowanie-użytkownika)
   - [CarController](#carcontroller)
     - [Pobierz Wszystkie Samochody](#pobierz-wszystkie-samochody)
     - [Pobierz Samochód po ID](#pobierz-samochód-po-id)
   - [ClientController](#clientcontroller)
     - [Pobierz Wszystkich Klientów](#pobierz-wszystkich-klientów)
     - [Pobierz Klienta po ID](#pobierz-klienta-po-id)
     - [Pobierz Mój Profil (Klient)](#pobierz-mój-profil-klient)
     - [Aktualizuj Mój Profil (Klient)](#aktualizuj-mój-profil-klient)
   - [EmployeeController](#employeecontroller)
     - [Pobierz Wszystkich Pracowników](#pobierz-wszystkich-pracowników)
     - [Pobierz Pracownika po ID](#pobierz-pracownika-po-id)
     - [Pobierz Mój Profil (Pracownik)](#pobierz-mój-profil-pracownik)
     - [Aktualizuj Mój Profil (Pracownik)](#aktualizuj-mój-profil-pracownik)
   - [RentalController](#rentalcontroller)
     - [Wypożycz Samochód](#wypożycz-samochód)
     - [Potwierdź Zwrot](#potwierdź-zwrot)
     - [Przyznaj Zniżkę](#przyznaj-zniżkę)
     - [Pobierz Moje Wypożyczenia](#pobierz-moje-wypożyczenia)
     - [Pobierz Wszystkie Wypożyczenia](#pobierz-wszystkie-wypożyczenia)
5. [Modele Danych](#modele-danych)
6. [Przykłady](#przykłady)
7. [Uwagi](#uwagi)
8. [Kontakt](#kontakt)

---

## Wprowadzenie

**CarRental_Backend API** udostępnia endpointy do zarządzania wypożyczalnią samochodów, użytkownikami (klientami i pracownikami) oraz autentykacją. Dokumentacja opisuje dostępne endpointy, ich użycie, wymaganą autentykację oraz oczekiwane dane wejściowe i wyjściowe.

---

## Autentykacja

API korzysta z **JWT (JSON Web Tokens)** do autentykacji. Po pomyślnym zalogowaniu otrzymasz token, który musi być dołączany w nagłówku `Authorization` do kolejnych żądań wymagających autentykacji.

### Format Nagłówka Autoryzacji

```
Authorization: Bearer {token}
```

Zastąp `{token}` swoim faktycznym tokenem JWT.

---

## Obsługa Błędów

API korzysta ze standardowych kodów statusu HTTP, aby wskazać sukces lub niepowodzenie żądań.

- **200 OK**: Żądanie zakończyło się sukcesem.
- **201 Created**: Zasób został pomyślnie utworzony.
- **204 No Content**: Żądanie zakończyło się sukcesem, brak treści do zwrócenia.
- **400 Bad Request**: Żądanie jest nieprawidłowe lub nie może być obsłużone.
- **401 Unauthorized**: Wymagana jest autentykacja, która nie powiodła się lub nie została dostarczona.
- **403 Forbidden**: Żądanie jest prawidłowe, ale serwer odmawia wykonania.
- **404 Not Found**: Żądany zasób nie został znaleziony.
- **500 Internal Server Error**: Wystąpił błąd po stronie serwera.

Odpowiedzi błędów zazwyczaj zawierają komunikat wyjaśniający przyczynę błędu.

---

## Endpointy API

### AuthController

#### Rejestracja Użytkownika

Rejestruje nowego użytkownika (klienta lub pracownika).

- **URL**: `/api/Auth/Register`
- **Metoda**: `POST`
- **Autentykacja**: Nie wymagana
- **Treść Żądania**:

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

- **Odpowiedź**:
  - **201 Created**: Użytkownik został pomyślnie utworzony.

    ```json
    "User created successfully!"
    ```

  - **400 Bad Request**: Błędy walidacji.

    ```json
    {
      "errors": {
        "nazwaPola": ["Komunikat błędu"]
      }
    }
    ```

---

#### Logowanie Użytkownika

Loguje użytkownika i zwraca token JWT.

- **URL**: `/api/Auth/Login`
- **Metoda**: `POST`
- **Autentykacja**: Nie wymagana
- **Treść Żądania**:

  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```

- **Odpowiedź**:
  - **200 OK**: Pomyślne logowanie.

    ```json
    {
      "token": "string"
    }
    ```

  - **401 Unauthorized**: Nieprawidłowa próba logowania.

    ```json
    "Invalid login attempt."
    ```

---

### CarController

#### Pobierz Wszystkie Samochody

Pobiera listę wszystkich samochodów.

- **URL**: `/api/Car`
- **Metoda**: `GET`
- **Autentykacja**: Nie wymagana
- **Odpowiedź**:
  - **200 OK**: Lista samochodów.

    ```json
    [
      {
        "carId": 1,
        "brand": "string",
        "model": "string",
        // ... inne właściwości samochodu
      },
      // ... inne samochody
    ]
    ```

---

#### Pobierz Samochód po ID

Pobiera szczegóły konkretnego samochodu na podstawie jego ID.

- **URL**: `/api/Car/{id}`
- **Metoda**: `GET`
- **Autentykacja**: Nie wymagana
- **Parametry URL**:
  - `id` (integer): ID samochodu.
- **Odpowiedź**:
  - **200 OK**: Szczegóły samochodu.

    ```json
    {
      "carId": 1,
      "brand": "string",
      "model": "string",
      // ... inne właściwości samochodu
    }
    ```

  - **404 Not Found**: Samochód nie został znaleziony.

---

### ClientController

#### Pobierz Wszystkich Klientów

Pobiera listę wszystkich klientów.

- **URL**: `/api/Client`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana (Role: **Employee**, **Administrator**)
- **Odpowiedź**:
  - **200 OK**: Lista klientów.

    ```json
    [
      {
        "clientId": "string",
        "firstName": "string",
        "surname": "string",
        // ... inne właściwości klienta
      },
      // ... inni klienci
    ]
    ```

---

#### Pobierz Klienta po ID

Pobiera szczegóły klienta na podstawie jego ID.

- **URL**: `/api/Client/ById/{id}`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana (Role: **Employee**, **Administrator**)
- **Parametry URL**:
  - `id` (string): ID klienta.
- **Odpowiedź**:
  - **200 OK**: Szczegóły klienta.

    ```json
    {
      "clientId": "string",
      "firstName": "string",
      "surname": "string",
      // ... inne właściwości klienta
    }
    ```

  - **404 Not Found**: Klient nie został znaleziony.

---

#### Pobierz Mój Profil (Klient)

Pobiera profil zalogowanego klienta.

- **URL**: `/api/Client/MyProfile`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana (Rola: **Client**)
- **Odpowiedź**:
  - **200 OK**: Profil klienta.

    ```json
    {
      "clientId": "string",
      "firstName": "string",
      "surname": "string",
      // ... inne właściwości klienta
    }
    ```

  - **404 Not Found**: Klient nie został znaleziony.

---

#### Aktualizuj Mój Profil (Klient)

Aktualizuje profil zalogowanego klienta.

- **URL**: `/api/Client/MyProfile`
- **Metoda**: `PUT`
- **Autentykacja**: Wymagana (Rola: **Client**)
- **Treść Żądania**:

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

- **Odpowiedź**:
  - **204 No Content**: Profil został pomyślnie zaktualizowany.
  - **404 Not Found**: Klient nie został znaleziony.

---

### EmployeeController

#### Pobierz Wszystkich Pracowników

Pobiera listę wszystkich pracowników.

- **URL**: `/api/Employee`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana
- **Odpowiedź**:
  - **200 OK**: Lista pracowników.

    ```json
    [
      {
        "employeeId": "string",
        "firstName": "string",
        "surname": "string",
        // ... inne właściwości pracownika
      },
      // ... inni pracownicy
    ]
    ```

---

#### Pobierz Pracownika po ID

Pobiera szczegóły pracownika na podstawie jego ID.

- **URL**: `/api/Employee/ById/{id}`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana
- **Parametry URL**:
  - `id` (string): ID pracownika.
- **Odpowiedź**:
  - **200 OK**: Szczegóły pracownika.

    ```json
    {
      "employeeId": "string",
      "firstName": "string",
      "surname": "string",
      // ... inne właściwości pracownika
    }
    ```

  - **404 Not Found**: Pracownik nie został znaleziony.

---

#### Pobierz Mój Profil (Pracownik)

Pobiera profil zalogowanego pracownika.

- **URL**: `/api/Employee/MyProfile`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana (Role: **Employee**, **Administrator**)
- **Odpowiedź**:
  - **200 OK**: Profil pracownika.

    ```json
    {
      "employeeId": "string",
      "firstName": "string",
      "surname": "string",
      // ... inne właściwości pracownika
    }
    ```

  - **404 Not Found**: Pracownik nie został znaleziony.

---

#### Aktualizuj Mój Profil (Pracownik)

Aktualizuje profil zalogowanego pracownika.

- **URL**: `/api/Employee/MyProfile`
- **Metoda**: `PUT`
- **Autentykacja**: Wymagana (Role: **Employee**, **Administrator**)
- **Treść Żądania**:

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

- **Odpowiedź**:
  - **204 No Content**: Profil został pomyślnie zaktualizowany.
  - **404 Not Found**: Pracownik nie został znaleziony.

---

### RentalController

#### Wypożycz Samochód

Tworzy nowe wypożyczenie (klient wypożycza samochód).

- **URL**: `/api/Rental/RentACar`
- **Metoda**: `POST`
- **Autentykacja**: Wymagana
- **Treść Żądania**:

  ```json
  {
    "carId": 1,
    "rentalDate": "2023-01-01T00:00:00Z",
    "returnDate": "2023-01-05T00:00:00Z"
  }
  ```

- **Odpowiedź**:
  - **200 OK**: Wypożyczenie zostało pomyślnie utworzone.

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

  - **400 Bad Request**: Błędy walidacji lub samochód niedostępny.

    ```json
    "Car is not available now."
    ```

  - **404 Not Found**: Samochód lub klient nie został znaleziony.

    ```json
    "Car not found!"
    ```

---

#### Potwierdź Zwrot

Potwierdza zwrot samochodu dla danego wypożyczenia.

- **URL**: `/api/Rental/{id}/ConfirmReturn`
- **Metoda**: `PUT`
- **Autentykacja**: Wymagana (Role: **Employee**, **Administrator**)
- **Parametry URL**:
  - `id` (integer): ID wypożyczenia.
- **Odpowiedź**:
  - **200 OK**: Zwrot został potwierdzony.

    ```json
    "Car return is approved."
    ```

  - **400 Bad Request**: Samochód został już zwrócony.

    ```json
    "Car has already been returned."
    ```

  - **404 Not Found**: Wypożyczenie nie zostało znalezione.

    ```json
    "Could not find rental."
    ```

---

#### Przyznaj Zniżkę

Przyznaje zniżkę do wypożyczenia.

- **URL**: `/api/Rental/{id}/ApplyDiscount`
- **Metoda**: `PUT`
- **Autentykacja**: Wymagana (Role: **Employee**, **Administrator**)
- **Parametry URL**:
  - `id` (integer): ID wypożyczenia.
- **Treść Żądania**:

  ```json
  {
    "discount": 0.1 // Liczba dziesiętna między 0 a 0.5
  }
  ```

- **Odpowiedź**:
  - **200 OK**: Zniżka została przyznana.

    ```json
    "Discount granted"
    ```

  - **400 Bad Request**: Nieprawidłowa wartość zniżki.

    ```json
    "Discount has to be between 0% and 50%."
    ```

  - **404 Not Found**: Wypożyczenie nie zostało znalezione.

    ```json
    "Rental not found!"
    ```

---

#### Pobierz Moje Wypożyczenia

Pobiera wypożyczenia powiązane z zalogowanym użytkownikiem (klientem lub pracownikiem).

- **URL**: `/api/Rental/MyRental`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana
- **Odpowiedź**:
  - **200 OK**: Lista wypożyczeń.

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
          // ... inne właściwości samochodu
        }
      },
      // ... inne wypożyczenia
    ]
    ```

  - **404 Not Found**: Użytkownik nie został znaleziony.

    ```json
    "User not found."
    ```

---

#### Pobierz Wszystkie Wypożyczenia

Pobiera wszystkie wypożyczenia.

- **URL**: `/api/Rental/AllRental`
- **Metoda**: `GET`
- **Autentykacja**: Wymagana (Role: **Employee**, **Administrator**)
- **Odpowiedź**:
  - **200 OK**: Lista wszystkich wypożyczeń.

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
          // ... inne właściwości samochodu
        },
        "client": {
          "clientId": "string",
          "firstName": "string",
          "surname": "string",
          "email": "string",
          "phoneNumber": "string"
          // ... inne właściwości klienta
        }
      },
      // ... inne wypożyczenia
    ]
    ```

---

## Modele Danych

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
  "discount": 0.1 // Liczba dziesiętna między 0 a 0.5
}
```

---

## Przykłady

### Rejestracja Nowego Klienta

**Żądanie**

```http
POST /api/Auth/Register
Content-Type: application/json

{
  "email": "jan.kowalski@example.com",
  "password": "Haslo123!",
  "confirmPassword": "Haslo123!",
  "firstName": "Jan",
  "lastName": "Kowalski",
  "phoneNumber": "123456789",
  "role": "Client"
}
```

**Odpowiedź**

```http
HTTP/1.1 201 Created

"User created successfully!"
```

---

### Logowanie

**Żądanie**

```http
POST /api/Auth/Login
Content-Type: application/json

{
  "email": "jan.kowalski@example.com",
  "password": "Haslo123!"
}
```

**Odpowiedź**

```http
HTTP/1.1 200 OK
Content-Type: application/json

{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

### Wypożycz Samochód

**Żądanie**

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

**Odpowiedź**

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

## Uwagi

- **Format Daty**: Wszystkie daty powinny być w formacie ISO 8601 (np. `"2023-01-01T00:00:00Z"`).
- **Wartości Dziesiętne**: Liczby dziesiętne powinny używać kropki `.` jako separatora dziesiętnego.
- **Autentykacja**: Endpointy wymagające autentykacji zwrócą `401 Unauthorized`, jeśli token jest nieobecny lub nieprawidłowy.
- **Autoryzacja**: Endpointy wymagające określonych ról zwrócą `403 Forbidden`, jeśli użytkownik nie posiada wymaganej roli.
- **Walidacja Danych**: Upewnij się, że wszystkie wymagane pola są dostarczone i poprawne podczas wysyłania żądań.

---

## Kontakt

W przypadku pytań lub problemów prosimy o kontakt ze mną pod adresem [norbert.karasek94@gmail.com].

---

**Nota**: Ta dokumentacja została wygenerowana na podstawie aktualnego stanu kontrolerów. Upewnij się, że endpointy API i modele są zgodne z najnowszą implementacją w Twoim projekcie.
**Data**: 2024-11-03

---