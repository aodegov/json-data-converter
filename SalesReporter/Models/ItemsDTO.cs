using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class ItemsDTO
    {
        public DeliveryDTO delivery { get; set; }

        public ProductDTO product { get; set; }

        public string product_id { get; set; }

        public int? qty { get; set; }

        public string sku_id { get; set; }

        public float? unit_price { get; set; }

        public string id { get; set; }
    }
}
