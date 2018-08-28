using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class DeliveryAddressDTO
    {
        public string address1 { get; set; }

        public string address2 { get; set; }

        public string address3 { get; set; }

        public string city { get; set; }

        public string country { get; set; }

        public string zipcode { get; set; }
    }
}
