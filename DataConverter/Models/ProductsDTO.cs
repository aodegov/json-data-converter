using System.Collections.Generic;

namespace DataConverter.Models
{
    public class ProductsDTO
    {
        public string Reference { get; set; }

        public string Name { get; set; }

        public string ShortDescr { get; set; }

        public string LongDescr { get; set; }

        public string Groups { get; set; }

        public IList<VariantsDTO> Variants { get; set; }

    }
}
