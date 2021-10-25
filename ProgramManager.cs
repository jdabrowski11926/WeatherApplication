using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication
{
    class ProgramManager
    {
        private readonly int defaultCityLimit = 100;
        private readonly string defaultCountryName = "Poland";
        private readonly string defaultOutputFileName = "weather.xlsx";

        private int cityLimit;
        private string countryName, outputExcelPath;
        private List<string> citiesList = new List<string>();
        private List<WeatherMatrixModel> cityWeatherList = new List<WeatherMatrixModel>();

        private CountriesSnowApiManager countriesSnowApiManager = new CountriesSnowApiManager();
        private JsonConverter jsonConverter = new JsonConverter();
        private OpenWeatherMapApiManager openWeatherMapApiManager = new OpenWeatherMapApiManager();
        private ExcelManager excelManager = new ExcelManager();

        public void executeProgram(string[] args)
        {
            if (areStartArgumentsCorrect(args))
            {
                setStartingArguments(args);
                printStartingArguments();
                getCityList();
                if (!citiesList.Any())
                    Console.WriteLine("BŁĄD - Nie znaleziono kraju o podanej nazwie");
                else
                {
                    getWeatherInfo();
                    printWeatherTable();
                    saveWeatherInfoToFile();
                } 
            }
        }

        public bool areStartArgumentsCorrect(string[] args)
        {
            if (args.Length > 0)
            {
                if (!Directory.Exists(args[0]))
                {
                    Console.WriteLine("BŁĄD - Argument 1 (ścieżka pliku excel) - podana ścieżka nie istnieje");
                    return false;
                }
            }
            if (args.Length > 3)
            {
                int cityNumber;
                if (!int.TryParse(args[3], out cityNumber))
                {
                    Console.WriteLine("BŁĄD - Argument 4 (limit miast) - wartość musi być liczbą całkowitą");
                    return false;
                }
                if (cityNumber < 1)
                {
                    Console.WriteLine("BŁĄD - Argument 4 (limit miast) - wartość musi być liczbą dodatnią");
                    return false;
                }
            }
            return true;
        }

        public void setStartingArguments(string[] args)
        {
            cityLimit = getCityLimit(args);
            countryName = getCountryName(args);
            outputExcelPath = getFilePath(args);
        }

        public string getFilePath(string[] args)
        {
            if (args.Length == 0)
                return getDefaultFilePath() + "\\" + defaultOutputFileName;
            if (args.Length == 1)
                return args[0] + defaultOutputFileName;
            else
                return args[0] + args[1];
        }

        public string getCountryName(string[] args)
        {
            if (args.Length > 2)
                return args[2];
            else
                return defaultCountryName;
        }

        public int getCityLimit(string[] args)
        {
            if (args.Length > 3)
                return Int32.Parse(args[3]);
            else
                return defaultCityLimit;
        }

        public string getDefaultFilePath()
        {
            string programPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var programDirectory = System.IO.Path.GetDirectoryName(programPath);
            return programDirectory;
        }

        public void printStartingArguments()
        {
            Console.WriteLine("CITY LIMIT : " + cityLimit);
            Console.WriteLine("COUNTRY NAME : " + countryName);
            Console.WriteLine("FILE PATH : " + outputExcelPath);
        }

        public void getCityList()
        {
            string responseString = countriesSnowApiManager.getCityNamesResponseString(countryName);
            if (jsonConverter.isResponseCorrect(responseString))
                citiesList = jsonConverter.getCityList(responseString);
        }

        public void getWeatherInfo()
        {
            int i = 0;
            while (i < cityLimit && i < citiesList.Count)
            {
                string responseStringWeather = openWeatherMapApiManager.getWeatherResponseString(citiesList[i]);
                WeatherMatrixModel weather = jsonConverter.getWeatherObject(responseStringWeather);
                cityWeatherList.Add(weather);
                i++;
            }
        }

        public void printWeatherTable()
        {
            printTableHeader();
            for (int i=0; i<cityWeatherList.Count; i++)
            {
                string cityNameFormated = String.Format("{0,30}|", citiesList[i]);
                Console.Write(cityNameFormated);
                cityWeatherList[i].printInfo();
            }
        }

        public void printTableHeader()
        {
            string headerFormat = String.Format("{0,30}|{1,20}|{2,30}|{3,20}|{4,20}|{5,20}|",
                        "City name", "Weather", "Weather description",
                        "Temperature", "Pressure", "Wind speed");
            Console.WriteLine(headerFormat);
            Console.WriteLine("__________________________________________________________________________________________________________________________________________________");
        }

        public void saveWeatherInfoToFile()
        {
            excelManager.exportToFile(outputExcelPath, citiesList, cityWeatherList);
        }
    }
}
