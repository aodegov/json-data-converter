using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class AvailabilitiesDTO
    {
        public float price { get; set; }

        public string[] restocking { get; set; }

        public string shop_id { get; set; }

        public string[] special_prices { get; set; }
    }
}
