using System;
using System.Collections.Generic;

namespace DataConverter.Models
{
    [Serializable]
    public class CustomersDTO
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Coutry { get; set; }
        public string Email { get; set; }
        public string SalesType { get; set; }
        public IList<ContactsDTO> Contacts { get; set; }
    }
}
