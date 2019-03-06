using System;
using DataConverter.Models;
using DataConverter.DataLoader;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataConverter.Builders
{
    public class CustomerBuilder : IBuilder
    {
        private readonly ConverterConfiguration configuration;

        public CustomerBuilder(ConverterConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JProperty BuildData()
        {
            CustomersLoader customersLoader = new CustomersLoader();
            List<CustomersDTO> customersData = customersLoader.LoadData(this.configuration.CustomersInputFile);
            var customers = GetCustomers(customersData);
            return customers;
        }

        private JProperty GetCustomers(List<CustomersDTO> customers)
        {
            return new JProperty("customers",
                        new JArray(
                              customers.Select(customer => new JObject
                                                    {
                                                        { "id", customer.ID },
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
                              )));
        }
    }
}
