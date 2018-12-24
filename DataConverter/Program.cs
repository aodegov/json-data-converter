using System;
using System.IO;
using DataConverter.Builders;
using DataConverter.Service;
using Microsoft.Extensions.Configuration;

namespace DataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");
#if DEBUG
           // configurationBuilder = configurationBuilder.AddUserSecrets<Program>();
#endif
            var configuration = configurationBuilder.Build();
            var converterConfiguration = ConfigurationBinder.Get<ConverterConfiguration>(configuration.GetSection("FilesData"));


            Console.WriteLine("Convertion started.....");
            //CustomerBuilder cb = new CustomerBuilder();
            //cb.BuildCustomers();

            //ProductsBuilder pb = new ProductsBuilder();
            //pb.BuildProducts();

            IWorker worker = new Worker(converterConfiguration);
            worker.CreateDB();

            Console.WriteLine("Convertion finished");
            Console.ReadKey();
        }
    }
}
