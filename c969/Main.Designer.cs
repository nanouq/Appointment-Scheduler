namespace c969
{
    partial class Main
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
            this.appointmentView = new System.Windows.Forms.DataGridView();
            this.customerButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.mainText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.appointmentsButton = new System.Windows.Forms.Button();
            this.reportButton = new System.Windows.Forms.Button();
            this.allButton = new System.Windows.Forms.RadioButton();
            this.monthButton = new System.Windows.Forms.RadioButton();
            this.weekButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.appointmentCalendar = new System.Windows.Forms.MonthCalendar();
            this.dayButton = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentView)).BeginInit();
            this.SuspendLayout();
            // 
            // appointmentView
            // 
            this.appointmentView.AllowUserToAddRows = false;
            this.appointmentView.AllowUserToDeleteRows = false;
            this.appointmentView.AllowUserToResizeColumns = false;
            this.appointmentView.AllowUserToResizeRows = false;
            this.appointmentView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.appointmentView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.appointmentView.Location = new System.Drawing.Point(271, 53);
            this.appointmentView.Name = "appointmentView";
            this.appointmentView.ReadOnly = true;
            this.appointmentView.RowHeadersVisible = false;
            this.appointmentView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.appointmentView.Size = new System.Drawing.Size(713, 414);
            this.appointmentView.TabIndex = 0;
            // 
            // customerButton
            // 
            this.customerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerButton.Location = new System.Drawing.Point(720, 6);
            this.customerButton.Name = "customerButton";
            this.customerButton.Size = new System.Drawing.Size(129, 43);
            this.customerButton.TabIndex = 2;
            this.customerButton.Text = "Customers";
            this.customerButton.UseVisualStyleBackColor = true;
            this.customerButton.Click += new System.EventHandler(this.customerButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 281);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(227, 39);
            this.addButton.TabIndex = 3;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(12, 326);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(227, 39);
            this.editButton.TabIndex = 4;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(12, 371);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(227, 39);
            this.deleteButton.TabIndex = 5;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // mainText
            // 
            this.mainText.AutoSize = true;
            this.mainText.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainText.Location = new System.Drawing.Point(266, 23);
            this.mainText.Name = "mainText";
            this.mainText.Size = new System.Drawing.Size(164, 29);
            this.mainText.TabIndex = 6;
            this.mainText.Text = "Appointments";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(672, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "View:";
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(855, 473);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(129, 39);
            this.exitButton.TabIndex = 8;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.LightGray;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(253, 517);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // appointmentsButton
            // 
            this.appointmentsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appointmentsButton.Location = new System.Drawing.Point(855, 6);
            this.appointmentsButton.Name = "appointmentsButton";
            this.appointmentsButton.Size = new System.Drawing.Size(129, 43);
            this.appointmentsButton.TabIndex = 1;
            this.appointmentsButton.Text = "Appointments";
            this.appointmentsButton.UseVisualStyleBackColor = true;
            this.appointmentsButton.Click += new System.EventHandler(this.appointmentsButton_Click);
            // 
            // reportButton
            // 
            this.reportButton.Location = new System.Drawing.Point(12, 236);
            this.reportButton.Name = "reportButton";
            this.reportButton.Size = new System.Drawing.Size(227, 39);
            this.reportButton.TabIndex = 15;
            this.reportButton.Text = "Create Report";
            this.reportButton.UseVisualStyleBackColor = true;
            this.reportButton.Click += new System.EventHandler(this.reportButton_Click);
            // 
            // allButton
            // 
            this.allButton.AutoSize = true;
            this.allButton.BackColor = System.Drawing.Color.LightGray;
            this.allButton.Checked = true;
            this.allButton.Location = new System.Drawing.Point(184, 35);
            this.allButton.Name = "allButton";
            this.allButton.Size = new System.Drawing.Size(36, 17);
            this.allButton.TabIndex = 9;
            this.allButton.TabStop = true;
            this.allButton.Text = "All";
            this.allButton.UseVisualStyleBackColor = false;
            this.allButton.CheckedChanged += new System.EventHandler(this.allButton_CheckedChanged);
            // 
            // monthButton
            // 
            this.monthButton.AutoSize = true;
            this.monthButton.BackColor = System.Drawing.Color.LightGray;
            this.monthButton.Location = new System.Drawing.Point(123, 35);
            this.monthButton.Name = "monthButton";
            this.monthButton.Size = new System.Drawing.Size(55, 17);
            this.monthButton.TabIndex = 11;
            this.monthButton.TabStop = true;
            this.monthButton.Text = "Month";
            this.monthButton.UseVisualStyleBackColor = false;
            this.monthButton.CheckedChanged += new System.EventHandler(this.monthButton_CheckedChanged);
            // 
            // weekButton
            // 
            this.weekButton.AutoSize = true;
            this.weekButton.BackColor = System.Drawing.Color.LightGray;
            this.weekButton.Location = new System.Drawing.Point(63, 35);
            this.weekButton.Name = "weekButton";
            this.weekButton.Size = new System.Drawing.Size(54, 17);
            this.weekButton.TabIndex = 10;
            this.weekButton.TabStop = true;
            this.weekButton.Text = "Week";
            this.weekButton.UseVisualStyleBackColor = false;
            this.weekButton.CheckedChanged += new System.EventHandler(this.weekButton_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightGray;
            this.label3.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "View appointments by:";
            // 
            // appointmentCalendar
            // 
            this.appointmentCalendar.Location = new System.Drawing.Point(12, 62);
            this.appointmentCalendar.Name = "appointmentCalendar";
            this.appointmentCalendar.TabIndex = 16;
            this.appointmentCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.appointmentCalendar_DateChanged);
            // 
            // dayButton
            // 
            this.dayButton.AutoSize = true;
            this.dayButton.BackColor = System.Drawing.Color.LightGray;
            this.dayButton.Location = new System.Drawing.Point(13, 35);
            this.dayButton.Name = "dayButton";
            this.dayButton.Size = new System.Drawing.Size(44, 17);
            this.dayButton.TabIndex = 17;
            this.dayButton.TabStop = true;
            this.dayButton.Text = "Day";
            this.dayButton.UseVisualStyleBackColor = false;
            this.dayButton.CheckedChanged += new System.EventHandler(this.dayButton_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 517);
            this.Controls.Add(this.dayButton);
            this.Controls.Add(this.appointmentCalendar);
            this.Controls.Add(this.reportButton);
            this.Controls.Add(this.appointmentsButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.monthButton);
            this.Controls.Add(this.weekButton);
            this.Controls.Add(this.allButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mainText);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.customerButton);
            this.Controls.Add(this.appointmentView);
            this.Controls.Add(this.splitter1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Scheduler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.appointmentView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView appointmentView;
        private System.Windows.Forms.Button customerButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Label mainText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button appointmentsButton;
        private System.Windows.Forms.Button reportButton;
        private System.Windows.Forms.RadioButton allButton;
        private System.Windows.Forms.RadioButton monthButton;
        private System.Windows.Forms.RadioButton weekButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MonthCalendar appointmentCalendar;
        private System.Windows.Forms.RadioButton dayButton;
    }
}