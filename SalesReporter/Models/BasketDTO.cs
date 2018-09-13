using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class BasketDTO
    {
        public string currency_iso4217 { get; set; }

        public int? items_quantity { get; set; }

        public string total { get; set; }

        public string items_total_amount { get; set; }

        public string shop_id { get; set; }

        public int total_discount { get; set; }

        public int delivery_fees_amount { get; set; }

        public List<ItemsDTO> items { get; set; }

    }
}
