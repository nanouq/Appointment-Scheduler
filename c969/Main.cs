﻿using System;
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
        public Login loginForm; 
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            //closes the hidden login form to close the whole program
            loginForm.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            loginForm.Close();
            this.Close();
        }
    }
}
