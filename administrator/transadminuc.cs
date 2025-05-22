using Google.Cloud.Firestore;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Firebase.Storage;

namespace VeterinaryClinicApp.administrator
{
    public partial class transadminuc : UserControl
    {
        private FirestoreDb firestoreDb;
        private Panel scrollablePanel;
        public transadminuc()
        {
            InitializeComponent();
            InitializeFirestore();
        }
        private async void transadminuc_Load(object sender, EventArgs e)
        {
            InitializeScrollablePanel();
            List<Transaction> transactions = await GetTransactionsDataAsync();
            DisplayTransactionsData(transactions);
        }
        public void InitializeFirestore()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"D:\capstone2\codings\VeterinaryClinicApp\veterinaryclinic-60d24-firebase-adminsdk-ptgn7-f1f61ef6e8.json");
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
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
        private async Task<List<Transaction>> GetTransactionsDataAsync()
        {
            var transactionsList = new List<Transaction>();
            try
            {
                QuerySnapshot snapshot = await firestoreDb.Collection("transactions").GetSnapshotAsync();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        Transaction transaction = document.ConvertTo<Transaction>();
                        transactionsList.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching transactions data: {ex.Message}");
            }
            return transactionsList;
        }
        private async void DisplayTransactionsData(List<Transaction> transactions)
        {
            int columns = 2; 
            int panelWidth = 510;
            int panelHeight = 320;
            int xOffset = 10;
            int yOffset = 20;
            int spacing = 20;
            scrollablePanel.Controls.Clear();
            scrollablePanel.Dock = DockStyle.Fill;
            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];
                int row = i / columns;
                int column = i % columns;
                Panel transactionPanel = new Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    Location = new Point(xOffset + (column * (panelWidth + spacing)), yOffset + (row * (panelHeight + spacing))),
                    BorderStyle = BorderStyle.FixedSingle
                };
                FlowLayoutPanel flowPanel = new FlowLayoutPanel
                {
                    Location = new Point(15, 15),
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown
                };
                Label transactionInfoLabel = new Label
                {
                    Text = $"Reference ID: {transaction.ReferenceID}\n" +
                           $"User ID: {transaction.UserId}\n" +
                           $"Service: {transaction.Service}\n" +
                           $"Product: {transaction.Product}\n" +
                           $"Payment Method: {transaction.PaymentMethod}\n" +
                           $"Animal: {transaction.Animal}\n" +
                           $"Date: {transaction.Date} Time: {transaction.Time}\n" +
                           $"Created At: {transaction.CreatedAt.ToDateTime():f}\n" +
                           $"Price: {transaction.TotalPrice:C}\n"+
                           $"Amount Paid: {transaction.AmountPaid}\n" ,
                    Location = new Point(15, 15),
                    Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
                    BackColor = Color.Transparent,
                    AutoSize = true
                };
                Label statusLabel = new Label
                {
                    Text = $"Status: {transaction.Status}",
                    Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                    switch (transaction.Status.ToUpper())
                    { 
                    
                    case "COMPLETED": statusLabel.ForeColor = Color.Green; 
                        break; 
                    
                    }
                flowPanel.Controls.Add(transactionInfoLabel);
                flowPanel.Controls.Add(statusLabel);
                transactionPanel.Controls.Add(flowPanel);
                custombtn btnPrint = CreateStyledButton("Print", new Point(160, 260));
                btnPrint.Click += (s, e) => ShowPrintWindow(transaction);
                transactionPanel.Controls.Add(btnPrint);

                custombtn btnSaveAsPDF = CreateStyledButton("Save as PDF", new Point(320, 260));
                btnSaveAsPDF.Click += (s, e) => SaveReceiptAsPDFAndUpload(transaction);
                transactionPanel.Controls.Add(btnSaveAsPDF);

                scrollablePanel.Controls.Add(transactionPanel);
            }
        }

        private async void SaveReceiptAsPDFAndUpload(Transaction transaction)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Save Receipt as PDF";
                saveFileDialog.FileName = $"{transaction.UserId}_{transaction.ReferenceID}.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    try
                    {
                        // Create the PDF document and specify the page size
                        Document pdfDocument = new Document(PageSize.A4);

                        // Create the PdfWriter to write to the stream
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDocument, fs);

                            // Open the document to add content
                            pdfDocument.Open();

                            // Add content to the PDF
                            Paragraph header = new Paragraph("Veterinary Clinic")
                            {
                                Alignment = Element.ALIGN_CENTER,
                                //Font = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)
                            };
                            pdfDocument.Add(header);

                            Paragraph title = new Paragraph("Transaction Details")
                            {
                                Alignment = Element.ALIGN_CENTER,
                                //Font = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)
                            };
                            pdfDocument.Add(title);

                            pdfDocument.Add(new Paragraph($"Transaction ID: {transaction.DocumentID}"));
                            pdfDocument.Add(new Paragraph($"Date: {DateTime.Now.ToShortDateString()}"));
                            pdfDocument.Add(new Paragraph($"Time: {DateTime.Now.ToShortTimeString()}"));
                            pdfDocument.Add(new Paragraph($"Created: {transaction.Animal}"));

                            pdfDocument.Add(new Paragraph("\nService Details:"));
                            pdfDocument.Add(new Paragraph($"Service: {transaction.Service}"));
                            pdfDocument.Add(new Paragraph($"Product: {transaction.Product}"));

                            pdfDocument.Add(new Paragraph("\nPayment Details:"));
                            pdfDocument.Add(new Paragraph($"Payment Method: {transaction.PaymentMethod}"));
                            pdfDocument.Add(new Paragraph($"Animal: {transaction.Animal}"));
                            pdfDocument.Add(new Paragraph($"Reference ID: {transaction.ReferenceID}"));

                            pdfDocument.Add(new Paragraph("\n-------------------------------"));
                            pdfDocument.Add(new Paragraph($"Total Price: {transaction.TotalPrice:C}"));
                            pdfDocument.Add(new Paragraph($"Amount Paid: {transaction.AmountPaid:C}"));
                            pdfDocument.Add(new Paragraph("-------------------------------"));

                            Paragraph footer = new Paragraph("\nThank you for your visit!")
                            {
                                Alignment = Element.ALIGN_CENTER,
                                //Font = new Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC)
                            };
                            pdfDocument.Add(footer);

                            // Close the document
                            pdfDocument.Close();
                            writer.Close();

                            // Upload PDF to Firebase Storage
                            await UploadPDFToFirebaseStorage(filePath);
                            MessageBox.Show("Receipt saved and uploaded to Firebase Storage successfully.", "Success");
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

                var uploadTask = storage.Child("Receipts").Child(fileName).PutAsync(fileStream);
                await uploadTask;

                // Optionally, get the download URL after upload is complete
                string downloadUrl = await storage.Child("Receipts").Child(fileName).GetDownloadUrlAsync();

                // Handle the download URL if needed (e.g., display or save it)
                MessageBox.Show($"File uploaded to Firebase Storage. Download URL: {downloadUrl}", "Upload Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading to Firebase Storage: {ex.Message}", "Upload Error");
            }
        }

        private void ShowPrintWindow(Transaction transaction)
        {
            printwindow printWindow = new printwindow
            {
                StartPosition = FormStartPosition.CenterScreen, 
                TopMost = true 
            };
            printWindow.SetTransactionDetails(transaction);
            printWindow.ShowDialog();
        }
        private custombtn CreateStyledButton(string text, Point location)
        {
            custombtn button = new custombtn
            {
                Text = text,
                BackColor = System.Drawing.Color.FromArgb(0, 61, 83),
                BackgroundColor = System.Drawing.Color.FromArgb(255, 0, 0),
                BorderColor = System.Drawing.Color.PaleVioletRed,
                BorderRadius = 20,
                BorderSize = 0,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Size = new Size(150, 40),
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
                BackColor = System.Drawing.Color.FromArgb(0, 61, 83),
                BackgroundColor = System.Drawing.Color.FromArgb(60, 179, 113),
                BorderColor = System.Drawing.Color.PaleVioletRed,
                BorderRadius = 20,
                BorderSize = 0,
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Size = new Size(150, 40),
                Location = location,
                TextColor = System.Drawing.Color.White
            };
            return button;
        }
    }
}
