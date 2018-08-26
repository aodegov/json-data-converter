using System;
using DataConverter.Builders;

namespace DataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Convertion started.....");
            CustomerBuilder cb = new CustomerBuilder();
            cb.BuildCustomers();

            ProductsBuilder pb = new ProductsBuilder();
            pb.BuildProducts(); 

            Console.WriteLine("Convertion finished");
            Console.ReadKey();
        }
    }
}
