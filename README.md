Projekt Zaginiony24 utworzony został przy użyciu .NET Core 3.1 oraz ReactJS Typescript
wersja npm: 6.14.4
wersja dotnet: 3.1.3

Po uruchomieniu projektu panel użytkownika dostępny jest po zalogowaniu na jedno z kont utworzonych przy inicjalizacji z pliku SeedData.cs lub poprzez rejestracje i logowanie na utworzone przez siebie konto.

Przykładowe dane do logowania użytkownika:
email: user@zaginiony24.pl
haslo: Passw0rd!

Panel administratora dostępny jest z poziomu paska nawigacyjnego i pola "Zarządzaj" po zalogowaniu na konto administratora i odświeżeniu strony!
email: admin@zaginiony24.pl
haslo: Passw0rd!

Aby projekt mógł zostać uruchomiony niezbędna jest baza danych SQL Server.
Connection string do bazy danych należy umieścić w pliku appsettings.json

W przypadku problemów z załadowaniem funkcjonalności front-endowych należy zastosować komendę "npm install" w folderze ClientApp.
