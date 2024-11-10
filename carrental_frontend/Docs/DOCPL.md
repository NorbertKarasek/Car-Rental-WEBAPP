# Dokumentacja Projektu - Aplikacja Wypożyczalnia Samochodów

## Spis Treści

1. [Wstęp](#wstęp)
2. [Struktura Projektu](#struktura-projektu)
3. [Instalacja](#instalacja)
4. [Uruchomienie](#uruchomienie)
5. [Opis Komponentów](#opis-komponentów)
    - [api/axios.ts](#apiaxiosts)
    - [components/Navbar.tsx](#componentsnavbartsx)
    - [components/PrivateRoute.tsx](#componentsprivateroutetsx)
6. [Opis Stron (Pages)](#opis-stron-pages)
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
7. [Użyte Biblioteki](#użyte-biblioteki)
8. [Autorzy](#autorzy)

---

## Wstęp

Jest to frontend aplikacji webowej napisanej w React z użyciem TypeScript. Aplikacja służy do zarządzania wypożyczalnią samochodów, umożliwiając użytkownikom przeglądanie dostępnych pojazdów, wynajmowanie ich, a także zarządzanie swoim profilem. Pracownicy mają dodatkowe uprawnienia do zarządzania klientami i wynajmami.

---

## Struktura Projektu

Projekt jest zorganizowany w następujący sposób:

- **api/** - konfiguracja Axios do komunikacji z backendowym API.
- **components/** - komponenty współdzielone w aplikacji (np. nawigacja, trasy prywatne).
- **pages/** - poszczególne strony aplikacji (np. strona główna, logowanie, rejestracja).
- **utils/** - funkcje pomocnicze (np. do pobierania roli użytkownika z tokenu JWT).

---

## Instalacja

Aby zainstalować projekt lokalnie, wykonaj następujące kroki:

1. **Sklonuj repozytorium:**

   ```bash
   git clone [URL_REPOZYTORIUM]
   ```

2. **Przejdź do katalogu projektu:**

   ```bash
   cd carrental_frontend
   ```

3. **Zainstaluj zależności:**

   ```bash
   npm install
   ```

   lub

   ```bash
   yarn install
   ```

---

## Uruchomienie

Aby uruchomić aplikację w trybie deweloperskim:

1. **Ustaw zmienne środowiskowe:**

   Upewnij się, że w pliku `.env` jest ustawiona zmienna `REACT_APP_API_URL` wskazująca na backendowe API.

2. **Uruchom aplikację:**

   ```bash
   npm start
   ```

   lub

   ```bash
   yarn start
   ```

3. **Otwórz przeglądarkę:**

   Aplikacja będzie dostępna pod adresem [http://localhost:3000](http://localhost:3000).

---

## Opis Komponentów

### api/axios.ts

Konfiguracja Axios do komunikacji z backendem.

- **BaseURL:** Ustawia bazowy URL na wartość `REACT_APP_API_URL` z pliku `.env`.
- **Interceptor żądania:** Automatycznie dodaje token JWT z `localStorage` do nagłówka `Authorization` przy każdym żądaniu.
- **Interceptor odpowiedzi:** Jeśli odpowiedź ma status 401 (nieautoryzowany), usuwa token z `localStorage` i przekierowuje użytkownika na stronę logowania.

### components/Navbar.tsx

Komponent nawigacji wyświetlany na każdej stronie.

- **Dynamiczne linki:** Wyświetla różne linki w zależności od roli użytkownika (np. klient, pracownik, administrator).
- **Wylogowanie:** Umożliwia wylogowanie użytkownika poprzez usunięcie tokenu z `localStorage`.

### components/PrivateRoute.tsx

Komponent do ochrony prywatnych tras w aplikacji.

- **Autoryzacja:** Sprawdza, czy użytkownik jest zalogowany i czy ma odpowiednią rolę do dostępu do trasy.
- **Przekierowanie:** W przypadku braku autoryzacji przekierowuje na stronę logowania lub informuje o braku dostępu.

---

## Opis Stron (Pages)

### HomePage

- **Ścieżka:** `/`
- **Opis:** Strona główna aplikacji, zawiera powitanie i podstawowe informacje o wypożyczalni.

### CarsPage

- **Ścieżka:** `/car`
- **Opis:** Wyświetla listę dostępnych samochodów do wynajęcia.
- **Funkcjonalność:** Pobiera dane z API `/Car` i wyświetla je w formie listy z linkami do szczegółów.

### CarDetailsPage

- **Ścieżka:** `/Car/:id`
- **Opis:** Wyświetla szczegółowe informacje o wybranym samochodzie.
- **Funkcjonalność:**
    - Wyświetla dane samochodu pobrane z API `/Car/{id}`.
    - Umożliwia wybór dat wynajmu i oblicza cenę na podstawie liczby dni.
    - Umożliwia wynajęcie samochodu przez zalogowanego użytkownika.

### LoginPage

- **Ścieżka:** `/login`
- **Opis:** Strona logowania dla użytkowników.
- **Funkcjonalność:**
    - Umożliwia logowanie za pomocą adresu e-mail i hasła.
    - Po pomyślnym logowaniu zapisuje token JWT w `localStorage`.

### RegisterPage

- **Ścieżka:** `/register`
- **Opis:** Strona rejestracji nowych użytkowników.
- **Funkcjonalność:**
    - Umożliwia rejestrację klienta lub (dla pracowników i administratorów) rejestrację nowego pracownika.
    - Wysyła dane rejestracyjne do API `/Auth/Register`.

### ProfilePage

- **Ścieżka:** `/profile`
- **Opis:** Strona profilu zalogowanego użytkownika.
- **Funkcjonalność:**
    - Wyświetla dane profilu użytkownika pobrane z API (`/Client/MyProfile` lub `/Employee/MyProfile`).
    - Umożliwia edycję danych profilu i ich aktualizację w API.

### MyRentalsPage

- **Ścieżka:** `/myrental`
- **Opis:** Wyświetla listę wynajmów zalogowanego klienta.
- **Funkcjonalność:**
    - Pobiera wynajmy z API `/Rental/MyRental`.
    - Wyświetla szczegóły każdego wynajmu.

### ClientsPage

- **Ścieżka:** `/client`
- **Opis:** Strona dostępna dla pracowników i administratorów, wyświetla listę klientów.
- **Funkcjonalność:**
    - Pobiera dane klientów z API `/Client`.
    - Wyświetla informacje kontaktowe i inne dane klientów.

### EmployeesPage

- **Ścieżka:** `/employee`
- **Opis:** Strona dla pracowników i administratorów, wyświetla listę pracowników.
- **Funkcjonalność:**
    - Pobiera dane pracowników z API `/Employee`.
    - Wyświetla informacje o każdym pracowniku.

### RentalsPage

- **Ścieżka:** `/rental`
- **Opis:** Strona dla pracowników i administratorów, zarządzanie wszystkimi wynajmami.
- **Funkcjonalność:**
    - Pobiera wszystkie wynajmy z API `/Rental/AllRental`.
    - Umożliwia zatwierdzanie zwrotów samochodów.
    - Umożliwia przyznawanie zniżek na wynajmy.

### ContactPage

- **Ścieżka:** `/contact`
- **Opis:** Strona kontaktowa z informacjami o firmie.
- **Funkcjonalność:**
    - Wyświetla adres, numer telefonu i adres e-mail wypożyczalni.

### UnauthorizedPage

- **Ścieżka:** `/unauthorized`
- **Opis:** Informuje użytkownika o braku dostępu do żądanej strony.

---

## Użyte Biblioteki

- **React** - biblioteka do budowy interfejsów użytkownika.
- **TypeScript** - język programowania dodający typowanie statyczne do JavaScript.
- **Axios** - biblioteka do wykonywania zapytań HTTP.
- **React Router** - zarządzanie routingiem w aplikacji SPA.
- **jwt-decode** - dekodowanie tokenów JWT po stronie klienta.

---

## Autorzy

- **[Norbert Karasek]**
- **[Imię i Nazwisko Współtwórcy]**

---

## Uwagi Dodatkowe

- **Bezpieczeństwo:** Token JWT jest przechowywany w `localStorage`, co może być podatne na ataki XSS. Rozważ użycie ciasteczek HTTP-only dla zwiększenia bezpieczeństwa.
- **Walidacja Formularzy:** Formularze nie posiadają pełnej walidacji danych. Zaleca się dodanie walidacji po stronie klienta.
- **Obsługa Błędów:** Aktualnie błędy są obsługiwane poprzez `alert`. Dla lepszego UX warto zaimplementować bardziej przyjazne komunikaty.

---

## Kontakt

W razie pytań lub problemów prosimy o kontakt pod adresem [norbert.karasek94@gmail.com](mailto:kontakt@wypozyczalnia.pl).

---