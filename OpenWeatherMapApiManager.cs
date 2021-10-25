using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication
{
    class OpenWeatherMapApiManager
    {
        private static readonly string openWeatherApiKey = "3128c40655d8abb347ba1d591bcf2557";
        private TaskManager taskManager = new TaskManager();
        private string baseURL = "https://api.openweathermap.org/data/2.5";

        public string getWeatherResponseString(string cityName)
        {
            string URL = $"{baseURL}/weather?q={cityName}&appid={openWeatherApiKey}";
            Task<string> requestWeatherTask = taskManager.getAsyncTask(URL);
            requestWeatherTask.Wait();
            return requestWeatherTask.Result;
        }

    }
}
