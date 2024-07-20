using c969.Database;
using c969.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        TimeZoneInfo localZone = TimeZoneInfo.Local;
        public Main(User user)
        {
            InitializeComponent();
            reloadAppointments();
            currentUser = user;
            checkForUpcomingAppointments();
        }

        private void checkForUpcomingAppointments()
        {
            
            List<Appointment> upcomingAppointments = new List<Appointment>();
            string query = @"SELECT c.customerName, a.type, a.start FROM appointment a JOIN customer c ON c.customerId = a.customerId WHERE a.userId = @userId 
AND start BETWEEN @now AND @fifteenMinutesFromNow ORDER BY a.start ASC";

            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);

            cmd.Parameters.AddWithValue("@userId", currentUser.userId);
            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
            cmd.Parameters.AddWithValue("@fifteenMinutesFromNow", DateTime.UtcNow.AddMinutes(15));

            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                upcomingAppointments.Add(new Appointment
                {
                    CustomerName = reader.GetString("customerName"),
                    Type = reader.GetString("type"),
                    dtStart = reader.GetDateTime("start")
                });
            }
            reader.Close();

            if (upcomingAppointments.Count > 0)
            {
                string alert = $"Hello, {currentUser.username}. You have upcoming appointments:\n";
                foreach (var appointment in upcomingAppointments)
                {
                    alert += $"{appointment.Type} appointment with {appointment.CustomerName} at {(TimeZoneInfo.ConvertTimeFromUtc(appointment.dtStart, localZone)).ToString("HH:mm tt")}.";
                }
                MessageBox.Show(alert, "Upcoming Appointments Alert", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {  
            Application.Exit();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to close the application?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void customerButton_Click(object sender, EventArgs e)
        {
            reloadCustomers();
            allButton.Enabled = false;
            monthButton.Enabled = false;
            weekButton.Enabled = false;
            dayButton.Enabled = false;
            appointmentCalendar.Enabled = false;
        }

        private void reloadCustomers()
        {
            mainText.Text = "Customers";
            appointmentsButton.Enabled = true;
            MySqlCommand cmd = new MySqlCommand("SELECT c.customerId, c.customerName, a.address, ci.city, a.postalCode, co.country, a.phone FROM customer c " +
                $"JOIN address a ON c.addressId = a.addressId JOIN city ci ON a.cityId = ci.cityId JOIN country co ON ci.countryId = co.countryId ORDER BY c.customerId ASC", DBConnection.conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            appointmentView.DataSource = dt;
            customerButton.Enabled = false;

            appointmentView.Columns[0].HeaderText = "Customer Id";
            appointmentView.Columns[1].HeaderText = "Customer Name";
            appointmentView.Columns[2].HeaderText = "Address";
            appointmentView.Columns[3].HeaderText = "City";
            appointmentView.Columns[4].HeaderText = "Zip Code";
            appointmentView.Columns[5].HeaderText = "Country";
            appointmentView.Columns[6].HeaderText = "Phone";
        }

        private void reloadAppointments()
        {
            mainText.Text = "Appointments";
            MySqlCommand cmd = new MySqlCommand("SELECT a.appointmentId, a.type, c.customerName, a.start, a.end, u.username FROM appointment a " +
                "JOIN customer c ON c.customerId = a.customerId JOIN user u ON u.userId = a.userId ORDER BY a.start ASC", DBConnection.conn);

            List<Appointment> appointments = new List<Appointment>();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                appointments.Add(new Appointment
                {
                    AppointmentId = reader.GetInt32("appointmentId"),
                    Type = reader.GetString("type"),
                    CustomerName = reader.GetString("customerName"),
                    dtStart = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime("start"), localZone),
                    dtEnd = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime("end"), localZone),
                    Username = reader.GetString("username")
                });
            }
            reader.Close();

            DateTime selectedDate = appointmentCalendar.SelectionStart;
            DateTime startDate = selectedDate;
            DateTime endDate = selectedDate;

            if (dayButton.Checked)
            {
                startDate = selectedDate;
                endDate = selectedDate.AddDays(1).AddSeconds(-1);
            }
            else if (weekButton.Checked)
            {
                startDate = selectedDate.Date.AddDays(-(int)selectedDate.DayOfWeek);
                endDate = startDate.AddDays(7).AddSeconds(-1);
            }
            else if (monthButton.Checked)
            {
                startDate = new DateTime(selectedDate.Year, selectedDate.Month, 1);
                endDate = startDate.AddMonths(1).AddSeconds(-1);
            }

            var filteredAppointments = appointments;

            if (!allButton.Checked)
            {
                filteredAppointments = appointments.FindAll(a => a.dtStart >= startDate && a.dtEnd <= endDate);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("Appointment ID");
            dt.Columns.Add("Type");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Start");
            dt.Columns.Add("End");
            dt.Columns.Add("User");

            foreach (var appointment in filteredAppointments)
            {
                dt.Rows.Add(appointment.AppointmentId, appointment.Type, appointment.CustomerName, appointment.dtStart, appointment.dtEnd, appointment.Username);
            }

            appointmentView.DataSource = dt;
            appointmentsButton.Enabled = false;        
        }

        private void appointmentsButton_Click(object sender, EventArgs e)
        {    
            customerButton.Enabled = true;
            reloadAppointments();
            allButton.Enabled = true;
            monthButton.Enabled = true;
            weekButton.Enabled = true;
            dayButton.Enabled = true;
            appointmentCalendar.Enabled = true;
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
                reloadAppointments();
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (mainText.Text == "Customers")
            {
                if (appointmentView.SelectedRows.Count != 0)
                {
                    int customerId = Convert.ToInt32(appointmentView.SelectedRows[0].Cells[0].Value);
                    new UpdateCustomer(currentUser, customerId).ShowDialog();
                    reloadCustomers();
                }
            }
            else
            {
                int appointmentId = Convert.ToInt32(appointmentView.SelectedRows[0].Cells[0].Value);
                new UpdateAppointment(currentUser, appointmentId).ShowDialog();
                reloadAppointments();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (mainText.Text == "Customers")
            {
                if (appointmentView.SelectedRows.Count != 0)
                {
                    int customerId = Convert.ToInt32(appointmentView.SelectedRows[0].Cells[0].Value);
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete customer: {appointmentView.SelectedRows[0].Cells[1].Value} and all associated appointments?", "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Helper.deleteCustomer(customerId);
                        MessageBox.Show("Customer and all associated appointments were successfully deleted.");
                        reloadCustomers();
                    }
                }
            }
            else
            {
                if (appointmentView.SelectedRows.Count != 0)
                {
                    int appointmentId = Convert.ToInt32(appointmentView.SelectedRows[0].Cells[0].Value);              
                    DialogResult result = MessageBox.Show($"Are you sure you want to delete this appointment for: {appointmentView.SelectedRows[0].Cells[2].Value}?", "Delete Appointment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        Helper.deleteAppointment(appointmentId);
                        MessageBox.Show("Appointment was successfully deleted.");
                        reloadAppointments();
                    }
                }
            }
        }

        private void allButton_CheckedChanged(object sender, EventArgs e)
        {
            reloadAppointments();
        }

        private void monthButton_CheckedChanged(object sender, EventArgs e)
        {
            reloadAppointments();
        }

        private void weekButton_CheckedChanged(object sender, EventArgs e)
        {
            reloadAppointments();
        }

        private void dayButton_CheckedChanged(object sender, EventArgs e)
        {
            reloadAppointments();
        }

        private void appointmentCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            reloadAppointments();
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            new CreateReport().ShowDialog();
        }
    }
}
