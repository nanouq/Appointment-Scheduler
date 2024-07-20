namespace c969
{
    partial class AddAppointment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label9 = new System.Windows.Forms.Label();
            this.dtp1 = new System.Windows.Forms.DateTimePicker();
            this.dtp2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.appointmentType = new System.Windows.Forms.TextBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboCustomers = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboLength = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(43, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(289, 29);
            this.label9.TabIndex = 19;
            this.label9.Text = "Appointment Information";
            // 
            // dtp1
            // 
            this.dtp1.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtp1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp1.Location = new System.Drawing.Point(80, 191);
            this.dtp1.Name = "dtp1";
            this.dtp1.Size = new System.Drawing.Size(200, 20);
            this.dtp1.TabIndex = 20;
            this.dtp1.ValueChanged += new System.EventHandler(this.dtp1_ValueChanged);
            // 
            // dtp2
            // 
            this.dtp2.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtp2.Enabled = false;
            this.dtp2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp2.Location = new System.Drawing.Point(80, 244);
            this.dtp2.MinDate = new System.DateTime(2024, 7, 18, 0, 0, 0, 0);
            this.dtp2.Name = "dtp2";
            this.dtp2.Size = new System.Drawing.Size(200, 20);
            this.dtp2.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Start time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "End time:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Type of appointment:";
            // 
            // appointmentType
            // 
            this.appointmentType.Location = new System.Drawing.Point(80, 92);
            this.appointmentType.Name = "appointmentType";
            this.appointmentType.Size = new System.Drawing.Size(200, 20);
            this.appointmentType.TabIndex = 25;
            // 
            // submitButton
            // 
            this.submitButton.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitButton.Location = new System.Drawing.Point(192, 354);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(129, 43);
            this.submitButton.TabIndex = 26;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(48, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 43);
            this.button1.TabIndex = 27;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Customer:";
            // 
            // comboCustomers
            // 
            this.comboCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCustomers.FormattingEnabled = true;
            this.comboCustomers.Location = new System.Drawing.Point(80, 299);
            this.comboCustomers.Name = "comboCustomers";
            this.comboCustomers.Size = new System.Drawing.Size(200, 21);
            this.comboCustomers.TabIndex = 29;
            this.comboCustomers.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.comboCustomers_Format);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 30;
            this.label5.Text = "Appointment Length:";
            // 
            // comboLength
            // 
            this.comboLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLength.FormattingEnabled = true;
            this.comboLength.Location = new System.Drawing.Point(80, 140);
            this.comboLength.Name = "comboLength";
            this.comboLength.Size = new System.Drawing.Size(200, 21);
            this.comboLength.TabIndex = 31;
            this.comboLength.SelectedIndexChanged += new System.EventHandler(this.comboLength_SelectedIndexChanged);
            // 
            // AddAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 440);
            this.Controls.Add(this.comboLength);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboCustomers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.appointmentType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp2);
            this.Controls.Add(this.dtp1);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddAppointment";
            this.Text = "Add Appointment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtp1;
        private System.Windows.Forms.DateTimePicker dtp2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox appointmentType;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboCustomers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboLength;
    }
}