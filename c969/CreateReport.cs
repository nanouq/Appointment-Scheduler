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
        public CreateReport()
        {
            InitializeComponent();
            loadCombo();
            getUsers();           
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
            comboReport.Items.Add("Etc");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboReport.SelectedItem.ToString())
            {
                case "Types of appointments by month":
                    appointmentsByMonth();
                    break;
                case "Schedule for all users":
                    userSchedule();
                    break;
                case "Etc":
                    break;
            }
        }

        private void userSchedule()
        {
            string username = comboReport.SelectedItem.ToString().Split(' ')[0];

            MySqlCommand cmd = new MySqlCommand($"SELECT u.username, a.type, c.customerName, a.start, a.end FROM appointment a " +
                $"JOIN customer c ON c.customerId = a.customerId JOIN user u ON u.userId = a.userId WHERE u.username = {username} ORDER BY a.start ASC", Database.DBConnection.conn);


        }

        private void appointmentsByMonth()
        {
            List<Appointment> appointments = new List<Appointment>();


            string query = "SELECT type, start FROM appointment";
            MySqlCommand cmd = new MySqlCommand(query, Database.DBConnection.conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                appointments.Add(new Appointment
                {
                    Type = reader.GetString("type"),
                    dtStart = TimeZoneInfo.ConvertTimeFromUtc(reader.GetDateTime("start"), localZone)
                });
            }


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

        }
    }
}
