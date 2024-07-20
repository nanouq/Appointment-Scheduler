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
    public partial class UpdateAppointment : Form
    {
        User currentUser;
        int currentAppointmentId;
        Appointment oldAppointment = new Appointment();
        Appointment newAppointment = new Appointment();
        public UpdateAppointment(User user, int appointmentId)
        {
            InitializeComponent();
            currentUser = user;
            currentAppointmentId = appointmentId;
            loadCustomers();
            loadAppointmentLengths();
            loadAppointmentDetails();
        }

        private void loadAppointmentDetails()
        {
            try
            {
                oldAppointment = Helper.getAppointmentDetails(currentAppointmentId);

                if (oldAppointment != null)
                {
                    appointmentType.Text = oldAppointment.Type;
                    dtp1.Value = DateTime.Parse(oldAppointment.Start);
                    dtp2.Value = DateTime.Parse(oldAppointment.End);
                    comboCustomers.SelectedValue = oldAppointment.CustomerId;
                    int appointmentLengthMinutes = (int)(DateTime.Parse(oldAppointment.End) - DateTime.Parse(oldAppointment.Start)).TotalMinutes;
                    comboLength.SelectedItem = $"{appointmentLengthMinutes} minutes";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        private void updateEndTime()
        {
            int appointmentLength = int.Parse(comboLength.SelectedItem.ToString().Split(' ')[0]);
            dtp2.Value = dtp1.Value.AddMinutes(appointmentLength);
        }

        private void loadAppointmentLengths()
        {
            comboLength.Items.Clear();

            for (int i = 15; i <= 120; i += 15)
            {
                comboLength.Items.Add($"{i} minutes");
            }

            if (comboLength.Items.Count > 0)
            {
                comboLength.SelectedIndex = 0;
            }
        }

        private void loadCustomers()
        {
            string selectQuery = $"SELECT customerId, customerName FROM customer";
            DataTable customers = new DataTable();

            MySqlDataAdapter adp = new MySqlDataAdapter(selectQuery, Database.DBConnection.conn);
            adp.Fill(customers);

            comboCustomers.DataSource = customers;
            comboCustomers.DisplayMember = "customerName";
            comboCustomers.ValueMember = "customerId";

            comboCustomers.Format += new ListControlConvertEventHandler(comboCustomers_Format);
        }

        private void comboCustomers_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRowView row = (DataRowView)e.ListItem;
            e.Value = $"{row["customerId"]} - {row["customerName"]}";
        }

        private void comboLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateEndTime();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp1.Value;
            int appointmentLength = int.Parse(comboLength.SelectedItem.ToString().Split(' ')[0]);
            DateTime endTime = startTime.AddMinutes(appointmentLength);
            int selectedCustomerId = (int)comboCustomers.SelectedValue;
            string currentTime = convertTimeToSQL(DateTime.Now.ToUniversalTime());

            if(!didInputChange(oldAppointment))
            {
                MessageBox.Show("No changes were made to the appointment. Appointment was not updated.", "No Changes Made", MessageBoxButtons.OK);
            }
            else if (endTime <= startTime)
            {
                MessageBox.Show("Appointment end time must be after start time.", "Invalid Time", MessageBoxButtons.OK);
            }
            else if (string.IsNullOrEmpty(appointmentType.Text))
            {
                MessageBox.Show("Please enter an appointment type.", "Invalid Appointment", MessageBoxButtons.OK);
            }
            else if (!isValidAppointmentTime(startTime) || !isValidAppointmentTime(endTime))
            {
                MessageBox.Show("Appointments can only be scheduled Mon-Fri, 9AM to 5PM. Please choose a different time.", "Invalid Appointment", MessageBoxButtons.OK);
            }
            else if (convertTimeToSQL(startTime) != convertTimeToSQL(DateTime.Parse(oldAppointment.Start)) && convertTimeToSQL(endTime) != convertTimeToSQL(DateTime.Parse(oldAppointment.End)) && isAppointmentOverlapping(startTime, endTime, currentUser.userId))
            {
                //if (isAppointmentOverlapping(startTime, endTime, currentUser.userId))
                //{
                    MessageBox.Show($"The selected appointment creates overlapping appointments for {currentUser.username}. Please select a new appointment time.", "Overlapping Appointment", MessageBoxButtons.OK);
                //}
            }   
            else
            {
                //enter appointment into database
                Appointment appt = new Appointment(oldAppointment.AppointmentId, selectedCustomerId, currentUser.userId, appointmentType.Text,
                    convertTimeToSQL(startTime), convertTimeToSQL(endTime), currentTime, currentUser.username, currentTime, currentUser.username);
                try
                {
                    Helper.updateAppointment(appt);
                    MessageBox.Show("Appointment updated.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }

        private bool didInputChange(Appointment apt)
        {
            if (apt.Type != appointmentType.Text.Trim() || convertTimeToSQL(dtp1.Value) != convertTimeToSQL(DateTime.Parse(apt.Start)) || convertTimeToSQL(dtp2.Value) != 
                    convertTimeToSQL(DateTime.Parse(apt.End)) || apt.CustomerId != (int)comboCustomers.SelectedValue)
            {
                return true;
            }
            return false;
        }

        private string convertTimeToSQL(DateTime time)
        {
            return time.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        private bool isValidAppointmentTime(DateTime time)
        {
            //convert to EST
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime estTime = TimeZoneInfo.ConvertTime(time, est);

            if (estTime.TimeOfDay < new TimeSpan(9, 0, 0) || estTime.TimeOfDay > new TimeSpan(17, 0, 0))
            {
                return false;
            }

            //check for mon-fri

            if (estTime.DayOfWeek < DayOfWeek.Monday || estTime.DayOfWeek > DayOfWeek.Friday)
            {
                return false;
            }

            return true;
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            updateEndTime();
        }


        private bool isAppointmentOverlapping(DateTime startTime, DateTime endTime, int userId)
        {
            //am I counting overlapping appointments only for one user or ALL appointments?
            //for user:

            string convertedStart = convertTimeToSQL(startTime);
            string convertedEnd = convertTimeToSQL(endTime);

            string appointmentQuery = $"SELECT COUNT(*) FROM appointment WHERE userId = @userId AND ((@startTime <= end AND @endTime >= start))";

            using (MySqlCommand cmd = new MySqlCommand(appointmentQuery, Database.DBConnection.conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@endTime", convertedEnd);
                cmd.Parameters.AddWithValue("@startTime", convertedStart);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                MessageBox.Show(count.ToString());
                return count > 0;
            }

        }
    }
}
