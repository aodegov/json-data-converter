using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class ProductDTO
    {
        public string brand { get; set; }

        public string[] family_ids { get; set; }

        public string id { get; set; }

        public string long_desc { get; set; }

        public List<MediasDTO> medias { get; set; }

        public string name { get; set; }

        public List<SelectorsDTO> selectors { get; set; }

        public string short_desc { get; set; }
    }
}
