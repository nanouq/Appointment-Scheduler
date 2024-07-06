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
    public partial class AddCustomer : Form
    {
        ErrorProvider errorFirstName = new ErrorProvider();
        ErrorProvider errorLastName = new ErrorProvider();
        ErrorProvider errorAddress = new ErrorProvider();
        ErrorProvider errorAddress2 = new ErrorProvider();
        ErrorProvider errorCity = new ErrorProvider();
        ErrorProvider errorZip = new ErrorProvider();
        ErrorProvider errorCountry = new ErrorProvider();
        ErrorProvider errorPhone = new ErrorProvider();
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
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.BackColor = Color.Salmon;
                errorFirstName.SetError(textBox1, "First name is required");
            }
            else
            {
                textBox1.BackColor = Color.White;
                errorFirstName.Clear();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.BackColor = Color.Salmon;
                errorLastName.SetError(textBox2, "Last name is required");
            }
            else
            {
                textBox2.BackColor = Color.White;
                errorLastName.Clear();
            }
        }
    }
}
