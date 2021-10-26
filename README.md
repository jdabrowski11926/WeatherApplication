Program wyświetlający pogodę, wykorzystujący API https://countriesnow.space/ oraz https://openweathermap.org/api

**Uruchomienie programu**

Program można uruchomić poprzez środowisko uruchomieniowe Visual Studio, bądź za pomocą wiersza poleceń. Przy wywołaniu aplikacji za pomocą wiersza poleceń można podać argumenty wejściowe, według poniższego schematu:

PATH\WeatherApplication.exe ExportFilePath FileName Country CityLimit
  
  - ExportFilePath - folder, w którym ma być zapisany plik wyjściowy
  - FileName - nazwa pliku, do którego mają być zapisane dane
  - Country - nazwa kraju, z którego mają być pobierane miasta. API wymaga podania nazwy w języku angielskim
  - CityLimit - limit miast, z których mają być zapisane dane. Domyślna wartość to 100
  
Przykładowe wywyłanie:  E:\WeatherApplication.exe E:\ WeatherFile.xlsx Poland 100
  
**Działanie programu**
  
Schemat działania programu można zaobserwować w funkcji executeProgram() w klasie ProgramManager. Są w niej wywoływane następujące funkcje:
  
  - areStartArgumentsCorrect() - sprawdzenie czy argumenty wejściowe args zostały podane w odpowiedznim formacie
  - setStartingArguments() - przypisanie argumentów do zmiennych klasy
  - printStartingArguments() - wyświetlenie argumentów startowych
  - getCityList() - pobranie listy miast, wykorzystując zapytanie do API https://countriesnow.space/. Funkcja zmienia również format odpowiedzi z Json na listę string
  - getWeatherInfo() - pobranie informacji o pogodzie za pomocą API https://openweathermap.org/api. Funkcja zmienia również format odpowiedzi z Json na obiekty przechowujące informacje o pogodzie
  - printWeatherTable() - wyświetlenie tabeli danych w oknie konsoli
  - saveWeatherInfoToFile() - zapisanie danych do pliku z wykorzystaniem biblioteki Epplus
