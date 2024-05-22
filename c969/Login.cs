using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969
{
    public partial class Login : Form
    {
        public string errorMessage = "Invalid credentials.";
        public Login()
        {
            InitializeComponent();

            //Translate login text to French when localization is changed.
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "en")
            {
                usernameLabel.Text = "Nom d'utilisateur";
                passwordLabel.Text = "Mot de passe";
                loginButton.Text = "Entrer";
                this.Text = "Se connecter";
                errorMessage = "Les informations d'identification invalides.";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(errorMessage);
        }
    }
}
