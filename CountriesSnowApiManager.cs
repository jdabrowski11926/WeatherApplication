using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace WeatherApplication
{
    class CountriesSnowApiManager
    {
        private TaskManager taskManager = new TaskManager();
        private string baseURL = "https://countriesnow.space/api/v0.1/countries";

        public string getCityNamesResponseString(String country)
        {
            string URL = $"{baseURL}/cities";
            var parameters = new Dictionary<string, string> { { "country", country } };
            Task<string> requestCitiesTask = taskManager.postAsyncTask(URL, parameters);
            requestCitiesTask.Wait();
            return requestCitiesTask.Result;
        }
    }
}
