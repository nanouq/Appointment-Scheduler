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
                //MessageBox.Show($"{customerInfo[0]} {customerInfo[6]}");
                firstNameBox.Text = nameParts[0];
                lastNameBox.Text = nameParts[1];
                addressBox.Text = customerInfo[1];
                addressTwoBox.Text = customerInfo[2];
                cityBox.Text = customerInfo[3];
                postalBox.Text = customerInfo[4];
                countryBox.Text = customerInfo[5];
                numberBox.Text = customerInfo[6];

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


            //1st check
            //check if all fields are filled out
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(city) ||
                string.IsNullOrEmpty(postalCode) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please fill out all required fields.");
            }
            else if (match.Success == false) //2nd check, phone number field validation
            {
                MessageBox.Show("Please enter a valid phone number.");
            }
            else
            {
                Customer c = new Customer(customerID, fullName, address, addressTwo, city, postalCode, country, phoneNumber);
                try//-----------------ADD UPDATE HERE
                {
                    Helper.updateCustomer(c, currentUser.username);


                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                MessageBox.Show("Success!");
            }
        }
    }
}
