using Google.Cloud.Firestore;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Firebase.Storage;
using System.IO.Ports;

namespace VeterinaryClinicApp.administrator
{
    public partial class patientsinfouc : UserControl
    {
        private Panel scrollablePanel;
        private FirestoreDb firestoreDb;
        public patientsinfouc()
        {
            InitializeComponent();
            InitializeFirestore();
        }
        public void InitializeFirestore()
        {
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
            Console.WriteLine("Connected to Firestore");
        }
        private async void patientsinfouc_Load(object sender, EventArgs e)
        {
            InitializeScrollablePanel(); 
            List<Patient> patients = await GetPatientsDataAsync();
            DisplayPatientsData(patients);
        }
        private void InitializeScrollablePanel()
        {
            scrollablePanel = new Panel
            {
                Size = new Size(1040, 500), 
                AutoScroll = true 
            };
            this.Controls.Add(scrollablePanel);
        }

       

        private async Task<List<Patient>> GetPatientsDataAsync()
        {
            var patientsList = new List<Patient>();
            try
            {
                QuerySnapshot snapshot = await firestoreDb.Collection("patients").GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        try
                        {
                            Patient patient = document.ConvertTo<Patient>();
                            patient.DocumentID = document.Id;

                            if (patient != null)
                            {
                                patientsList.Add(patient);
                            }
                            else
                            {
                                Console.WriteLine("Warning: A document was found but could not be converted to a Patient object.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error while converting document ID {document.Id}: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching patients data: {ex.Message}");
            }
            return patientsList;
        }
        private void DisplayPatientsData(List<Patient> patients)
        {
            int columns = 2; // Number of columns
            int panelWidth = 500;
            int panelHeight = 300;
            int xOffset = 10;
            int yOffset = 10;
            int spacing = 10;
            scrollablePanel.Controls.Clear();
            scrollablePanel.BackColor = Color.White;
            scrollablePanel.Dock = DockStyle.Fill;
            for (int i = 0; i < patients.Count; i++)
            {
                var patient = patients[i];
                int row = i / columns;
                int column = i % columns;
                Panel patientPanel = new Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    Location = new Point(xOffset + (column * (panelWidth + spacing)), yOffset + (row * (panelHeight + spacing))),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray,
                };
                PictureBox pictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(80, 80),
                    Location = new Point(10, 10),
                };
                if (!string.IsNullOrEmpty(patient.ProfileImage))
                {
                    pictureBox.Image = ConvertBase64ToImage(patient.ProfileImage);
                }
                patientPanel.Controls.Add(pictureBox);
                PictureBox findingsBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(150, 150), // Match picture size from DisplayPatientsData
                    Location = new Point(100, 145),
                    Cursor = Cursors.Hand,
                    BackColor = Color.White
                };
                if (!string.IsNullOrEmpty(patient.Findings))
                {
                    try
                    {
                        findingsBox.Image = ConvertBase64ToImage(patient.Findings); // Convert from base64 to Image
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("Invalid image format in Findings field.");
                    }
                }
                else
                {
                    
                }
                patientPanel.Controls.Add(findingsBox);
                Label patientInfoLabel = new Label
                {
                    Text = $"Owner Name: {patient.OwnerName}\n" +
                           $"Pet Name: {patient.PetName}\n" +
                           $"Pet Age: {patient.PetAge} years old\n" +
                           $"Pet Breed: {patient.PetBreed}\n" +
                           $"Animal Group: {patient.AnimalGroup}\n" +
                           $"Admission Date: {patient.AdmissionDate}\n" +
                           $"Findings: \n",
                    Location = new Point(100, 10),
                    Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true
                };
                patientPanel.Controls.Add(patientInfoLabel);
                custombtn btnEdit = CreateStyledButton("Edit Details", new Point(370, 210));
                btnEdit.Click += (s, e) =>
                {
                    ShowPatientDetails(patient, patientPanel.Location);
                };
                patientPanel.Controls.Add(btnEdit);

                custombtn btnSaveAsPDF = CreateStyledButton("Save as PDF", new Point(370, 250));
                btnSaveAsPDF.Click += (s, e) =>
                {
                    SavePatientDetailsAsPDF(patient);
                };
                patientPanel.Controls.Add(btnSaveAsPDF);
                scrollablePanel.Controls.Add(patientPanel);
            }
        }

