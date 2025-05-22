using Google.Cloud.Firestore;
namespace VeterinaryClinicApp
{
    public partial class offersadminuc : UserControl
    {
        private Panel scrollablePanelServices;
        private Panel scrollablePanelProducts;
        private FirestoreDb firestoreDb;

        public offersadminuc()
        {
            InitializeComponent();
            InitializeFirestore();
            InitializeScrollablePanel();
        }
        private async void offersadminuc_Load(object sender, EventArgs e)
        {
            var (services, products) = await FetchOffersAsync();
            DisplayOffers(services, scrollablePanelServices);
            DisplayOffers(products, scrollablePanelProducts);
        }
        private void InitializeFirestore()
        {
            try
            {
                firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
                Console.WriteLine("Connected to Firestore");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Firestore: {ex.Message}");
            }
        }
        private void InitializeScrollablePanel()
        {
            scrollablePanelServices = new Panel
            {
                Location = new Point(5, 30),
                Size = new Size(500, 530),
                AutoScroll = true,
                BackColor = Color.White,
            };
            this.Controls.Add(scrollablePanelServices);
            scrollablePanelProducts = new Panel
            {
                Location = new Point(520, 30),
                Size = new Size(500, 530),
                AutoScroll = true,
                BackColor = Color.White,
            };
            this.Controls.Add(scrollablePanelProducts);
        }
        private async Task<(List<Offer> Services, List<Offer> Products)> FetchOffersAsync()
        {
            var servicesList = new List<Offer>();
            var productsList = new List<Offer>();
            try
            {
                string[] documentNames = { "Services", "Products" }; // Document names in Firestore

                foreach (var documentName in documentNames)
                {
                    DocumentReference docRef = firestoreDb.Collection("offers").Document(documentName);
                    DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
                    if (snapshot.Exists)
                    {
                        Console.WriteLine($"Fetching offers from: {documentName}");
                        foreach (var field in snapshot.ToDictionary())
                        {
                            if (field.Value is Dictionary<string, object> offerData)
                            {
                                var offer = new Offer
                                {
                                    OfferUID = field.Key,
                                    OfferName = offerData.ContainsKey("OfferName") ? offerData["OfferName"].ToString() : "",
                                    Price = offerData.ContainsKey("Price") ? offerData["Price"].ToString() : "",
                                    Description = offerData.ContainsKey("Description") ? offerData["Description"].ToString() : "",
                                    Image = offerData.ContainsKey("Image") ? offerData["Image"].ToString() : ""
                                };

                                if (documentName == "Services")
                                {
                                    servicesList.Add(offer);
                                }
                                else if (documentName == "Products")
                                {
                                    productsList.Add(offer);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Document {documentName} does not exist in 'offers' collection.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching offers: {ex.Message}");
            }
            return (servicesList, productsList);
        }
        public void DisplayOffers(List<Offer> offers, Panel targetPanel)
        {
            if (offers == null || offers.Count == 0)
            {
                MessageBox.Show("No offers to display.");
                return;
            }
            targetPanel.Controls.Clear();
            targetPanel.AutoScroll = true;
            int panelWidth = 475;
            int panelHeight = 180;
            int spacing = 10;
            int xOffset = 5; 
            int yOffset = 5;
            for (int i = 0; i < offers.Count; i++)
            {
                var offer = offers[i];
                Panel offerPanel = new Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    Location = new Point(xOffset, yOffset + i * (panelHeight + spacing)),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray
                };
                PictureBox pictureBox = new PictureBox
                {
                    Size = new Size(100, 100),
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = !string.IsNullOrEmpty(offer.Image) ? ImageConverter.ConvertBase64ToImage(offer.Image) : null
                };
                offerPanel.Controls.Add(pictureBox);
                Label offerInfoLabel = new Label
                {
                    Text = $"Name: {offer.OfferName}\n" +
                           $"Price: {offer.Price}\n" +
                           $"Description: {offer.Description}",
                    Location = new Point(120, 10),
                    Size = new Size(330, 170),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                };
                offerPanel.Controls.Add(offerInfoLabel);
                targetPanel.Controls.Add(offerPanel);
            }
        }
        private void btnaddoffer_Click(object sender, EventArgs e)
        {
            ShowAddOffers();
        }
        private void ShowAddOffers()
        {
            try
            {
                addoffers addoffers = new addoffers
                {
                    Location = new Point(0, 0),
                    Dock = DockStyle.Fill
                };
                this.Controls.Clear();
                this.Controls.Add(addoffers);
                addoffers.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying Add Offers form: {ex.Message}");
            }
        }
        public static class ImageConverter
        {
            public static Image ConvertBase64ToImage(string base64String)
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
        }
    }
}
