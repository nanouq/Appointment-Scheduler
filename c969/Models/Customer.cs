using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c969
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        
        public string Address {  get; set; }
        public string AddressTwo {  get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country {  get; set; }
        public string Phone { get; set; }

        public Customer() { }
        public Customer(int customerID, string customerName, string address, string addressTwo, string city, string zip, string country, string phone)
        {
            CustomerId = customerID;
            CustomerName = customerName;
            Address = address;
            AddressTwo = addressTwo;
            City = city;
            Zip = zip;
            Country = country;
            Phone = phone;
     
        }
    }
}
