# Frontend Wypożyczalni Samochodów ![PL](https://img.shields.io/badge/-PL-red)

Witamy w aplikacji **Frontend Wypożyczalni Samochodów**! Jest to platforma internetowa zbudowana z użyciem React i TypeScript, zaprojektowana do efektywnego zarządzania usługą wynajmu samochodów.

## Funkcje

- **Autoryzacja Użytkowników:** Bezpieczne logowanie i rejestracja dla klientów oraz administratorów.
- **Przeglądanie Samochodów:** Wyświetlanie pełnej listy dostępnych samochodów z szczegółowymi informacjami.
- **Wynajem Samochodów:** Wybór dat wynajmu i bezproblemowy proces wynajmu samochodów.
- **Zarządzanie Profilem:** Użytkownicy mogą przeglądać i aktualizować swoje profile.
- **Narzędzia Administracyjne:** Administratorzy i pracownicy mogą zarządzać klientami, pracownikami oraz wynajmami.

## Rozpoczęcie Pracy

### Wymagania Wstępne

- **Node.js** (wersja 14 lub wyższa)
- **npm** lub **yarn**

### Instalacja

1. **Sklonuj Repozytorium**

   ```bash
   git clone [URL_REPOZYTORIUM]
   ```

2. **Przejdź do Katalogu Projektu**

   ```bash
   cd carrental_frontend
   ```

3. **Zainstaluj Zależności**

   Używając npm:

   ```bash
   npm install
   ```

   Lub używając yarn:

   ```bash
   yarn install
   ```

4. **Skonfiguruj Zmienne Środowiskowe**

   Utwórz plik `.env` w katalogu głównym i dodaj następującą linię:

   ```env
   REACT_APP_API_URL=adres_twojego_api_backend
   ```

   Zastąp `adres_twojego_api_backend` rzeczywistym adresem URL Twojego backendowego API.

### Uruchomienie Aplikacji

Uruchom serwer deweloperski:

Używając npm:

```bash
npm start
```

Lub używając yarn:

```bash
yarn start
```

Otwórz [http://localhost:3000](http://localhost:3000) w przeglądarce, aby zobaczyć aplikację.

## Dostępne Skrypty

W katalogu projektu możesz uruchomić:

- **Uruchom Serwer Deweloperski**

  ```bash
  npm start
  ```

  Uruchamia aplikację w trybie deweloperskim. Strona będzie automatycznie się odświeżać przy wprowadzaniu zmian.

- **Uruchom Testy**

  ```bash
  npm test
  ```

  Uruchamia testy w trybie interaktywnego obserwowania. Więcej informacji znajdziesz w sekcji dotyczącej uruchamiania testów.

- **Zbuduj Aplikację dla Produkcji**

  ```bash
  npm run build
  ```

  Buduje aplikację do folderu `build` w trybie produkcyjnym. Optymalizuje aplikację pod kątem wydajności.

- **Ejekcja Konfiguracji**

  ```bash
  npm run eject
  ```

  **Uwaga:** Jest to operacja jednokierunkowa. Po wykonaniu tego kroku nie można cofnąć zmian!

  Jeśli nie jesteś zadowolony z narzędzia do budowania i wyborów konfiguracyjnych, możesz wykonać ejekcję w dowolnym momencie. Ta komenda usunie pojedynczą zależność budowania z projektu, kopiując wszystkie pliki konfiguracyjne i zależności transytywne (webpack, Babel, ESLint, itp.) bezpośrednio do projektu, abyś miał pełną kontrolę nad nimi. Wszystkie polecenia poza `eject` będą nadal działać, ale będą wskazywać na skopiowane skrypty, dzięki czemu możesz je modyfikować. Po tym kroku jesteś sam ze swoją konfiguracją.

  Nie musisz nigdy używać `eject`. Oferowany zestaw funkcji jest odpowiedni dla małych i średnich wdrożeń, i nie powinieneś czuć się zobowiązany do używania tej funkcji. Rozumiemy jednak, że to narzędzie nie byłoby użyteczne, gdybyś nie mógł go dostosować, gdy będzie to potrzebne.

## Dokumentacja

Kompleksowa dokumentacja projektu jest utrzymywana oddzielnie w katalogu `DOC/`. Prosimy o zapoznanie się z plikami markdown tam zawartymi, aby uzyskać szczegółowe informacje na temat struktury projektu, komponentów, stron i innych aspektów.

## Wkład

Wkład jest mile widziany! Prosimy o forkowanie repozytorium i zgłaszanie pull requestów w przypadku jakichkolwiek ulepszeń lub poprawek błędów.


## Kontakt

W razie jakichkolwiek pytań lub problemów, prosimy o kontakt pod adresem [norbert.karasek94@gmail.com](mailto:kontakt@wypozyczalnia.pl).

---

Dziękujemy za korzystanie z aplikacji Wypożyczalni Samochodów! Mamy nadzieję, że spełni ona Twoje oczekiwania.
