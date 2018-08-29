namespace SalesReporter.Models
{
    internal class SalesDTO
    {
        public CustomerDTO customer { get; set; }

        public BasketDTO basket { get; set; }

        public string comment { get; set; }
    }
}
