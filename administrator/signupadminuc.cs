using System.Security.Cryptography;
using System.Text;
using FirebaseAdmin;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
namespace VeterinaryClinicApp
{
    public partial class signupadminuc : UserControl
    {
        private FirestoreDb firestoreDb;
        private mainform mainform;
        private adminformuc adminformuc;
        public signupadminuc(mainform parentForm, adminformuc adminformuc)
        {
            InitializeComponent();
            InitializeFirestore();
            InitializeFirebaseAuth();
            this.mainform = parentForm;
            this.adminformuc = adminformuc;
            txtemailsuad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            txtpasssuad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            txtcpasssuad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            txtpasssuad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            txtaddsuad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            txtbaddsuad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
            txtphonesuad.KeyDown += new KeyEventHandler(InputControl_KeyDown);
        }
        private void InputControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Prevents the "ding" sound
                signUpbtnad_Click(sender, e); // Trigger the sign-in button click event
            }
        }
        private void InitializeFirestore()
        {
            string projectId = "veterinaryclinic-60d24";
            firestoreDb = FirestoreDb.Create(projectId);
        }
        private void InitializeFirebaseAuth()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(@"D:\capstone2\codings\VeterinaryClinicApp\veterinaryclinic-60d24-firebase-adminsdk-ptgn7-f1f61ef6e8.json")
                });
            }
        }
        private async void signUpbtnad_Click(object sender, EventArgs e)
        {
            string fullname = txtfnamesuad.Text;
            string email = txtemailsuad.Text;
            string phoneNumber = txtphonesuad.Text;
            string branchLocation = txtbaddsuad.Text;
            string address = txtaddsuad.Text;
            string password = txtpasssuad.Text;
            string confirmPassword = txtcpasssuad.Text;
            string rfid = txtadminrfid.Text;
            if (string.IsNullOrWhiteSpace(fullname) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(branchLocation) ||
                string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(rfid))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Password and Confirm Password do not match.");
                return;
            }
            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.");
                return;
            }
            if (!IsValidRFID(rfid))
            {
                MessageBox.Show("Invalid RFID. It must be exactly 10 digits.");
                return;
            }
            try
            {
                bool isRfidRegistered = await IsRfidRegisteredAsync(rfid);
                if (isRfidRegistered)
                {
                    MessageBox.Show("This RFID is already registered. Please use a different RFID.");
                    return;
                }
                string firebaseUid = await CreateFirebaseUserAsync(email, password);
                DocumentReference docRef = firestoreDb.Collection("admins").Document(firebaseUid);
                var adminData = new
                {
                    Fullname = fullname,
                    Email = email,
                    Branch = branchLocation,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    RFID = rfid
                };
                await docRef.SetAsync(adminData);
                txtfnamesuad.Text = txtemailsuad.Text = txtphonesuad.Text = txtbaddsuad.Text = txtaddsuad.Text = txtpasssuad.Text = txtcpasssuad.Text = txtadminrfid.Text = "";
                MessageBox.Show("Admin created successfully! You can now Sign In.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating user: " + ex.Message);
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var emailCheck = new System.Net.Mail.MailAddress(email);
                return emailCheck.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    result.Append(bytes[i].ToString("x2"));
                }
                return result.ToString();
            }
        }
        private void linksiad_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            adminformuc.ShowSignInAdmin();
        }
        private async Task<string> CreateFirebaseUserAsync(string email, string password)
        {
            string apiKey = "AIzaSyAqd2kAiAN8t_BSE9y_GbOFy5NWteOMaH0";
            string url = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}";
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
                    return jsonResult.localId;
                }
                else
                {
                    string errorResult = await response.Content.ReadAsStringAsync();
                    dynamic error = JsonConvert.DeserializeObject(errorResult);
                    throw new Exception($"Error creating user: {error.error.message}");
                }
            }
        }
        private bool IsValidRFID(string rfid)
        {
            return rfid.Length == 10 && rfid.All(char.IsDigit);
        }
        private async Task<bool> IsRfidRegisteredAsync(string rfid)
        {
            Query adminsQuery = firestoreDb.Collection("admins").WhereEqualTo("RFID", rfid);
            QuerySnapshot querySnapshot = await adminsQuery.GetSnapshotAsync();
            return querySnapshot.Count > 0; 
        }
    }
}