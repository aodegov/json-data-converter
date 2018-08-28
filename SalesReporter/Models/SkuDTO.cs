using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesReporter.Models
{
    internal class SkuDTO
    {
        public List<AvailabilitiesDTO> availabilities { get; set; }

        public string id { get; set; }

        public List<MediasDTO> medias { get; set; }

        public string name { get; set; }

        public string[] related_sku_ids { get; set; }

        public List<SpecsDTO> specs { get; set; }
    }
}
