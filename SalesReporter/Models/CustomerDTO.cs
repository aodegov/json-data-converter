using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class CustomerDTO
    {
        public DetailsDTO details { get; set; }

        public PaymentMeansDTO means_of_payment { get; set; }

        public ProfDetaisDTO professional_details { get; set; }

        public string id { get; set; }

        public bool added_by_app { get; set; }
    }
}
