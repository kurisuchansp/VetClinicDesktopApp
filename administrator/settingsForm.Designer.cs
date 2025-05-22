namespace VeterinaryClinicApp
{
    partial class settingsForm
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
            btnlogout = new custombtn();
            btnaddrfid = new custombtn();
            exitbtn = new custombtn();
            txtadminrfid1 = new customcontrols.customtextbox();
            label1 = new Label();
            label2 = new Label();
            lbladname = new Label();
            lblademail = new Label();
            label4 = new Label();
            txtadminrfid = new TextBox();
            lbladminrfid = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // btnlogout
            // 
            btnlogout.BackColor = SystemColors.Control;
            btnlogout.BackgroundColor = SystemColors.Control;
            btnlogout.BorderColor = Color.PaleVioletRed;
            btnlogout.BorderRadius = 20;
            btnlogout.BorderSize = 0;
            btnlogout.FlatAppearance.BorderSize = 0;
            btnlogout.FlatStyle = FlatStyle.Flat;
            btnlogout.ForeColor = Color.White;
            btnlogout.Image = Properties.Resources.logouticon;
            btnlogout.Location = new Point(195, 516);
            btnlogout.Name = "btnlogout";
            btnlogout.Size = new Size(45, 40);
            btnlogout.TabIndex = 0;
            btnlogout.TextColor = Color.White;
            btnlogout.UseVisualStyleBackColor = false;
            btnlogout.Click += btnlogout_Click;
            // 
            // btnaddrfid
            // 
            btnaddrfid.BackColor = Color.FromArgb(0, 61, 83);
            btnaddrfid.BackgroundColor = Color.FromArgb(0, 61, 83);
            btnaddrfid.BorderColor = Color.PaleVioletRed;
            btnaddrfid.BorderRadius = 20;
            btnaddrfid.BorderSize = 0;
            btnaddrfid.FlatAppearance.BorderSize = 0;
            btnaddrfid.FlatStyle = FlatStyle.Flat;
            btnaddrfid.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnaddrfid.ForeColor = Color.White;
            btnaddrfid.Location = new Point(161, 395);
            btnaddrfid.Name = "btnaddrfid";
            btnaddrfid.Size = new Size(93, 39);
            btnaddrfid.TabIndex = 1;
            btnaddrfid.Text = "Add RFID";
            btnaddrfid.TextColor = Color.White;
            btnaddrfid.UseVisualStyleBackColor = false;
            btnaddrfid.Click += btnaddrfid_Click;
            // 
            // exitbtn
            // 
            exitbtn.BackColor = Color.Transparent;
            exitbtn.BackgroundColor = Color.Transparent;
            exitbtn.BorderColor = Color.Transparent;
            exitbtn.BorderRadius = 20;
            exitbtn.BorderSize = 0;
            exitbtn.FlatAppearance.BorderSize = 0;
            exitbtn.FlatStyle = FlatStyle.Flat;
            exitbtn.ForeColor = Color.White;
            exitbtn.Image = Properties.Resources.exiticon;
            exitbtn.Location = new Point(375, 12);
            exitbtn.Margin = new Padding(4, 3, 4, 3);
            exitbtn.Name = "exitbtn";
            exitbtn.Size = new Size(42, 33);
            exitbtn.TabIndex = 153;
            exitbtn.TextColor = Color.White;
            exitbtn.UseVisualStyleBackColor = false;
            exitbtn.Click += exitbtn_Click;
            // 
            // txtadminrfid1
            // 
            txtadminrfid1.BackColor = SystemColors.Window;
            txtadminrfid1.BorderColor = Color.FromArgb(0, 61, 83);
            txtadminrfid1.BorderFocusColor = Color.HotPink;
            txtadminrfid1.BorderRadius = 0;
            txtadminrfid1.BorderSize = 2;
            txtadminrfid1.Enabled = false;
            txtadminrfid1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtadminrfid1.ForeColor = Color.Black;
            txtadminrfid1.Location = new Point(147, 315);
            txtadminrfid1.Margin = new Padding(4);
            txtadminrfid1.Multiline = true;
            txtadminrfid1.Name = "txtadminrfid1";
            txtadminrfid1.Padding = new Padding(10, 7, 10, 7);
            txtadminrfid1.PasswordChar = false;
            txtadminrfid1.PlaceholderColor = Color.DarkGray;
            txtadminrfid1.PlaceholderText = "";
            txtadminrfid1.Size = new Size(120, 36);
            txtadminrfid1.TabIndex = 2;
            txtadminrfid1.Texts = "";
            txtadminrfid1.UnderlinedStyle = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(60, 315);
            label1.Name = "label1";
            label1.Size = new Size(80, 21);
            label1.TabIndex = 154;
            label1.Text = "RFID Tag:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(51, 114);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 155;
            label2.Text = "Fullname:";
            // 
            // lbladname
            // 
            lbladname.AutoSize = true;
            lbladname.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbladname.Location = new Point(142, 114);
            lbladname.Name = "lbladname";
            lbladname.Size = new Size(85, 21);
            lbladname.TabIndex = 156;
            lbladname.Text = "Fullname:";
            // 
            // lblademail
            // 
            lblademail.AutoSize = true;
            lblademail.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblademail.Location = new Point(142, 153);
            lblademail.Name = "lblademail";
            lblademail.Size = new Size(57, 21);
            lblademail.TabIndex = 158;
            lblademail.Text = "Email:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(79, 153);
            label4.Name = "label4";
            label4.Size = new Size(57, 21);
            label4.TabIndex = 157;
            label4.Text = "Email:";
            // 
            // txtadminrfid
            // 
            txtadminrfid.BorderStyle = BorderStyle.None;
            txtadminrfid.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtadminrfid.Location = new Point(156, 322);
            txtadminrfid.Name = "txtadminrfid";
            txtadminrfid.Size = new Size(100, 22);
            txtadminrfid.TabIndex = 159;
            // 
            // lbladminrfid
            // 
            lbladminrfid.AutoSize = true;
            lbladminrfid.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbladminrfid.Location = new Point(142, 184);
            lbladminrfid.Name = "lbladminrfid";
            lbladminrfid.Size = new Size(49, 21);
            lbladminrfid.TabIndex = 161;
            lbladminrfid.Text = "RFID:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(87, 184);
            label5.Name = "label5";
            label5.Size = new Size(49, 21);
            label5.TabIndex = 160;
            label5.Text = "RFID:";
            // 
            // settingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(430, 568);
            Controls.Add(lbladminrfid);
            Controls.Add(label5);
            Controls.Add(txtadminrfid);
            Controls.Add(lblademail);
            Controls.Add(label4);
            Controls.Add(lbladname);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtadminrfid1);
            Controls.Add(exitbtn);
            Controls.Add(btnaddrfid);
            Controls.Add(btnlogout);
            FormBorderStyle = FormBorderStyle.None;
            Name = "settingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "settingsForm";
            Load += settingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private custombtn btnlogout;
        private custombtn btnaddrfid;
        private custombtn exitbtn;
        private customcontrols.customtextbox txtadminrfid1;
        private Label label1;
        private Label label2;
        private Label lbladname;
        private Label lblademail;
        private Label label4;
        private TextBox txtadminrfid;
        private Label lbladminrfid;
        private Label label5;
    }
}