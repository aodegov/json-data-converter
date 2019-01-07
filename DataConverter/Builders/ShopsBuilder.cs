using Newtonsoft.Json.Linq;
using System.Linq;

namespace DataConverter.Builders
{
    public class ShopsBuilder : IBuilder
    {
        private readonly ConverterConfiguration configuration;

        public ShopsBuilder(ConverterConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JProperty BuildData()
        {
            var selectors = this.GetShops();
            return selectors;
        }

        private JProperty GetShops()
        {
            return new JProperty("shops",
                    new JArray(
                        new JObject
                                {
                                    { "id", "MOBILE" },
                                    { "short_name", "PIXICA" },
                                    { "long_name", "PIXICA" },
                                    {"currency", new JObject{
                                                                { "symbol", "€" },
                                                                { "label", "EURO" },
                                                                { "iso4217", "EUR" },
                                                            }
                                    },
                                    { "mail", new JObject{
                                                            { "address1", string.Empty },
                                                            { "address2", string.Empty },
                                                            { "address3", string.Empty },
                                                            { "zipcode", string.Empty },
                                                            { "city", "PARIS" },
                                                            { "country", "France" }
                                                         }
                                    },
                                    { "contact", new JObject{
                                                                { "phone_number", "01 44 54 81 62" },
                                                                { "fax_number", string.Empty },
                                                                { "email", "info@pixika.com" }
                                                            }
                                    },
                                    { "geolocation", new JObject{
                                                                { "longitude", 2.294747 },
                                                                { "latitude", 48.858222 }
                                                            }
                                    },

                                    { "shop_type" , "web"},
                                    { "active" , true},
                                    { "area" , 13200000.0 }
                                }
                            ));
        }
    }
}
