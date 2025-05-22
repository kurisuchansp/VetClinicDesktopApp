namespace VeterinaryClinicApp
{
    partial class addoffers
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
            label3 = new Label();
            txtdescrip = new TextBox();
            customtextbox3 = new customcontrols.customtextbox();
            exitbtn = new custombtn();
            label5 = new Label();
            pbOfferImage = new PictureBox();
            btnAddOffer = new custombtn();
            label2 = new Label();
            txtprice = new TextBox();
            customtextbox2 = new customcontrols.customtextbox();
            label1 = new Label();
            label10 = new Label();
            txtoffername = new TextBox();
            customtextbox6 = new customcontrols.customtextbox();
            rdbtnService = new RadioButton();
            rdbtnProduct = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)pbOfferImage).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(0, 61, 83);
            label3.Location = new Point(422, 331);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(90, 16);
            label3.TabIndex = 171;
            label3.Text = "Description:";
            // 
            // txtdescrip
            // 
            txtdescrip.BackColor = SystemColors.Control;
            txtdescrip.BorderStyle = BorderStyle.None;
            txtdescrip.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            txtdescrip.ForeColor = Color.FromArgb(0, 61, 83);
            txtdescrip.Location = new Point(422, 356);
            txtdescrip.Margin = new Padding(4, 3, 4, 3);
            txtdescrip.Multiline = true;
            txtdescrip.Name = "txtdescrip";
            txtdescrip.Size = new Size(253, 120);
            txtdescrip.TabIndex = 169;
            // 
            // customtextbox3
            // 
            customtextbox3.BackColor = SystemColors.Control;
            customtextbox3.BorderColor = Color.FromArgb(0, 61, 83);
            customtextbox3.BorderFocusColor = Color.HotPink;
            customtextbox3.BorderRadius = 10;
            customtextbox3.BorderSize = 2;
            customtextbox3.Enabled = false;
            customtextbox3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customtextbox3.ForeColor = Color.Black;
            customtextbox3.Location = new Point(422, 349);
            customtextbox3.Margin = new Padding(5);
            customtextbox3.Multiline = true;
            customtextbox3.Name = "customtextbox3";
            customtextbox3.Padding = new Padding(12, 8, 12, 8);
            customtextbox3.PasswordChar = false;
            customtextbox3.PlaceholderColor = Color.DarkGray;
            customtextbox3.PlaceholderText = "";
            customtextbox3.Size = new Size(266, 135);
            customtextbox3.TabIndex = 170;
            customtextbox3.Texts = "";
            customtextbox3.UnderlinedStyle = true;
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
            exitbtn.Location = new Point(982, 14);
            exitbtn.Margin = new Padding(4, 3, 4, 3);
            exitbtn.Name = "exitbtn";
            exitbtn.Size = new Size(42, 33);
            exitbtn.TabIndex = 168;
            exitbtn.TextColor = Color.White;
            exitbtn.UseVisualStyleBackColor = false;
            exitbtn.Click += exitbtn_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(0, 61, 83);
            label5.Location = new Point(133, 45);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(105, 16);
            label5.TabIndex = 167;
            label5.Text = "Upload photo:";
            // 
            // pbOfferImage
            // 
            pbOfferImage.BackColor = Color.White;
            pbOfferImage.BackgroundImageLayout = ImageLayout.Stretch;
            pbOfferImage.Image = Properties.Resources.uploadimgicon;
            pbOfferImage.InitialImage = null;
            pbOfferImage.Location = new Point(136, 78);
            pbOfferImage.Margin = new Padding(4, 3, 4, 3);
            pbOfferImage.Name = "pbOfferImage";
            pbOfferImage.Size = new Size(209, 165);
            pbOfferImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbOfferImage.TabIndex = 166;
            pbOfferImage.TabStop = false;
            pbOfferImage.Click += pbOfferImage_Click;
            // 
            // btnAddOffer
            // 
            btnAddOffer.BackColor = Color.FromArgb(0, 61, 83);
            btnAddOffer.BackgroundColor = Color.FromArgb(0, 61, 83);
            btnAddOffer.BorderColor = SystemColors.ActiveCaptionText;
            btnAddOffer.BorderRadius = 20;
            btnAddOffer.BorderSize = 3;
            btnAddOffer.FlatAppearance.BorderSize = 0;
            btnAddOffer.FlatStyle = FlatStyle.Flat;
            btnAddOffer.ForeColor = Color.White;
            btnAddOffer.Image = Properties.Resources.submiticonb;
            btnAddOffer.Location = new Point(520, 492);
            btnAddOffer.Margin = new Padding(4, 3, 4, 3);
            btnAddOffer.Name = "btnAddOffer";
            btnAddOffer.Padding = new Padding(1, 0, 0, 0);
            btnAddOffer.Size = new Size(61, 59);
            btnAddOffer.TabIndex = 165;
            btnAddOffer.TextColor = Color.White;
            btnAddOffer.UseVisualStyleBackColor = false;
            btnAddOffer.Click += btnAddOffer_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 61, 83);
            label2.Location = new Point(422, 249);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(47, 16);
            label2.TabIndex = 164;
            label2.Text = "Price:";
            // 
            // txtprice
            // 
            txtprice.BackColor = SystemColors.Control;
            txtprice.BorderStyle = BorderStyle.None;
            txtprice.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            txtprice.ForeColor = Color.FromArgb(0, 61, 83);
            txtprice.Location = new Point(426, 277);
            txtprice.Margin = new Padding(4, 3, 4, 3);
            txtprice.Name = "txtprice";
            txtprice.Size = new Size(253, 17);
            txtprice.TabIndex = 162;
            // 
            // customtextbox2
            // 
            customtextbox2.BackColor = SystemColors.Control;
            customtextbox2.BorderColor = Color.FromArgb(0, 61, 83);
            customtextbox2.BorderFocusColor = Color.HotPink;
            customtextbox2.BorderRadius = 10;
            customtextbox2.BorderSize = 2;
            customtextbox2.Enabled = false;
            customtextbox2.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customtextbox2.ForeColor = Color.Black;
            customtextbox2.Location = new Point(421, 268);
            customtextbox2.Margin = new Padding(5);
            customtextbox2.Multiline = true;
            customtextbox2.Name = "customtextbox2";
            customtextbox2.Padding = new Padding(12, 8, 12, 8);
            customtextbox2.PasswordChar = false;
            customtextbox2.PlaceholderColor = Color.DarkGray;
            customtextbox2.PlaceholderText = "";
            customtextbox2.Size = new Size(266, 35);
            customtextbox2.TabIndex = 163;
            customtextbox2.Texts = "";
            customtextbox2.UnderlinedStyle = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(0, 61, 83);
            label1.Location = new Point(422, 165);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(84, 16);
            label1.TabIndex = 161;
            label1.Text = "Offer Type:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(0, 61, 83);
            label10.Location = new Point(422, 78);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(89, 16);
            label10.TabIndex = 158;
            label10.Text = "Offer Name:";
            // 
            // txtoffername
            // 
            txtoffername.BackColor = SystemColors.Control;
            txtoffername.BorderStyle = BorderStyle.None;
            txtoffername.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            txtoffername.ForeColor = Color.FromArgb(0, 61, 83);
            txtoffername.Location = new Point(425, 107);
            txtoffername.Margin = new Padding(4, 3, 4, 3);
            txtoffername.Name = "txtoffername";
            txtoffername.Size = new Size(253, 17);
            txtoffername.TabIndex = 156;
            // 
            // customtextbox6
            // 
            customtextbox6.BackColor = SystemColors.Control;
            customtextbox6.BorderColor = Color.FromArgb(0, 61, 83);
            customtextbox6.BorderFocusColor = Color.HotPink;
            customtextbox6.BorderRadius = 10;
            customtextbox6.BorderSize = 2;
            customtextbox6.Enabled = false;
            customtextbox6.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            customtextbox6.ForeColor = Color.Black;
            customtextbox6.Location = new Point(421, 97);
            customtextbox6.Margin = new Padding(5);
            customtextbox6.Multiline = true;
            customtextbox6.Name = "customtextbox6";
            customtextbox6.Padding = new Padding(12, 8, 12, 8);
            customtextbox6.PasswordChar = false;
            customtextbox6.PlaceholderColor = Color.DarkGray;
            customtextbox6.PlaceholderText = "";
            customtextbox6.Size = new Size(266, 35);
            customtextbox6.TabIndex = 157;
            customtextbox6.Texts = "";
            customtextbox6.UnderlinedStyle = true;
            // 
            // rdbtnService
            // 
            rdbtnService.AutoSize = true;
            rdbtnService.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rdbtnService.ForeColor = Color.FromArgb(0, 61, 83);
            rdbtnService.Location = new Point(519, 165);
            rdbtnService.Name = "rdbtnService";
            rdbtnService.Size = new Size(67, 19);
            rdbtnService.TabIndex = 172;
            rdbtnService.TabStop = true;
            rdbtnService.Text = "Service";
            rdbtnService.UseVisualStyleBackColor = true;
            // 
            // rdbtnProduct
            // 
            rdbtnProduct.AutoSize = true;
            rdbtnProduct.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rdbtnProduct.ForeColor = Color.FromArgb(0, 61, 83);
            rdbtnProduct.Location = new Point(592, 165);
            rdbtnProduct.Name = "rdbtnProduct";
            rdbtnProduct.Size = new Size(69, 19);
            rdbtnProduct.TabIndex = 173;
            rdbtnProduct.TabStop = true;
            rdbtnProduct.Text = "Product";
            rdbtnProduct.UseVisualStyleBackColor = true;
            // 
            // addoffers
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbOfferImage);
            Controls.Add(rdbtnProduct);
            Controls.Add(rdbtnService);
            Controls.Add(label3);
            Controls.Add(txtdescrip);
            Controls.Add(customtextbox3);
            Controls.Add(exitbtn);
            Controls.Add(label5);
            Controls.Add(btnAddOffer);
            Controls.Add(label2);
            Controls.Add(txtprice);
            Controls.Add(customtextbox2);
            Controls.Add(label1);
            Controls.Add(label10);
            Controls.Add(txtoffername);
            Controls.Add(customtextbox6);
            Cursor = Cursors.Hand;
            Name = "addoffers";
            Size = new Size(1040, 566);
            ((System.ComponentModel.ISupportInitialize)pbOfferImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label3;
        private TextBox txtdescrip;
        private customcontrols.customtextbox customtextbox3;
        private custombtn exitbtn;
        private Label label5;
        private PictureBox pbOfferImage;
        private custombtn btnAddOffer;
        private Label label2;
        private TextBox txtprice;
        private customcontrols.customtextbox customtextbox2;
        private Label label1;
        private TextBox txtage;
        private customcontrols.customtextbox customtextbox1;
        private Label label10;
        private TextBox txtoffername;
        private customcontrols.customtextbox customtextbox6;
        private RadioButton rdbtnService;
        private RadioButton rdbtnProduct;
    }
}
