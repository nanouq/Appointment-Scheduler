using c969.Database;
using c969.Models;
using MySql.Data.MySqlClient;
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
    public partial class Main : Form
    {
        User currentUser;
        public Main(User user)
        {
            InitializeComponent();
            reloadAppointments();
            currentUser = user;
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //closes the hidden login form to close the whole program     
            Application.Exit();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        //Changes the datagridview and UI to customer information
        private void customerButton_Click(object sender, EventArgs e)
        {
            mainText.Text = "Customers";
            appointmentsButton.Enabled = true;
            MySqlCommand cmd = new MySqlCommand("SELECT customerId, customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy FROM customer", DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            appointmentView.DataSource = dt;
            customerButton.Enabled = false;
        }

        //Changes the datagridview and UI to appointment information (default)
        private void reloadAppointments()
        {
            mainText.Text = "Appointments";
            MySqlCommand cmd = new MySqlCommand("SELECT appointmentId, customerId, userId, type, start, end, createDate, createdBy, lastUpdate, lastUpdateBy FROM appointment", DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            appointmentView.DataSource = dt;
            appointmentsButton.Enabled = false;        
        }

        private void appointmentsButton_Click(object sender, EventArgs e)
        {    
            customerButton.Enabled = true;
            reloadAppointments();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (mainText.Text == "Customers")
            {
                new AddCustomer(currentUser).ShowDialog();
            }
        }
    }
}
