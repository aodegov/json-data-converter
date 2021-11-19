using DataConverter.Models;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;

namespace DataConverter.DataLoader
{
    public class CustomersLoader: DataLoader<CustomersDTO>
    {
        public override List<CustomersDTO> LoadData(string file)
        {
            List<CustomersDTO> lstResult = new List<CustomersDTO>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(file, FileMode.Open, FileAccess.Read))
            {
                var reader = ExcelReaderFactory.CreateCsvReader(stream); 
                var lstCheck = new List<string>();
                var lastName = string.Empty;
                var id = 1;
                CustomersDTO customer = null;

                // Skip first row if necesary
                // reader.Read();

                while (reader.Read())
                {
                    var country = reader.GetString(8);
                    var currName = reader.GetString(0);

                    // Only France!!!
                    //if (country.Trim() != "France")
                    //{
                    //    continue;
                    //}

                    if (lastName != currName && !lstCheck.Contains(currName))
                    {
                        customer = new CustomersDTO();
                        customer.ID = id++;
                        customer.Name = reader.GetString(0)?.Trim();
                        customer.Code = reader.GetString(1);
                        customer.Address1 = reader.GetString(2);
                        customer.Address2 = reader.GetString(3);
                        customer.Address3 = reader.GetString(4);
                        customer.ZIP = reader.GetString(5);
                        customer.City = reader.GetString(6);
                        customer.State = reader.GetString(7);
                        customer.Coutry = country;
                        customer.Email = reader.GetString(9)?.Trim();
                        customer.SalesType = reader.GetString(10);
                        customer.Contacts = new List<ContactsDTO>() { new ContactsDTO {
                                                                                        ContactCode = reader.GetString(11),
                                                                                        ContactLastName = reader.GetString(12),
                                                                                        ContactFirstName = reader.GetString(13),
                                                                                        ContactEmail = reader.GetString(14)
                                                                                      }
                                                                    };

                        lastName = currName;

                        lstResult.Add(customer);

                        lstCheck.Add(currName);

                    }
                    else if (customer != null)
                    {
                        customer.Address1 = string.IsNullOrEmpty(customer.Address1.Trim()) ? reader.GetString(2) : customer.Address1;
                        customer.Address2 = string.IsNullOrEmpty(customer.Address2.Trim()) ? reader.GetString(3) : customer.Address2;
                        customer.Address3 = string.IsNullOrEmpty(customer.Address3.Trim()) ? reader.GetString(4) : customer.Address3;
                        customer.ZIP = string.IsNullOrEmpty(customer.ZIP.Trim()) ? reader.GetString(5) : customer.ZIP;
                        customer.State = string.IsNullOrEmpty(customer.State.Trim()) ? reader.GetString(7) : customer.State;
                        customer.Email = string.IsNullOrEmpty(customer.Email.Trim()) ? reader.GetString(9) : customer.Email;
                        customer.Contacts.Add(new ContactsDTO
                                                            {
                                                                ContactCode = reader.GetString(11),
                                                                ContactLastName = reader.GetString(12),
                                                                ContactFirstName = reader.GetString(13),
                                                                ContactEmail = reader.GetString(14)
                                                            }
                                              );

                        lstResult[lstResult.IndexOf(customer)] = customer;
                    }
                }
            }

            return lstResult;

        }
    }
}
