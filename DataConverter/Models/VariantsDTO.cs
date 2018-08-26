namespace DataConverter.Models
{
    public class VariantsDTO
    {
        public string VariantReference { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Capacity { get; set; }

        public string ImagePath { get; set; }

        public TariffsDTO Tariff { get; set; }
    }
}
