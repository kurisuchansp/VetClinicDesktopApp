using Google.Cloud.Storage.V1;
using Google.Cloud.Firestore;
using Control = System.Windows.Forms.Control;
namespace VeterinaryClinicApp
{
    public partial class addoffers : UserControl
    {
        private string _bucketName = "veterinaryclinic-60d24.appspot.com"; 
        private StorageClient _storageClient;
        private FirestoreDb firestoreDb;
        private string base64Image;
        public addoffers()
        {
            InitializeComponent();
            InitializeFirestore();
            InitializeStorageClient();
        }
        private void InitializeStorageClient()
        {
            _storageClient = StorageClient.Create();
        }
        public void InitializeFirestore()
        {
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
        }
        private async void btnAddOffer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtoffername.Text) ||
                string.IsNullOrEmpty(txtprice.Text) ||
                string.IsNullOrEmpty(txtdescrip.Text) ||
                pbOfferImage.Image == null)
            {
                MessageBox.Show("Please fill all fields and select an image.");
                return;
            }
            string offerType = rdbtnService.Checked ? "Service" : rdbtnProduct.Checked ? "Product" : null;
            if (string.IsNullOrEmpty(offerType))
            {
                MessageBox.Show("Please select an offer type.");
                return;
            }
            string imageUrl = await UploadImageToStorage(pbOfferImage.ImageLocation);
            Offer offer = new Offer
            {
                OfferName = txtoffername.Text,
                Price = txtprice.Text,
                Description = txtdescrip.Text,
                Image = imageUrl
            };
            await SaveOfferInfoAsync(offer, offerType);
        }
        private async Task SaveOfferInfoAsync(Offer offer, string offerType)
        {
            try
            {
                string documentName = offerType == "Service" ? "Services" : "Products";
                string offerUID = Guid.NewGuid().ToString(); 

                DocumentReference docRef = firestoreDb.Collection("offers").Document(documentName);

                Dictionary<string, object> offerData = new Dictionary<string, object>
                {
                    { "OfferUID", offerUID },
                    { "OfferName", offer.OfferName },
                    { "Price", offer.Price },
                    { "Description", offer.Description },
                    { "Image", offer.Image }
                };
                await docRef.UpdateAsync(new Dictionary<string, object> { { offerUID, offerData } });
                MessageBox.Show($"Offer saved successfully under {documentName}.");
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving offer: {ex.Message}");
            }
        }
        private async Task<string> UploadImageToStorage(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("File path is invalid.");
                return null;
            }

            var file = new FileInfo(filePath);

            if (IsFileLocked(filePath))
            {
                MessageBox.Show("The file is being used by another process. Please close it and try again.");
                return null;
            }
            try
            {
                pbOfferImage.Image?.Dispose();
                pbOfferImage.Image = null; 
                using (var stream = File.Open(file.FullName, FileMode.Open, FileAccess.Read))
                {
                    var objectName = $"images/{file.Name}";
                    var storageObject = await _storageClient.UploadObjectAsync(
                        _bucketName,
                        objectName, 
                        "image/jpeg", 
                        stream);
                    string downloadUrl = $"https://storage.googleapis.com/{_bucketName}/{objectName}";
                    return downloadUrl;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading image: {ex.Message}");
                return null;
            }
        }
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false; 
                }
            }
            catch (IOException)
            {
                return true;
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
        private void pbOfferImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an Image"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pbOfferImage.ImageLocation = openFileDialog.FileName;
                pbOfferImage.Image = Image.FromFile(openFileDialog.FileName);
            }
        }
        private void exitbtn_Click(object sender, EventArgs e)
        {
            ShowOffers();
        }
        private void ShowOffers()
        {
            try
            {
                offersadminuc offersadminuc = new offersadminuc
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill
                };
                this.Controls.Clear();
                this.Controls.Add(offersadminuc);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying Add Offers form: {ex.Message}");
            }
        }
        private void ResetForm()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
                else if (control is RichTextBox richTextBox)
                {
                    richTextBox.Text = string.Empty;
                }
            }
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton radioButton)
                {
                    radioButton.Checked = false;
                }
            }
            pbOfferImage.Image = null;
        }
    }
}
