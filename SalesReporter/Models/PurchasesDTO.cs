using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class PurchasesDTO
    {
        public CustomerDTO customer { get; set; }

        public BasketDTO basket { get; set; }

        public string comment { get; set; }
    }
}
