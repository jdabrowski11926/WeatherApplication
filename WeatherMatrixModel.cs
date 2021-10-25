using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double feels_like { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class WeatherMatrixModel
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        public void printInfo()
        {
            switch (cod)
            {
                case 404:
                    string noDataString = "No data";
                    string dataFormat1 = String.Format("{0,20}|{1,30}|{2,20}|{3,20}|{4,20}|",
                        noDataString, noDataString, noDataString, noDataString, noDataString);
                    Console.WriteLine(dataFormat1);
                    break;
                case 200:
                    string temperature = string.Format("{0:0.00} oC", calculateKelvinToCelcius(main.temp));
                    string dataFormat2 = String.Format("{0,20}|{1,30}|{2,20}|{3,20}|{4,20}|",
                        weather[0].main, weather[0].description,
                        temperature, main.pressure, wind.speed);
                    Console.WriteLine(dataFormat2);
                    break;
            }
        }

        public double calculateKelvinToCelcius(double tempKelvin)
        {
            return tempKelvin - 273.15;
        }
    }
}
