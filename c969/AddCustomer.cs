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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace c969
{
    public partial class AddCustomer : Form
    {
        User currentUser;
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        public AddCustomer(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            validateFields();
        }

        private void validateFields()
        {
            //Regex that specifies the phone number field should only be formatted as 0000000000 000-000-0000 0000000 or 000-0000
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
                try
                {
                    string fullName = $"{firstName} {lastName}";
                    Customer newCustomer = new Customer(Helper.getNextID("customerId","customer"), fullName, address, addressTwo, city, postalCode, country, phoneNumber);
                    bool didItCreate = Helper.createCustomer(newCustomer, currentUser.username);
                    if (didItCreate)
                    {
                        MessageBox.Show($"Customer successfully created.", "Success", MessageBoxButtons.OK);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"Unable to add customer. Customer already exists.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }            
            }
        }
    }
}
