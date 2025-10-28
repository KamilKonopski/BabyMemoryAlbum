# 👶 Baby Memory Album

**Baby Memory Album** to prosta aplikacja desktopowa stworzona w technologii **WPF (.NET 8)**.  
Pozwala tworzyć cyfrowy album wspomnień dziecka – dodawać zdjęcia, opisy, daty oraz momenty z życia malucha.

---

## ✨ Funkcjonalności

- ➕ Dodawanie nowych wpisów (zdjęcie, opis, data, tytuł)
- 📅 Przeglądanie wspomnień chronologicznie
- ✏️ Edytowanie już istniejących wpisów
- 💾 Zapisywanie wszystkich danych lokalnie (plik `albumEntries.json`)
- 🎞️ Prosty efekt animacji przy zmianie wpisu
- ⚠️ Walidacja pustych pól (tytuł jest wymagany)

---

## 🛠️ Technologie

- **C# (.NET 8 / WPF)**
- **MVVM (Model-View-ViewModel)**
- **XAML**
- **JSON (System.Text.Json)** — lokalne przechowywanie danych
- **Własne style XAML / motywy kolorów**

---

## 🚀 Uruchamianie projektu

1. Otwórz plik `MojePierwsze.sln` w Visual Studio 2022  
2. Wybierz konfigurację `Release`  
3. Uruchom projekt lub wykonaj publikację:
   ```bash
   dotnet publish -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true
   ```
    Gotowy plik .exe znajdziesz w folderze:
   /bin/Release/net8.0-windows/win-x64/publish/

---
    
## 💡 Informacje dodatkowe

Przy pierwszym uruchomieniu aplikacja tworzy folder Data, w którym zapisuje wszystkie wpisy oraz zdjęcia.
Wszystkie dane są przechowywane lokalnie — bez potrzeby połączenia z internetem.
