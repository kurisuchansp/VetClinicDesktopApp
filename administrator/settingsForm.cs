using Google.Cloud.Firestore;
namespace VeterinaryClinicApp
{
    public partial class settingsForm : Form
    {
        private string adminEmail;
        private RFIDReader rfidReader;
        public settingsForm()
        {
            InitializeComponent();
            string fullname = LoggedInAdmin.Fullname;
            string email = LoggedInAdmin.Email;
            string rfid = LoggedInAdmin.RFIDTag;
            lbladname.Text = $"{fullname}";
            lblademail.Text = $"{email}";
            lbladminrfid.Text = $"{rfid}";
            adminEmail = $"{email}";
            try
            {
                rfidReader = new RFIDReader("COM1");
                rfidReader.OnRFIDScanned += RfidReader_OnRFIDScanned;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing RFID reader: {ex.Message}");
            }
        }
        private void settingsForm_Load(object sender, EventArgs e)
        {
            try
            {
                rfidReader.StartReading();
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"COM port not found: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Access denied to COM port: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred while starting RFID reader: {ex.Message}");
            }
        }
        private void RfidReader_OnRFIDScanned(string rfidTag)
        {
            Invoke(new Action(() =>
            {
                txtadminrfid.Text = rfidTag;
            }));
        }
        private async void btnaddrfid_Click(object sender, EventArgs e)
        {
            try
            {
                string rfidtag = txtadminrfid.Text.Trim();
                if (rfidtag.Length != 10 || !rfidtag.All(char.IsDigit))
                {
                    MessageBox.Show("Please enter a valid 10-digit RFID tag (only numbers).");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(rfidtag))
                {
                    txtadminrfid.ReadOnly = true;
                }
                string email = lblademail.Text;
                var firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
                Query query = firestoreDb.Collection("admins").WhereEqualTo("Email", email);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();
                if (snapshot.Count == 0)
                {
                    MessageBox.Show("Admin record not found.");
                    return;
                }
                DocumentReference docRef = snapshot.Documents[0].Reference;
                await docRef.UpdateAsync(new Dictionary<string, object>
                    {
                        { "RFIDTag", rfidtag }
                    });
                MessageBox.Show("RFID Tag successfully added.");
                txtadminrfid.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void settingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                rfidReader.StopReading();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping RFID reader: {ex.Message}");
            }
        }
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnlogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
        }
    }
}
