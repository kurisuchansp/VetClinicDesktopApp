using Google.Cloud.Firestore;
namespace VeterinaryClinicApp
{
    public partial class addpetinfo : UserControl
    {
        private FirestoreDb firestoreDb;
        private string base64Image;
        public string UserID { get; set; }
        private string FullName { get; set; }
        public addpetinfo(string userId, string fullName)
        {
            InitializeComponent();
            InitializeFirestore();
            UserID = userId;
            FullName = fullName;
            lblOwnername.Text = FullName;
        }
        public void InitializeFirestore()
        {
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
        }
        private async Task SavePatientInfoAsync(Patient patient)
        {
            DocumentReference docRef = firestoreDb.Collection("patients").Document();
            await docRef.SetAsync(patient);
            string documentId = docRef.Id;
            Dictionary<string, object> updateData = new Dictionary<string, object>
            {
                { "DocumentID", documentId }
             };
            await docRef.UpdateAsync(updateData);
            MessageBox.Show("Patient info saved successfully.");
        }
        private void pbProfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pbProfile.Image = Image.FromFile(openFileDialog.FileName);

                    base64Image = ImageConverter.ConvertImageToBase64(openFileDialog.FileName);
                }
            }
        }
        public static class ImageConverter
        {
            public static string ConvertImageToBase64(string filePath)
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
                return Convert.ToBase64String(imageBytes);
            }
            public static Image ConvertBase64ToImage(string base64String)
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
        }
        private async void btnAddPatient_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                MessageBox.Show("Please select a profile image for the pet.");
                return;
            }

            Patient patient = new Patient
            {
                userId = UserID,
                OwnerName = FullName,
                PetName = txtpetname.Text,
                AnimalGroup = txtanimalg.Text,
                PetAge = txtpetage.Text,
                PetBreed = txtbreed.Text,
                AdmissionDate = dtpAdmissionDate.Value.ToUniversalTime(),
                ProfileImage = base64Image 
            };
            txtpetname.Text = txtpetage.Text = txtbreed.Text = txtanimalg.Text = "";
            await SavePatientInfoAsync(patient); 
            this.Hide();
        }
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
