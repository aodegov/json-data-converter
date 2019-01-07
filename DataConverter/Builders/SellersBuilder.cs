using Newtonsoft.Json.Linq;

namespace DataConverter.Builders
{
    public class SellersBuilder : IBuilder
    {
        private readonly ConverterConfiguration configuration;

        public SellersBuilder(ConverterConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JProperty BuildData()
        {
            var selectors = this.GetSellers();
            return selectors;
        }

        private JProperty GetSellers()
        {
            return new JProperty("sellers",
                  new JArray(
                      new JObject
                              {
                                    { "id", "PIXICA" },
                                    { "first_name", "PI" },
                                    { "last_name", "XICA" },
                                    { "has_temp_password", false },
                                    { "password", "1234" },
                                    {"basket", new JObject{
                                                                { "basket_offers", new JArray() },
                                                                { "creation_date", "2017-12-21T15:06:01.883Z" },
                                                                { "shop_id", "MOBILE" },
                                                                { "update_date", "2018-08-14T13:50:28.970Z" },
                                                                { "total", 0.0 },
                                                                { "total_discount", 0.0 },
                                                                { "delivery_fees_amount", 0.0 },
                                                                { "items_total_amount", 0.0 },
                                                                { "currency_iso4217", "EUR" },
                                                                { "items_quantity", 0 },
                                                                { "delivery_items", null },
                                                            }
                                    }
                              }
                          ));
        }
    }
}
