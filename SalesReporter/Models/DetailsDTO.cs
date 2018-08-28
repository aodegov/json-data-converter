using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class DetailsDTO
    {
        public string birth_date { get; set; }

        public object contactability { get; set; }

        public string creation_shop_id { get; set; } 

        public CustomerEmailDTO email { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public object mail { get; set; }

        public string main_shop_id { get; set; }

        public MeasurmentsDTO measurements { get; set; }

        public object phone { get; set; }
    }
}
