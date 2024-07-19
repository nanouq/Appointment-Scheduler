using c969.Models;
using Google.Protobuf.WellKnownTypes;
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
    public partial class AddAppointment : Form
    {
        User currentUser;
        public AddAppointment(User user)
        {
            InitializeComponent();
            loadCustomers();
            loadAppointmentLengths();
            updateEndTime();
            currentUser = user;
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtp1.Value;
            //DateTime endTime = dtp2.Value;
            int appointmentLength = int.Parse(comboLength.SelectedItem.ToString().Split(' ')[0]);
            DateTime endTime = startTime.AddMinutes(appointmentLength);
            int selectedCustomerId = (int)comboCustomers.SelectedValue;
            DateTime currentTime = DateTime.Now;


            if (endTime <= startTime)
            {
                MessageBox.Show("End time must be after start time.", "Invalid Time", MessageBoxButtons.OK);
            }
            else if (string.IsNullOrEmpty(appointmentType.Text))
            {
                MessageBox.Show("Please enter an appointment type.", "Invalid Appointment", MessageBoxButtons.OK);
            }
            else if(isAppointmentOverlapping(startTime, endTime, currentUser.userId))
            {
                MessageBox.Show("Overlapping appointments. Please select a new appointment time.","Overlapping Appointment", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show($"Appointment is correct. Customer Id is: {selectedCustomerId}");
                //enter appointment into database
                Appointment appt = new Appointment(Helper.getNextID("appointmentId","appointment"), selectedCustomerId, currentUser.userId, appointmentType.Text, 
                    convertTimeToSQL(startTime), convertTimeToSQL(endTime), currentTime, currentUser.username, currentTime, currentUser.username);
                try
                {
                    Helper.addAppointment(appt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }     

            }
        }

        private string convertTimeToSQL(DateTime time)
        {
            return time.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void updateEndTime()
        {
            int appointmentLength = int.Parse(comboLength.SelectedItem.ToString().Split(' ')[0]);
            dtp2.Value = dtp1.Value.AddMinutes(appointmentLength);
        }

        private bool isValidAppointmentTime(DateTime time)
        {
            //convert to EST
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            DateTime estTime = TimeZoneInfo.ConvertTime(time, est);

            if (estTime.TimeOfDay < new TimeSpan(9,0,0) || estTime.TimeOfDay > new TimeSpan(17,0,0))
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void comboCustomers_Format(object sender, ListControlConvertEventArgs e)
        {
            DataRowView row = (DataRowView)e.ListItem;
            e.Value = $"{row["customerId"]} - {row["customerName"]}";
        }

        private void comboLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateEndTime();
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