        private async void SavePatientDetailsAsPDF(Patient patient)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save Patient Details as PDF";
                saveFileDialog.FileName = $"{patient.PetName}_{patient.OwnerName}.pdf"; // Using patient full name as the filename

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Create the PDF document and specify the page size
                        Document pdfDocument = new Document(PageSize.A4);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, fs);

                            // Open the document to add content
                            pdfDocument.Open();

                            // Add content to the PDF
                            Paragraph header = new Paragraph("Veterinary Clinic - Patient Details")
                            {
                                Alignment = Element.ALIGN_CENTER,
                            };
                            pdfDocument.Add(header);

                            pdfDocument.Add(new Paragraph($"Patient Name: {patient.PetName}"));
                            pdfDocument.Add(new Paragraph($"Patient ID: {patient.DocumentID}"));
                            pdfDocument.Add(new Paragraph($"Breed: {patient.PetBreed}"));
                            pdfDocument.Add(new Paragraph($"Animal Group: {patient.AnimalGroup}"));
                            pdfDocument.Add(new Paragraph($"Age: {patient.PetAge}"));
                            pdfDocument.Add(new Paragraph($"Owner: {patient.OwnerName}"));
                            pdfDocument.Add(new Paragraph($"Admission Date: {patient.AdmissionDate}"));

                            pdfDocument.Add(new Paragraph("\n-------------------------------"));
                            pdfDocument.Add(new Paragraph("Thank you for trusting our clinic!"));

                            // Close the document
                            pdfDocument.Close();
                            writer.Close();

                            // Optionally, upload the PDF to Firebase Storage (similar to transaction admin)
                            await UploadPDFToFirebaseStorage(filePath);
                            MessageBox.Show("Patient details saved as PDF and uploaded successfully.", "Success");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving PDF: {ex.Message}", "Error");
                    }
                }
            }
        }





        private async Task UploadPDFToFirebaseStorage(string filePath)
        {
            try
            {
                // Initialize Firebase Storage
                var storage = new FirebaseStorage("veterinaryclinic-60d24.appspot.com");

                // Upload file
                var fileStream = new FileStream(filePath, FileMode.Open);
                string fileName = Path.GetFileName(filePath); // You can customize this filename as needed

                var uploadTask = storage.Child("PatientFiles").Child(fileName).PutAsync(fileStream);
                await uploadTask;

                // Optionally, get the download URL after upload is complete
                string downloadUrl = await storage.Child("PatientFiles").Child(fileName).GetDownloadUrlAsync();

                // Handle the download URL if needed (e.g., display or save it)
                MessageBox.Show($"File uploaded to Firebase Storage. Download URL: {downloadUrl}", "Upload Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading to Firebase Storage: {ex.Message}", "Upload Error");
            }
        }

        private async Task UpdatePatientProfileImageAsync(Patient patient)
        {
            if (string.IsNullOrEmpty(patient.DocumentID))
            {
                MessageBox.Show("Patient record does not have a Document ID.");
                return;
            }
            DocumentReference docRef = firestoreDb.Collection("patients").Document(patient.DocumentID);
            await docRef.UpdateAsync(new Dictionary<string, object> { { "ProfileImage", patient.ProfileImage } });
            MessageBox.Show("Profile image updated successfully.");
        }
        private void ShowPatientDetails(Patient patient, Point clickedLocation)
        {
            scrollablePanel.Controls.Clear();
            Panel detailPanel = new Panel
            {
                Size = new Size(520, 300), 
                Location = clickedLocation, 
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };
            PictureBox pictureBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(80, 80), 
                Location = new Point(10, 10),
                Cursor = Cursors.Hand
            };
            if (!string.IsNullOrEmpty(patient.ProfileImage))
            {
                pictureBox.Image = ConvertBase64ToImage(patient.ProfileImage);
            }
            pictureBox.Click += async (s, e) =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox.Image = System.Drawing.Image.FromFile(openFileDialog.FileName);
                        string newBase64Image = ConvertImageToBase64(openFileDialog.FileName);
                        patient.ProfileImage = newBase64Image;
                        await UpdatePatientProfileImageAsync(patient);
                    }
                }
            };
            detailPanel.Controls.Add(pictureBox);
            Label lblPetName = new Label { Text = "Pet Name:", Location = new Point(100, 10), Width = 100, Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblPetName);
            TextBox txtPetName = new TextBox { Text = patient.PetName, Location = new Point(260, 10), Width = 200, Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(txtPetName);
            Label lblAnimalGroup = new Label { Text = "Animal Group:", Location = new Point(100, 40), Width = 150, Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblAnimalGroup);
            TextBox txtAnimalGroup = new TextBox { Text = patient.AnimalGroup, Location = new Point(260, 40), Width = 200, Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(txtAnimalGroup);
            Label lblPetAge = new Label { Text = "Pet Age:", Location = new Point(100, 70), Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblPetAge);
            TextBox txtPetAge = new TextBox { Text = patient.PetAge.ToString(), Location = new Point(260, 70), Width = 200, Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(txtPetAge);
            Label lblBreed = new Label { Text = "Pet Breed:", Location = new Point(100, 100), Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblBreed);
            TextBox txtBreed = new TextBox { Text = patient.PetBreed, Location = new Point(260, 100), Width = 200, Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(txtBreed);
            Label lblFindings = new Label { Text = "Findings:", Location = new Point(100, 130), Font = new System.Drawing.Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblFindings);
            PictureBox findingsBox = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(80, 80), 
                Location = new Point(260, 130),
                Cursor = Cursors.Hand,
                BackColor = Color.White
            };
            if (!string.IsNullOrEmpty(patient.Findings))
            {
                findingsBox.Image = ConvertBase64ToImage(patient.Findings);
            }
            findingsBox.Click += async (s, e) =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\"; // Starting directory
                    openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"; // Filter for image files
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.RestoreDirectory = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        findingsBox.Image = System.Drawing.Image.FromFile(openFileDialog.FileName); // Display the image in PictureBox

                        string newBase64Image = ConvertImageToBase64(openFileDialog.FileName); // Convert the image to base64
                        patient.Findings = newBase64Image; // Store the new base64 string in Findings field

                        await SaveFindingsToFirestore(patient); // Save to Firestore
                    }
                }
            };
            detailPanel.Controls.Add(findingsBox);
            custombtn btnSave = CreateStyledButton("Save", new Point(200, 250));
            btnSave.Click += async (s, e) =>
            {
                patient.PetName = txtPetName.Text;
                patient.AnimalGroup = txtAnimalGroup.Text;
                patient.PetAge = txtPetAge.Text;
                patient.PetBreed = txtBreed.Text;
                await UpdatePatientInFirestore(patient);
                DisplayPatientsData(await GetPatientsDataAsync()); // Refresh the patient list after saving
            };
            detailPanel.Controls.Add(btnSave);
            custombtn btnClose = CreateStyledButton1("Close", new Point(350, 250));
            btnClose.Click += async (s, e) => DisplayPatientsData(await GetPatientsDataAsync()); // Refresh on close
            detailPanel.Controls.Add(btnClose);
            scrollablePanel.Controls.Add(detailPanel);
        }
        private System.Drawing.Image ConvertBase64ToImage(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String); // Convert base64 string to byte array
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return System.Drawing.Image.FromStream(ms); // Convert byte array to image
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting base64 to image: {ex.Message}");
                return null;
            }
        }
        private string ConvertImageToBase64(string filePath)
        {
            try
            {
                byte[] imageBytes = File.ReadAllBytes(filePath);
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting image: {ex.Message}");
                return null;
            }
        }
        private async Task SaveFindingsToFirestore(Patient patient)
        {
            try
            {
                FirestoreDb firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24"); 
                DocumentReference docRef = firestoreDb.Collection("patients").Document(patient.DocumentID.ToString());
                await docRef.UpdateAsync("Findings", patient.Findings);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to Firestore: {ex.Message}");
            }
        }
        private async Task UpdatePatientInFirestore(Patient patient)
        {
            var firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
            var patientRef = firestoreDb.Collection("patients").Document(patient.DocumentID);
            await patientRef.SetAsync(patient, SetOptions.MergeAll);
        }
        public static class ImageConverter
        {
            public static string ConvertImageToBase64(string filePath)
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
                return Convert.ToBase64String(imageBytes);
            }
            public static System.Drawing.Image ConvertBase64ToImage(string base64String)
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return System.Drawing.Image.FromStream(ms);
                }
            }
        }
        private custombtn CreateStyledButton(string text, Point location)
        {
            custombtn button = new custombtn
            {
                Text = text,
                BackColor = System.Drawing.Color.FromArgb(0, 61, 83),
                BackgroundColor = System.Drawing.Color.FromArgb(0, 61, 83),
                BorderColor = System.Drawing.Color.PaleVioletRed,
                BorderRadius = 20,
                BorderSize = 0,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Size = new Size(130, 40),
                Location = location,
                TextColor = System.Drawing.Color.White
            };
            return button;
        }
        private custombtn CreateStyledButton1(string text, Point location)
        {
            custombtn button = new custombtn
            {
                Text = text,
                BackColor = System.Drawing.Color.FromArgb(255, 0, 0),
                BackgroundColor = System.Drawing.Color.FromArgb(0, 61, 83),
                BorderColor = System.Drawing.Color.PaleVioletRed,
                BorderRadius = 20,
                BorderSize = 0,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Size = new Size(130, 40),
                Location = location,
                TextColor = System.Drawing.Color.White
            };
            return button;
        }
    }
}
