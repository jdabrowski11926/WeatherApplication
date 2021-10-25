using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WeatherApplication
{
    class JsonConverter
    {

        public List<string> getCityList(string jsonString)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);
            return jsonObject["data"].ToObject<List<string>>();
        }

        public WeatherMatrixModel getWeatherObject(string jsonString)
        {
            return JsonConvert.DeserializeObject<WeatherMatrixModel>(jsonString);
        }

        public bool isResponseCorrect(string jsonString)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);
            if (jsonObject.error == "true")
                return false;
            else
                return true;
        }
    }
}
