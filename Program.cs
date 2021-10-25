using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WeatherApplication
{
    class Program
    {
        // https://documenter.getpostman.com/view/1134062/T1LJjU52?version=latest
        // https://openweathermap.org/api
        // PATH\WeatherApplication.exe E:\ weather2.xlsx France 100

        static void Main(string[] args)
        {
            ProgramManager programManager = new ProgramManager();
            programManager.executeProgram(args);
        }
    }
}
