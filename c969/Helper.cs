using c969.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969
{
    public class Helper
    {
        //creates a country in database or returns ID of existing country
        public static int createCountry(string countryName, string username)
        {
            //check if the country already exists
            int countryId = 0;
            
            string query = $"SELECT countryId FROM country WHERE country = '{countryName}'";
            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                countryId = reader.GetInt32(0);
                reader.Close();
            }
            reader.Close();

            if (countryId == 0) //the country doesnt exist yet
            {
            
                countryId = getNextID("countryId", "country") + 1;
                
                string insertQuery = $"INSERT into country (countryId, country, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    $"VALUES ('{countryId}', '{countryName}', CURRENT_TIMESTAMP, '{username}', CURRENT_TIMESTAMP, '{username}')";

                MySqlCommand insert = new MySqlCommand(insertQuery, Database.DBConnection.conn);
  
                insert.ExecuteNonQuery();
                return countryId;
            }

            return countryId;
        }

        public static int getNextID(string itemID, string table)
        {
            int nextId;
            string query = $"SELECT max({itemID}) FROM {table}";
            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                if (reader[0] == DBNull.Value)
                {
                    reader.Close();
                    return 0;
                }
                nextId = reader.GetInt32(0);
                reader.Close();
                return nextId;
            }
            reader.Close();
            return 0;
        }

        public static bool createCustomer(string firstName, string lastName, string address, string addressTwo, string city, string postalCode, string country, string phoneNumber, string username)
        {
            int addressId = createAddress(address, addressTwo, city, postalCode, country, phoneNumber, username);
            string fullName = $"{firstName} {lastName}";
            int customerId = 0;

            //check to see if the customer is already in the database
            string query = $"SELECT customerId FROM customer WHERE customerName = '{fullName}' AND addressId = '{addressId}'";
            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) //the combination of name and address exist already
            {
                reader.Read();
                customerId = reader.GetInt32(0);
                reader.Close();
                return false;
            }
            reader.Close();

            customerId = getNextID("customerId","customer") + 1;
            //if the combination doesnt exist yet 
            string insertQuery = $"INSERT into customer (customerId, customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    $"VALUES ('{customerId}', '{fullName}', '{addressId}', 1, CURRENT_TIMESTAMP, '{username}', CURRENT_TIMESTAMP, '{username}')";

            MySqlCommand insert = new MySqlCommand(insertQuery, Database.DBConnection.conn);

            insert.ExecuteNonQuery();

            return true;
        }

        public static int createAddress(string address, string addressTwo, string city, string postalCode, string country, string phoneNumber, string username)
        {
            int cityId = createCity(city, country, username);
            int addressId = getNextID("addressId","address") + 1;

            //check to see if there is already an existing combination
            string query = $"SELECT addressId FROM address WHERE address = '{address}' AND address2 = '{addressTwo}' " +
                $"AND cityId = '{cityId}' AND postalCode = '{postalCode}' AND phone = '{phoneNumber}'";
            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) //the combination of address, city, and postal code exists
            {
                reader.Read();
                addressId = reader.GetInt32(0);
                reader.Close();
                return addressId;
            }
            reader.Close();
            

            //if the combination doesnt exist yet 
            string insertQuery = $"INSERT into address (addressId, address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    $"VALUES ('{addressId}', '{address}', '{addressTwo}', '{cityId}', '{postalCode}', '{phoneNumber}', CURRENT_TIMESTAMP, '{username}', CURRENT_TIMESTAMP, '{username}')";

            MySqlCommand insert = new MySqlCommand(insertQuery, Database.DBConnection.conn);

            insert.ExecuteNonQuery();

            return addressId;
        }

        public static int createCity(string city, string country, string username)
        {
            int countryId = createCountry(country, username);
            int cityId = 0;

            //check if city already exists
            
            string query = $"SELECT cityId FROM city WHERE city = '{city}' AND countryId = '{countryId}'";
            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) //the combination of city and country exist already
            {
                reader.Read();
                cityId = reader.GetInt32(0);
                reader.Close();
            }
            reader.Close();

            if (cityId == 0) //the combination of city and country does not exist yet
            {
            
                cityId = getNextID("cityId", "city") + 1;

                string insertQuery = $"INSERT into city (cityId, city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy) " +
                    $"VALUES ('{cityId}', '{city}', '{countryId}', CURRENT_TIMESTAMP, '{username}', CURRENT_TIMESTAMP, '{username}')";

                MySqlCommand insert = new MySqlCommand(insertQuery, Database.DBConnection.conn);

                insert.ExecuteNonQuery();
                return cityId;
            }
            
            return cityId;
        }

        public static string[] getCustomerInformation(int id)
        {
            string[] customerInfo = new string[7];


            string query = $"SELECT c.customerName, a.address, a.address2, ci.city, a.postalCode, co.country, a.phone FROM customer c " +
                $"JOIN address a ON c.addressId = a.addressId JOIN city ci ON a.cityId = ci.cityId JOIN country co ON ci.countryId = co.countryId WHERE customerId = {id}";
            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) 
            {
                reader.Read();
               for (int i = 0; i < 7; i++)
                {
                    customerInfo[i] = reader[i].ToString();
                }
                reader.Close();
                Console.WriteLine("ROW WAS FOUND");
            }
            reader.Close();

            return customerInfo;
        }

        public static void updateCustomer(Customer c, string username)
        {
            int addressId = createAddress(c.Address, c.AddressTwo, c.City, c.Zip, c.Country, c.Phone, username);

            string updateCustomerQuery = $"UPDATE customer SET customerName = '{c.CustomerName}', addressId = {addressId}, lastUpdate = CURRENT_TIMESTAMP, lastUpdateBy = '{username}' WHERE customerId = {c.CustomerId}";

            MySqlCommand cmd = new MySqlCommand(updateCustomerQuery, Database.DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

        public static void deleteCustomer(int id)
        {
            string query = $"DELETE FROM customer WHERE customerId = {id}";

            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            cmd.ExecuteNonQuery();
        }

        public static void addAppointment(Appointment appt)
        {

        }

    }
}
