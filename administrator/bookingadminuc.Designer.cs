namespace VeterinaryClinicApp
{
    partial class bookingadminuc
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblcurrentdate = new Label();
            cmbdateselection = new ComboBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblcurrentdate
            // 
            lblcurrentdate.AutoSize = true;
            lblcurrentdate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblcurrentdate.Location = new Point(377, 18);
            lblcurrentdate.Name = "lblcurrentdate";
            lblcurrentdate.Size = new Size(96, 21);
            lblcurrentdate.TabIndex = 1;
            lblcurrentdate.Text = "Date Today";
            // 
            // cmbdateselection
            // 
            cmbdateselection.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbdateselection.FormattingEnabled = true;
            cmbdateselection.Location = new Point(792, 15);
            cmbdateselection.Name = "cmbdateselection";
            cmbdateselection.Size = new Size(222, 29);
            cmbdateselection.TabIndex = 2;
            cmbdateselection.Text = "Sort the Bookings by Date";
            cmbdateselection.SelectedIndexChanged += cmbdateselection_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(275, 18);
            label1.Name = "label1";
            label1.Size = new Size(100, 21);
            label1.TabIndex = 3;
            label1.Text = "Date Today:";
            // 
            // bookingadminuc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(label1);
            Controls.Add(cmbdateselection);
            Controls.Add(lblcurrentdate);
            Margin = new Padding(4, 3, 4, 3);
            Name = "bookingadminuc";
            Size = new Size(1046, 566);
            Load += bookingadminuc_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblcurrentdate;
        private ComboBox cmbdateselection;
        private Label label1;
    }
}
