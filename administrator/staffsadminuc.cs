using Google.Cloud.Firestore;
using VeterinaryClinicApp.addcontrols;
namespace VeterinaryClinicApp
{
    public partial class staffsadminuc : UserControl
    {
        private Panel scrollablePanel;
        private FirestoreDb firestoreDb;
        public staffsadminuc()
        {
            InitializeComponent();
            InitializeFirestore();
        }
        private async void staffsadminuc_Load(object sender, EventArgs e)
        {
            InitializeScrollablePanel();
            List<Staff> staffs = await GetStaffDataAsync();
            DisplayStaffData(staffs);
        }
        public void InitializeFirestore()
        {
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
            Console.WriteLine("Connected to Firestore");
        }
        private void InitializeScrollablePanel()
        {
            scrollablePanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1040, 566),
                AutoScroll = true
            };
            this.Controls.Add(scrollablePanel);
        }
        private async Task<List<Staff>> GetStaffDataAsync()
        {
            var staffsList = new List<Staff>();
            try
            {
                QuerySnapshot snapshot = await firestoreDb.Collection("staffs").GetSnapshotAsync();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        try
                        {
                            Staff staff = document.ConvertTo<Staff>();
                            staff.DocumentID = document.Id;
                            if (staff != null)
                            {
                                staffsList.Add(staff);
                            }
                            else
                            {
                                Console.WriteLine("Warning: A document was found but could not be converted to a Staff object.");
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
                Console.WriteLine($"Error fetching staffs data: {ex.Message}");
            }
            return staffsList;
        }
        private void DisplayStaffData(List<Staff> staffs)
        {
            int columns = 2;
            int panelWidth = 500;
            int panelHeight = 200;
            int xOffset = 10;
            int yOffset = 10;
            int spacing = 10;
            scrollablePanel.Controls.Clear();
            scrollablePanel.BackColor = Color.White;
            scrollablePanel.Dock = DockStyle.Fill;
            for (int i = 0; i < staffs.Count; i++)
            {
                var staff = staffs[i];
                int row = i / columns;
                int column = i % columns;
                Panel staffPanel = new Panel
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
                if (!string.IsNullOrEmpty(staff.pbProfile))
                {
                    pictureBox.Image = ConvertBase64ToImage(staff.pbProfile);
                }
                staffPanel.Controls.Add(pictureBox);
                Label staffInfoLabel = new Label
                {
                    Text = $"Staff Name: {staff.staffname}\n" +
                           $"Staff Age: {staff.staffage}\n" +
                           $"Staff Gender: {staff.gender}\n" ,
                    Location = new Point(100, 10),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    AutoSize = true
                };
                staffPanel.Controls.Add(staffInfoLabel);
                scrollablePanel.Controls.Add(staffPanel);
            }
        }
        private Image ConvertBase64ToImage(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String); // Convert base64 string to byte array
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms); // Convert byte array to image
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting base64 to image: {ex.Message}");
                return null;
            }
        }
        private void btnaddstaff_Click(object sender, EventArgs e)
        {
            ShowAddStaffs();
        }
        private void ShowAddStaffs()
        {
            addstaffs addstaffs = new addstaffs
            {
                Location = new Point(0, 0),
                Dock = DockStyle.Fill
            };
            scrollablePanel.Controls.Add(addstaffs);
            addstaffs.BringToFront();
        }
    }
}
