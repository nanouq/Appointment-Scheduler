using c969.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969
{
    public partial class UpdateCustomer : Form
    {
        User currentUser;
        int customerID;
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        Customer oldCustomer = new Customer();

        public UpdateCustomer(User user, int customerId)
        {
            InitializeComponent();
            currentUser = user;
            customerID = customerId;
            loadCustomerInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadCustomerInfo()
        {
            try
            {
                string[] customerInfo = Helper.getCustomerInformation(customerID);
                string[] nameParts = customerInfo[0].Split(' ');
                
                firstNameBox.Text = nameParts[0];
                lastNameBox.Text = nameParts[1];
                addressBox.Text = customerInfo[1];
                addressTwoBox.Text = customerInfo[2];
                cityBox.Text = customerInfo[3];
                postalBox.Text = customerInfo[4];
                countryBox.Text = customerInfo[5];
                numberBox.Text = customerInfo[6];

                oldCustomer.CustomerId = customerID;
                oldCustomer.CustomerName = customerInfo[0];
                oldCustomer.Address = customerInfo[1];
                oldCustomer.AddressTwo = customerInfo[2];
                oldCustomer.City = customerInfo[3];
                oldCustomer.Zip = customerInfo[4];
                oldCustomer.Country = customerInfo[5];
                oldCustomer.Phone = customerInfo[6];

            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}");
            }
            
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            validateFields();
        }

        private void validateFields()
        {
            //regex that specifies the phone number field should only be formatted as 0000000000 or 000-000-0000
            Regex regex = new Regex(@"^(\d{3}-?\d{4}|\d{3}-?\d{3}-?\d{4})$");
            Match match = regex.Match(numberBox.Text.Trim());
            string firstName = textInfo.ToTitleCase(firstNameBox.Text.Trim());
            string lastName = textInfo.ToTitleCase(lastNameBox.Text.Trim());
            string address = addressBox.Text.Trim();
            string addressTwo = textInfo.ToTitleCase(addressTwoBox.Text.Trim());
            string city = textInfo.ToTitleCase(cityBox.Text.Trim());
            string postalCode = postalBox.Text.Trim();
            string country = textInfo.ToTitleCase(countryBox.Text.Trim());
            string phoneNumber = numberBox.Text.Trim();
            string fullName = $"{firstName} {lastName}";

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(postalCode) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please fill out all required fields.", "Missing entry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (match.Success == false)
            {
                MessageBox.Show("Please enter a valid phone number.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Customer c = new Customer(customerID, fullName, address, addressTwo, city, postalCode, country, phoneNumber);

                if (c.CustomerName == oldCustomer.CustomerName && c.Address == oldCustomer.Address && c.AddressTwo == oldCustomer.AddressTwo && c.City == oldCustomer.City
                    && c.Zip == oldCustomer.Zip && c.Country == oldCustomer.Country && c.Phone == oldCustomer.Phone)
                {
                    MessageBox.Show("No modifications found. Customer was not updated.", "No Changes Made", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {

                        Helper.updateCustomer(c, currentUser.username);
                        MessageBox.Show("Customer was updated successfully.");
                        this.Close();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }
        }
    }
}
