using Google.Cloud.Firestore;
namespace VeterinaryClinicApp.addcontrols
{
    public partial class addstaffs : UserControl
    {
        private FirestoreDb firestoreDb;
        private string base64Image;
        public addstaffs()
        {
            InitializeComponent();
            InitializeFirestore();
        }
        public void InitializeFirestore()
        {
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
        }
        private async void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                MessageBox.Show("Please select a profile image for the pet.");
                return;
            }
            Staff staff = new Staff
            {
                staffname = txtproname.Text,
                staffage = txtage.Text,
                gender = txtgender.Text,
                Role = txtRole.Text,
                pbProfile = base64Image
            };
            await SaveStaffInfoAsync(staff);
            this.Hide();
        }
        private async Task SaveStaffInfoAsync(Staff staff)
        {
            DocumentReference docRef = firestoreDb.Collection("staffs").Document();
            await docRef.SetAsync(staff);
            string documentId = docRef.Id;
            Dictionary<string, object> updateData = new Dictionary<string, object>
            {
                { "DocumentID", documentId }
             };
            await docRef.UpdateAsync(updateData);
            MessageBox.Show("Staff info saved successfully.");
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
        private void pbProfile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pbProfile.Image = Image.FromFile(openFileDialog.FileName);

                    base64Image = ImageConverter.ConvertImageToBase64(openFileDialog.FileName);
                }
            }
        }
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
