using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class DeliveryDTO
    {
        public DeliveryAddressDTO address { get; set; }

        public string name { get; set; }

        public string shop_id { get; set; }

        public string type { get; set; }
    }
}
