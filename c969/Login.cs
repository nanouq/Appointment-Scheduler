using c969.Database;
using c969.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969
{
    public partial class Login : Form
    {
        public string errorMessage = "Invalid credentials.";
        public int userId;
        public string userName;
        static string filePath = "Login_History.txt";
        public Login()
        {
            InitializeComponent();
            locationLabel.Text = CultureInfo.CurrentCulture.DisplayName;

            //Translate login text to French when localization is changed.
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "fr")
            {
                usernameLabel.Text = "Nom d'utilisateur";
                passwordLabel.Text = "Mot de passe";
                loginButton.Text = "Entrer";
                loginLabel.Text = "Se connecter";
                currentLabel.Text = "Localisation actuelle:";
                this.Text = "Se connecter";
                errorMessage = "Les informations d'identification invalides.";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // TO DO --- ADD CREDENTIAL VERIFICATION
        private void loginButton_Click(object sender, EventArgs e)
        {       
            //Creates SQL username query
             MySqlCommand cmd = new MySqlCommand($"SELECT userId FROM user WHERE userName = '{usernameBox.Text}' AND password = '{passwordBox.Text}'", DBConnection.conn);
             MySqlDataReader reader = cmd.ExecuteReader();

            //Checks for instance of username and opens schedule window if found, alerts user to an error if not found
            if (reader.HasRows)
            {
                reader.Read();
                userId = Convert.ToInt32(reader[0]);
                userName = usernameBox.Text;
                reader.Close();
                MessageBox.Show("User " + userName + " was found. Their id is " + userId);

                //Write to Login_History file located in Bin folder
                TextWriter tw = new StreamWriter(filePath,true);
                tw.WriteLine($"{DateTime.Now.ToString()} - {userName} logged in.");
                tw.Close();

                User newUser = new User(usernameBox.Text, passwordBox.Text);
                Main mainForm = new Main(newUser);
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(errorMessage);
                reader.Close();
            }
        }
    }
}
