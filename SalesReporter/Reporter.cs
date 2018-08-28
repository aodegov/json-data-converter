using Newtonsoft.Json;
using SalesReporter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter
{
    class Reporter
    {
        private static string FormatCsvItem(string content)
        {
            return "\"" + content.Replace("\"", "\"\"") + "\"";
        }

        public static void CreateReports(string inputDir)
        {
            var files = Directory.GetFiles(inputDir, "*.json");

            foreach (var file in files)
            {
                using (StreamReader r = new StreamReader(file))
                {
                    var json = r.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<SalesModel>(json);
                    Console.WriteLine(result);
                }
            }
        }
    }
}
