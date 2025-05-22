namespace VeterinaryClinicApp
{
    partial class adminformuc
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
            panel1 = new Panel();
            panelLogoadm = new Panel();
            lblClin = new Label();
            pbLogo = new PictureBox();
            lblVet = new Label();
            lblWelcome = new Label();
            panelLogoadm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Location = new Point(0, -2);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(628, 683);
            panel1.TabIndex = 18;
            // 
            // panelLogoadm
            // 
            panelLogoadm.BackgroundImageLayout = ImageLayout.Stretch;
            panelLogoadm.Controls.Add(lblClin);
            panelLogoadm.Controls.Add(pbLogo);
            panelLogoadm.Controls.Add(lblVet);
            panelLogoadm.Location = new Point(864, 53);
            panelLogoadm.Margin = new Padding(4, 3, 4, 3);
            panelLogoadm.Name = "panelLogoadm";
            panelLogoadm.Size = new Size(215, 202);
            panelLogoadm.TabIndex = 16;
            // 
            // lblClin
            // 
            lblClin.AutoSize = true;
            lblClin.Font = new Font("Sans Serif Collection", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClin.ForeColor = SystemColors.ControlLight;
            lblClin.Location = new Point(66, 31);
            lblClin.Margin = new Padding(4, 0, 4, 0);
            lblClin.Name = "lblClin";
            lblClin.Size = new Size(75, 27);
            lblClin.TabIndex = 9;
            lblClin.Text = "CLINIC";
            lblClin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pbLogo
            // 
            pbLogo.Image = Properties.Resources.logo1;
            pbLogo.Location = new Point(0, 55);
            pbLogo.Margin = new Padding(4, 3, 4, 3);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(215, 147);
            pbLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbLogo.TabIndex = 1;
            pbLogo.TabStop = false;
            // 
            // lblVet
            // 
            lblVet.AutoSize = true;
            lblVet.Font = new Font("Sans Serif Collection", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVet.ForeColor = SystemColors.ControlLight;
            lblVet.Location = new Point(28, 0);
            lblVet.Margin = new Padding(4, 0, 4, 0);
            lblVet.Name = "lblVet";
            lblVet.Size = new Size(137, 27);
            lblVet.TabIndex = 0;
            lblVet.Text = "VETERINARY";
            lblVet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.Font = new Font("Sans Serif Collection", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWelcome.ForeColor = SystemColors.ControlLight;
            lblWelcome.Location = new Point(713, 286);
            lblWelcome.Margin = new Padding(4, 0, 4, 0);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(507, 104);
            lblWelcome.TabIndex = 22;
            lblWelcome.Text = "Please fill in all the necessary \r\ninformation";
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // adminformuc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 61, 83);
            Controls.Add(lblWelcome);
            Controls.Add(panel1);
            Controls.Add(panelLogoadm);
            Margin = new Padding(4, 3, 4, 3);
            Name = "adminformuc";
            Size = new Size(1264, 681);
            panelLogoadm.ResumeLayout(false);
            panelLogoadm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelLogoadm;
        private System.Windows.Forms.Label lblClin;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblVet;
        private System.Windows.Forms.Label lblWelcome;
    }
}
