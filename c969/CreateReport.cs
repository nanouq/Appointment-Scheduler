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
    public partial class CreateReport : Form
    {
        TimeZoneInfo localZone = TimeZoneInfo.Local;
        List<Appointment> appointments = new List<Appointment>();
        public CreateReport()
        {
            InitializeComponent();
            loadCombo();
            getUsers();
            loadAppointments();
        }

        private void loadAppointments()
        {
            string query = $"SELECT a.appointmentId, a.type, c.customerName, a.start, a.end, u.username FROM appointment a JOIN customer c ON c.customerId = a.customerId " +
                $"JOIN user u ON u.userId = a.userId";

            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
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
        }

        private void getUsers()
        {
            string query = $"SELECT username FROM user";
            MySqlCommand cmd = new MySqlCommand(query,Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                comboReport.Items.Add($"{reader.GetString("username")} Schedule");
            }
            reader.Close();
        }

        private void loadCombo()
        {
            comboReport.Items.Add("Types of appointments by month");
            comboReport.Items.Add("Customer Appointment History");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboReport.SelectedItem.ToString() == "Types of appointments by month")
            {
                appointmentsByMonth();
            }
            else if (comboReport.SelectedItem.ToString() == "Customer Appointment History")
            {
                customerAppointmentHistory();
            }
            else
            {
                userSchedule();
            }
        }

        private void customerAppointmentHistory()
        {
            //THIRD REPORT
            string username = comboReport.SelectedItem.ToString().Split(' ')[0];

            var report = appointments
                .GroupBy(a => new { a.CustomerName, a.Type })
                .Select(g => new
                {
                    CustomerName = g.Key.CustomerName,
                    AppointmentType = g.Key.Type,
                    Count = g.Count()
                }).OrderBy(a => a.CustomerName)
                .ToList();

            dgv.DataSource = report;
            dgv.Columns["AppointmentType"].HeaderText = "Appointment Type";
            dgv.Columns["CustomerName"].HeaderText = "Customer Name";
            dgv.Columns["Count"].HeaderText = "Number of appointments";
        }

        private void userSchedule()
        {
            string username = comboReport.SelectedItem.ToString().Split(' ')[0];

            var userScheduleReport = appointments
                .Where(a => a.Username == username)
                .OrderBy(a => a.dtStart)
                .Select(a => new
                {
                    Username = a.Username,
                    AppointmentType = a.Type,
                    CustomerName = a.CustomerName,
                    Start = a.dtStart,
                    End = a.dtEnd
                }).ToList();

            dgv.DataSource = userScheduleReport;
            dgv.Columns["AppointmentType"].HeaderText = "Appointment Type";
            dgv.Columns["CustomerName"].HeaderText = "Customer Name";
        }

        private void appointmentsByMonth()
        {
            var appointmentTypesByMonth = appointments
                .GroupBy(a => new { a.dtStart.Year, a.dtStart.Month, a.Type })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                    Type = g.Key.Type,
                    Count = g.Count()
                }).ToList();

            dgv.DataSource = appointmentTypesByMonth;
            dgv.Columns["Count"].HeaderText = "Number of appointments";


        }
    }
}
