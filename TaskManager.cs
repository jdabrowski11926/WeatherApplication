using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication
{
    class TaskManager
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> getAsyncTask(string URL)
        {
            var response = await client.GetAsync(URL);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

        public async Task<string> postAsyncTask(String URL, Dictionary<string, string> parameters)
        {
            var content = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(URL, content);
            var responseString = await response.Content.ReadAsStringAsync();
            return responseString;
        }

    }
}
