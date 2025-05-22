namespace VeterinaryClinicApp
{
    partial class offersadminuc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(offersadminuc));
            btnaddoffer = new custombtn();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnaddoffer
            // 
            btnaddoffer.BackColor = Color.FromArgb(0, 61, 83);
            btnaddoffer.BackgroundColor = Color.FromArgb(0, 61, 83);
            btnaddoffer.BorderColor = Color.PaleVioletRed;
            btnaddoffer.BorderRadius = 20;
            btnaddoffer.BorderSize = 0;
            btnaddoffer.FlatAppearance.BorderSize = 0;
            btnaddoffer.FlatStyle = FlatStyle.Flat;
            btnaddoffer.ForeColor = Color.White;
            btnaddoffer.Image = (Image)resources.GetObject("btnaddoffer.Image");
            btnaddoffer.Location = new Point(968, 508);
            btnaddoffer.Name = "btnaddoffer";
            btnaddoffer.Size = new Size(57, 46);
            btnaddoffer.TabIndex = 0;
            btnaddoffer.TextColor = Color.White;
            btnaddoffer.UseVisualStyleBackColor = false;
            btnaddoffer.Click += btnaddoffer_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(192, 0);
            label1.Name = "label1";
            label1.Size = new Size(73, 21);
            label1.TabIndex = 1;
            label1.Text = "Services";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(744, 2);
            label2.Name = "label2";
            label2.Size = new Size(77, 21);
            label2.TabIndex = 2;
            label2.Text = "Products";
            // 
            // offersadminuc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnaddoffer);
            Margin = new Padding(4, 3, 4, 3);
            Name = "offersadminuc";
            Size = new Size(1040, 566);
            Load += offersadminuc_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private custombtn btnaddoffer;
        private Label label1;
        private Label label2;
    }
}
