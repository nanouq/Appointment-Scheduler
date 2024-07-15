using c969.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static void createCustomer(string firstName, string lastName, string address, string addressTwo, string city, string postalCode, string country, string phoneNumber, string username)
        {
            int addressId = createAddress(address, addressTwo, city, postalCode, country, phoneNumber, username);
        }

        public static int createAddress(string address, string addressTwo, string city, string postalCode, string country, string phoneNumber, string username)
        {
            int cityId = createCity(city, country, username);
            return 0;
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
    }
}
