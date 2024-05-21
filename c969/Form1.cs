using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ConnectB_Click(object sender, EventArgs e)
        {
            //get connection string
            string constr = ConfigurationManager.ConnectionStrings["localdb"].ConnectionString;

            //make connection
            MySqlConnection conn = null;

            //open connection
            try
            {
                conn = new MySqlConnection(constr);

                conn.Open();

                MessageBox.Show("Connection is open");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close connection
                if(conn != null)
                {
                    conn.Close();
                }
            }

        }
    }
}
