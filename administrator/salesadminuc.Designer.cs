namespace VeterinaryClinicApp.administrator
{
    partial class salesadminuc
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
            cmbMonths = new ComboBox();
            dgvSales = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvSales).BeginInit();
            SuspendLayout();
            // 
            // cmbMonths
            // 
            cmbMonths.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbMonths.FormattingEnabled = true;
            cmbMonths.Location = new Point(658, 125);
            cmbMonths.Name = "cmbMonths";
            cmbMonths.Size = new Size(264, 23);
            cmbMonths.TabIndex = 0;
            cmbMonths.Text = "Select Month";
            cmbMonths.SelectedIndexChanged += cmbMonths_SelectedIndexChanged;
            // 
            // dgvSales
            // 
            dgvSales.AllowUserToAddRows = false;
            dgvSales.AllowUserToDeleteRows = false;
            dgvSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSales.Location = new Point(152, 154);
            dgvSales.Name = "dgvSales";
            dgvSales.ReadOnly = true;
            dgvSales.Size = new Size(770, 345);
            dgvSales.TabIndex = 1;
            // 
            // salesadminuc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(dgvSales);
            Controls.Add(cmbMonths);
            Name = "salesadminuc";
            Size = new Size(1046, 566);
            Load += salesadminuc_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSales).EndInit();
            ResumeLayout(false);
        }
        #endregion
        private ComboBox cmbMonths;
        private DataGridView dgvSales;
    }
}
