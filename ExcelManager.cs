using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApplication
{
    class ExcelManager
    {
        public void exportToFile(string filePath, List<string> cityNames, List<WeatherMatrixModel> weatherList)
        {
            var file = new FileInfo(filePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(file))
            {
                if (doesFileContainsWorksheet(package, "Pogoda"))
                    package.Workbook.Worksheets.Delete("Pogoda");
                var sheet = package.Workbook.Worksheets.Add("Pogoda");
                writeDataToWorksheet(sheet, cityNames, weatherList);
                package.Save();
            }
        }

        private bool doesFileContainsWorksheet(ExcelPackage package, string worksheetName)
        {
            for(int i=0; i<package.Workbook?.Worksheets?.Count; i++)
            {
                if (package.Workbook.Worksheets[i].Name.Equals(worksheetName))
                    return true;
            }
            return false;
        }

        private void writeDataToWorksheet(ExcelWorksheet worksheet, List<string> cityNames, List<WeatherMatrixModel> weatherList)
        {
            writeHeader(worksheet);
            for (int i = 0; i < weatherList.Count; i++)
            {
                if (weatherList[i].cod == 200)
                    writeRowData(worksheet, cityNames, weatherList, i + 2);
                else
                    writeRowNoData(worksheet, cityNames, i + 2);
            }
        }
        private void writeHeader(ExcelWorksheet worksheet)
        {
            worksheet.Cells["A1"].Value = "City name";
            worksheet.Cells["B1"].Value = "Weather";
            worksheet.Cells["C1"].Value = "Weather description";
            worksheet.Cells["D1"].Value = "Temperature";
            worksheet.Cells["E1"].Value = "Pressure";
            worksheet.Cells["F1"].Value = "Wind speed";
        }

        private void writeRowData(ExcelWorksheet worksheet, List<string> cityNames, List<WeatherMatrixModel> weatherList, int rowNumber)
        {
            worksheet.Cells["A" + rowNumber].Value = cityNames[rowNumber - 2];
            worksheet.Cells["B" + rowNumber].Value = weatherList[rowNumber - 2].weather[0].main;
            worksheet.Cells["C" + rowNumber].Value = weatherList[rowNumber - 2].weather[0].description;
            worksheet.Cells["D" + rowNumber].Value = weatherList[rowNumber - 2].main.temp;
            worksheet.Cells["E" + rowNumber].Value = weatherList[rowNumber - 2].main.pressure;
            worksheet.Cells["F" + rowNumber].Value = weatherList[rowNumber - 2].wind.speed;
        }

        private void writeRowNoData(ExcelWorksheet worksheet, List<string> cityNames, int rowNumber)
        {
            worksheet.Cells["A" + rowNumber].Value = cityNames[rowNumber - 2];
            worksheet.Cells["B" + rowNumber].Value = "No data";
            worksheet.Cells["C" + rowNumber].Value = "No data";
            worksheet.Cells["D" + rowNumber].Value = "No data";
            worksheet.Cells["E" + rowNumber].Value = "No data";
            worksheet.Cells["F" + rowNumber].Value = "No data";
        }
    }
}
