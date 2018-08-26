using System;
using DataConverter.Models;
using DataConverter.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataConverter.Builders
{
    public class CustomerBuilder
    {

        private readonly CustomersLoader custLoader;
        private readonly string customersInput;
        private readonly string customersOutput;

        public CustomerBuilder()
        {
            custLoader = new CustomersLoader();
            customersInput = @"C:\temp\AndroidPixika\Load\Customers.csv";
            customersOutput = @"C:\temp\AndroidPixika\Result\Companies.json";
        }

        public void BuildCustomers()
        {
            try
            {
                List<CustomersDTO> customers = custLoader.LoadData(customersInput);

                JObject export =
                new JObject(
                    new JProperty("customers",
                        new JArray(
                              customers.Select(customer => new JObject
                                                    {
                                                        { "_id", customer.ID },
                                                        {"professional_details", new JObject{
                                                                                                { "organization_name", customer.Name }
                                                                                            }
                                                        },
                                                        { "details", new JObject{
                                                                                    { "first_name", customer.Contacts.FirstOrDefault().ContactFirstName },
                                                                                    { "last_name", customer.Contacts.FirstOrDefault().ContactLastName },
                                                                                    { "emails",  new JArray {customer.Contacts.Select(contact => contact.ContactEmail).ToArray() } },
                                                                                    { "email", new JObject{
                                                                                                             { "professional", customer.Contacts.FirstOrDefault().ContactEmail }
                                                                                                          }
                                                                                    }
                                                                                }
                                                         },
                                                         { "basket", new JObject{
                                                                                     { "basket_offers",  new JArray() },
                                                                                     { "seller_offers", new JArray() },
                                                                                     { "items", new JArray() },

                                                                                     { "delivery_items", null },
                                                                                     { "currency_iso4217", "EUR" },
                                                                                     { "shop_id", "" },
                                                                                     { "delivery_fees_amount", 0.0 },
                                                                                     { "update_date", null },
                                                                                     { "items_quantity", 0 },
                                                                                     { "total", 0.0 },
                                                                                     { "total_discount", 0.0 },
                                                                                     { "items_total_amount", 0.0 },
                                                                                 }
                                                          },
                                                          { "pixika_email" , customer.PixikaEmail},
                                                          { "address1" , customer.Address1 },
                                                          { "address2" , customer.Address2 },
                                                          { "address3" , customer.Address3 },
                                                          { "zip" , customer.ZIP },
                                                          { "city" , customer.City },
                                                          { "state" , customer.State },
                                                          { "country" , customer.Coutry },
                                                          { "activities" , null },
                                                          { "addresses" , null },
                                                          { "subscriptions" , null },
                                                          { "lookbooks" , null },
                                                          { "wishlist" , null }
                                                    }
                              ))));

                using (StreamWriter file = File.CreateText(customersOutput))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    export.WriteTo(writer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
