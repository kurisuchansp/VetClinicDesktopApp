namespace VeterinaryClinicApp
{
    partial class staffsadminuc
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
            btnaddstaff = new custombtn();
            SuspendLayout();
            // 
            // btnaddstaff
            // 
            btnaddstaff.BackColor = Color.FromArgb(0, 61, 83);
            btnaddstaff.BackgroundColor = Color.FromArgb(0, 61, 83);
            btnaddstaff.BackgroundImageLayout = ImageLayout.Stretch;
            btnaddstaff.BorderColor = Color.PaleVioletRed;
            btnaddstaff.BorderRadius = 20;
            btnaddstaff.BorderSize = 0;
            btnaddstaff.FlatAppearance.BorderSize = 0;
            btnaddstaff.FlatStyle = FlatStyle.Flat;
            btnaddstaff.ForeColor = Color.White;
            btnaddstaff.Image = Properties.Resources.addstafficon;
            btnaddstaff.Location = new Point(508, 504);
            btnaddstaff.Margin = new Padding(4, 3, 4, 3);
            btnaddstaff.Name = "btnaddstaff";
            btnaddstaff.Size = new Size(57, 46);
            btnaddstaff.TabIndex = 0;
            btnaddstaff.TextColor = Color.White;
            btnaddstaff.UseVisualStyleBackColor = false;
            btnaddstaff.Click += btnaddstaff_Click;
            // 
            // staffsadminuc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btnaddstaff);
            Margin = new Padding(4, 3, 4, 3);
            Name = "staffsadminuc";
            Size = new Size(1046, 566);
            Load += staffsadminuc_Load;
            ResumeLayout(false);
        }

        #endregion

        private custombtn btnaddstaff;
    }
}
