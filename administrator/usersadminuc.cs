using Google.Cloud.Firestore;
namespace VeterinaryClinicApp.administrator
{
    public partial class usersadminuc : UserControl
    {
        private FirestoreDb firestoreDb;
        private Panel scrollableUserPanel;
        private string selectedUserID;
        private string selectedFullName;
        public usersadminuc()
        {
            InitializeComponent();
            InitializeFirestore();
        }
        private async void usersadminuc_Load(object sender, EventArgs e)
        {
            InitializeScrollablePanel();
            List<User> users = await GetUsersDataAsync();
            DisplayUsersData(users);
        }
        public void InitializeFirestore()
        {
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24"); 
        }
        private void InitializeScrollablePanel()
        {
            scrollableUserPanel = new Panel
            {
                Size = new Size(1040, 500),
                AutoScroll = true,
                BackColor = Color.White,
            };
            this.Controls.Add(scrollableUserPanel);
        }
        private async Task<List<User>> GetUsersDataAsync()
        {
            var usersList = new List<User>();
            try
            {
                QuerySnapshot snapshot = await firestoreDb.Collection("users").GetSnapshotAsync();
                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        User user = document.ConvertTo<User>();
                        user.DocumentID = document.Id;
                        usersList.Add(user);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users data: {ex.Message}");
            }
            return usersList;
        }
        private void DisplayUsersData(List<User> users)
        {
            int columns = 2; // Number of columns
            int panelWidth = 500;
            int panelHeight = 100;
            int xOffset = 10;
            int yOffset = 10;
            int spacing = 20;
            scrollableUserPanel.Controls.Clear();
            scrollableUserPanel.BackColor = Color.White;
            scrollableUserPanel.Dock = DockStyle.Fill;
            for (int i = 0; i < users.Count; i++)
            {
                var user = users[i];
                int row = i / columns;
                int column = i % columns;
                Panel userPanel = new Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    Location = new Point(xOffset + (column * (panelWidth + spacing)), yOffset + (row * (panelHeight + spacing))),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray,
                };
                Label userInfoLabel = new Label
                {
                    Text = $"Name: {user.FullName}\n" +
                                $"Email: {user.Email}\n",
                    Location = new Point(10, 10),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                userPanel.Controls.Add(userInfoLabel);
                userPanel.Click += (s, e) => ExpandUserDetails(user, userPanel.Location);
                scrollableUserPanel.Controls.Add(userPanel);
            }
        }
        private void ExpandUserDetails(User user, Point originalLocation)
        {
            selectedUserID = user.DocumentID;
            selectedFullName = user.FullName;
            scrollableUserPanel.Controls.Clear();
            scrollableUserPanel.BackColor = Color.LightGray;
            Panel detailPanel = new Panel
            {
                Size = new Size(550, 220),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            int centerX = (scrollableUserPanel.Width - detailPanel.Width) / 2; 
            int centerY = (scrollableUserPanel.Height - detailPanel.Height) / 2; 
            detailPanel.Location = new Point(centerX, centerY);
            Label userInfoLabel = new Label
            {
                Text = $"Full Name: {user.FullName}\n" +
                            $"Gender: {user.Gender}\n"+
                            $"Email: {user.Email}\n" +
                            $"Phone: {user.PhoneNumber}\n" +
                            $"Birthdate: {user.Birthdate.ToDateTime():f}\n" +
                            $"Account Created: {user.CreatedAt.ToDateTime():f}\n" +
                            $"Address: {user.Address}\n",
                Location = new Point(10, 10),
                Font = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true
            };
            detailPanel.Controls.Add(userInfoLabel);
            custombtn addPetButton = CreateStyledButton("Add Pet", new Point(340, 170));
            addPetButton.Click += (s, e) => ShowAddPetControl();
            detailPanel.Controls.Add(addPetButton);
            custombtn btnClose = CreateStyledButton("Close", new Point(440, 170));
            btnClose.Click += async (s, e) => DisplayUsersData(await GetUsersDataAsync()); // Refresh patient list
            detailPanel.Controls.Add(btnClose);
            custombtn addRfidButton = CreateStyledButton("Add RFID Tag", new Point(240, 170));
            TextBox rfidTextBox = new TextBox
            {
                Location = new Point(10, userInfoLabel.Bottom + 10),
                Width = 200,
                Visible = false
            };
            addRfidButton.Click += (s, e) =>
            {
                if (!rfidTextBox.Visible)
                {
                    rfidTextBox.Visible = true;
                    addRfidButton.Text = "Save RFID";
                }
                else
                {
                    SaveRfidToUser(user.DocumentID, rfidTextBox.Text);
                    addRfidButton.Text = "Add RFID Tag";
                    rfidTextBox.Visible = false;
                }
            };
            detailPanel.Controls.Add(addRfidButton);
            detailPanel.Controls.Add(rfidTextBox);
            scrollableUserPanel.Controls.Add(detailPanel);
        }
        private async void SaveRfidToUser(string userId, string rfidTag)
        {
            if (!string.IsNullOrWhiteSpace(rfidTag))
            {
                var userDoc = FirestoreDb.Create("veterinaryclinic-60d24").Collection("users").Document(userId);
                await userDoc.UpdateAsync(new Dictionary<string, object>
        {
            { "RFIDTag", rfidTag }
        });
                MessageBox.Show("RFID Tag saved successfully!");
            }
            else
            {
                MessageBox.Show("Please enter a valid RFID Tag.");
            }
        }
        private void ShowAddPetControl()
        {
            addpetinfo addPetControl = new addpetinfo(selectedUserID, selectedFullName)
            {
                Location = new Point(0, 0),
                Dock = DockStyle.Fill
            };
            scrollableUserPanel.Controls.Add(addPetControl);
            addPetControl.BringToFront();
        }
        private custombtn CreateStyledButton(string text, Point location)
        {
            custombtn button = new custombtn
            {
                Text = text,
                BackColor = System.Drawing.Color.FromArgb(0, 255, 0),
                BackgroundColor = System.Drawing.Color.FromArgb(0, 255, 0),
                BorderColor = System.Drawing.Color.PaleVioletRed,
                BorderRadius = 20,
                BorderSize = 0,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Size = new Size(100, 40),
                Location = location,
                TextColor = System.Drawing.Color.White
            };
            return button;
        }
    }
}
