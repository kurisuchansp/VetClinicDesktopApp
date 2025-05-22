using System.IO.Ports;
using System.Security.Cryptography;
using System.Text;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
namespace VeterinaryClinicApp
{
    public partial class signinadminuc : UserControl
    {
        private SerialPort rfidSerialPort;
        private mainform mainform;
        private adminformuc adminformuc;
        private FirestoreDb firestoreDb;
        private Label lblRfidTag;
        private TextBox txtAdminRfid;
        private Button btnRfidLogin;
        private const string ApiKey = "AIzaSyAqd2kAiAN8t_BSE9y_GbOFy5NWteOMaH0";
        public signinadminuc(mainform parentForm, adminformuc adminformuc)
        {
            InitializeComponent();
            InitializeFirestore();
            InitializeRFIDReader();
            this.mainform = parentForm;
            this.adminformuc = adminformuc;
            txtemailsiad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            txtpasssiad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            InitializeRfidLoginControls();
        }
        private void InputControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                signInbtnad_Click(sender, e); 
            }
        }
        public void InitializeFirestore()
        {
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
            Console.WriteLine("Connected to Firestore");
        }
        private void InitializeRFIDReader()
        {
            try
            {
                RFIDReader rfidReader = new RFIDReader("COM1"); 
                rfidReader.OnRFIDScanned += RFIDReader_OnRFIDScanned;
                rfidReader.StartReading(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing RFID reader: " + ex.Message);
            }
        }
        private void InitializeRfidLoginControls()
        {
            lblRfidTag = new Label
            {
                Text = "RFID Tag:",
                Visible = false, 
                Location = new Point(120, 440), 
                Size = new Size(80, 30)
            };
            this.Controls.Add(lblRfidTag);
            txtAdminRfid = new TextBox
            {
                Visible = false, 
                Location = new Point(212, 440), 
                Size = new Size(175, 46)
            };
            this.Controls.Add(txtAdminRfid);
            btnRfidLogin = new Button
            {
                Text = "RFID Login",
                Location = new Point(212, 467),
                Size = new Size(175, 46),
                BackColor = Color.Green,
            };
            btnRfidLogin.Click += BtnRfidLogin_Click; 
            this.Controls.Add(btnRfidLogin); 
        }
        private bool isRfidLoginMode = false;
        private void BtnRfidLogin_Click(object sender, EventArgs e)
        {
            if (!isRfidLoginMode)
            {
                lblRfidTag.Visible = true;
                txtAdminRfid.Visible = true;
                txtemailsiad.Visible = false;
                txtpasssiad.Visible = false;
                signInbtnad.Visible = true; 
                btnRfidLogin.Text = "Login"; 
                isRfidLoginMode = true;
            }
            else
            {
                PerformRfidLogin();
            }
        }
        private async void PerformRfidLogin()
        {
            string rfidTag = txtAdminRfid.Text.Trim();
            if (string.IsNullOrWhiteSpace(rfidTag))
            {
                MessageBox.Show("Please scan a valid RFID tag.");
                return;
            }
            try
            {
                Query query = firestoreDb.Collection("admins").WhereEqualTo("RFIDTag", rfidTag);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                if (snapshot.Count == 0)
                {
                    MessageBox.Show("Invalid RFID tag.");
                    return;
                }
                DocumentSnapshot document = snapshot.Documents[0];
                string fullname = document.GetValue<string>("Fullname");
                string email = document.GetValue<string>("Email");
                string rfid = document.GetValue<string>("RFIDTag");
                LoggedInAdmin.Fullname = fullname;
                LoggedInAdmin.Email = email;
                LoggedInAdmin.RFIDTag = rfid;
                mainform.ShowAdminDashboard(fullname, email, rfid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during RFID login: " + ex.Message);
            }
        }
        private void linksuad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminformuc.ShowSignUpAdmin();
        }
        private async void signInbtnad_Click(object sender, EventArgs e)
        {
            string email = txtemailsiad.Text;
            string password = txtpasssiad.Text;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in both email and password fields.");
                return;
            }
            try
            {
                bool isAuthenticated = await AuthenticateUserAsync(email, password);
                if (!isAuthenticated)
                {
                    MessageBox.Show("Incorrect email or password.");
                    return;
                }
                var firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
                Query query = firestoreDb.Collection("admins").WhereEqualTo("Email", email);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                if (snapshot.Count == 0)
                {
                    MessageBox.Show("Account not found in Firestore.");
                    return;
                }
                DocumentSnapshot document = snapshot.Documents[0];
                string fullname = document.GetValue<string>("Fullname");
                string rfid = document.GetValue<string>("RFIDTag");
                LoggedInAdmin.Fullname = fullname;
                LoggedInAdmin.Email = email;
                LoggedInAdmin.RFIDTag = rfid;
                mainform.ShowAdminDashboard(fullname, email, rfid);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error logging in: " + ex.Message);
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            string apiKey = "AIzaSyAqd2kAiAN8t_BSE9y_GbOFy5NWteOMaH0";
            string url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";
            var payload = new
            {
                email = email,
                password = password,
                returnSecureToken = true
            };
            using (HttpClient client = new HttpClient())
            {
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    dynamic jsonResult = JsonConvert.DeserializeObject(result);
                    return true;
                }
                else
                {
                    MessageBox.Show("Incorrect email or password.");
                    return false;
                }
            }
        }
        private void RFIDReader_OnRFIDScanned(string rfidTag)
        {
            if (isRfidLoginMode)
            {
                txtAdminRfid.Text = rfidTag;
            }
        }
    }
}
