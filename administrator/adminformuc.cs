namespace VeterinaryClinicApp
{
    public partial class adminformuc : UserControl
    {
        private mainform mainform;
        public adminformuc(mainform parentForm)
        {
            InitializeComponent();
            this.mainform = parentForm;
            ShowSignUpAdmin();
        }
        public void ShowSignUpAdmin()
        {
            panel1.Controls.Clear(); 
            signupadminuc signupadminuc = new signupadminuc(mainform, this); 
            panel1.Controls.Add(signupadminuc);
            signupadminuc.Show(); 
        }
        public void ShowSignInAdmin()
        {
            panel1.Controls.Clear(); 
            signinadminuc signinadminuc = new signinadminuc(mainform, this); 
            signinadminuc.Dock = DockStyle.Fill; 
            panel1.Controls.Add(signinadminuc); 
            signinadminuc.Show();
        }
    }
}
