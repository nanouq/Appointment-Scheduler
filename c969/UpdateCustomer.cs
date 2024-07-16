using c969.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969
{
    public partial class UpdateCustomer : Form
    {
        User currentUser;
        int customerID;
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
    }
}
