using c969.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969
{
    public partial class AddCustomer : Form
    {
        /*
        ErrorProvider errorFirstName = new ErrorProvider();
        ErrorProvider errorLastName = new ErrorProvider();
        ErrorProvider errorAddress = new ErrorProvider();
        ErrorProvider errorAddress2 = new ErrorProvider();
        ErrorProvider errorCity = new ErrorProvider();
        ErrorProvider errorZip = new ErrorProvider();
        ErrorProvider errorCountry = new ErrorProvider();
        ErrorProvider errorPhone = new ErrorProvider();
        */
        ErrorProvider errorProvider = new ErrorProvider();
        User currentUser;
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
            //regex that specifies the phone number field should only be formatted as 0000000000 or 000-000-0000
            Regex regex = new Regex(@"^\d{3}-?\d{3}-?\d{4}$");
            Match match = regex.Match(numberBox.Text.Trim());
            //1st check
            //check if all fields are filled out
            if (string.IsNullOrEmpty(firstNameBox.Text.Trim()) || string.IsNullOrEmpty(lastNameBox.Text.Trim()) || string.IsNullOrEmpty(addressBox.Text.Trim()) || string.IsNullOrEmpty(cityBox.Text.Trim()) ||
                string.IsNullOrEmpty(postalBox.Text.Trim()) || string.IsNullOrEmpty(countryBox.Text.Trim()) || string.IsNullOrEmpty(numberBox.Text.Trim()))
            {
                MessageBox.Show("Please fill out all required fields.");
            }
            else if (match.Success == false) //2nd check, phone number field validation
            {
                MessageBox.Show("Please enter a valid phone number.");
            }
            else
            {
                try
                {
                    int countryid = Helper.createCountry(countryBox.Text.Trim(), currentUser.username);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                
                MessageBox.Show("Success!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
