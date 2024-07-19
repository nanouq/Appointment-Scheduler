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
            reloadCustomers();
        }

        private void reloadCustomers()
        {
            mainText.Text = "Customers";
            appointmentsButton.Enabled = true;
            MySqlCommand cmd = new MySqlCommand("SELECT c.customerId, c.customerName, a.address, ci.city, a.postalCode, co.country, a.phone FROM customer c " +
                $"JOIN address a ON c.addressId = a.addressId JOIN city ci ON a.cityId = ci.cityId JOIN country co ON ci.countryId = co.countryId", DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            appointmentView.DataSource = dt;
            customerButton.Enabled = false;

            //set column names

            appointmentView.Columns[0].HeaderText = "Customer Id";
            appointmentView.Columns[1].HeaderText = "Customer Name";
            appointmentView.Columns[2].HeaderText = "Address";
            appointmentView.Columns[3].HeaderText = "City";
            appointmentView.Columns[4].HeaderText = "Zip Code";
            appointmentView.Columns[5].HeaderText = "Country";
            appointmentView.Columns[6].HeaderText = "Phone";
        }

        //Changes the datagridview and UI to appointment information (default)
        private void reloadAppointments()
        {
            mainText.Text = "Appointments";
            MySqlCommand cmd = new MySqlCommand("SELECT a.appointmentId, a.type, c.customerName, a.start, a.end FROM appointment a " +
                "JOIN customer c ON c.customerId = a.customerId ORDER BY a.start ASC", DBConnection.conn);
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
                reloadCustomers();
            }
            else
            {
                new AddAppointment(currentUser).ShowDialog();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (mainText.Text == "Customers")
            {
                if (appointmentView.SelectedRows.Count != 0)
                {
                    int customerId = (int)appointmentView.SelectedRows[0].Cells[0].Value;
                    MessageBox.Show($"{customerId}");
                    new UpdateCustomer(currentUser, customerId).ShowDialog();
                    reloadCustomers();
                }               
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (mainText.Text == "Customers")
            {
                if (appointmentView.SelectedRows.Count != 0)
                {
                    int customerId = (int)appointmentView.SelectedRows[0].Cells[0].Value;
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete customer: {appointmentView.SelectedRows[0].Cells[1].Value}?", "Delete Customer", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        //eventually delete all appointments associated with customer BEFORE deleting customer
                        //delete customer here
                        Helper.deleteCustomer(customerId);
                        MessageBox.Show("Customer was successfully deleted.");
                        reloadCustomers();
                    }
                }
            }
        }
    }
}
