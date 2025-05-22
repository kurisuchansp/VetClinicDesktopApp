using System.Data;
using System.IO.Ports;
using Google.Cloud.Firestore;
using Timer = System.Windows.Forms.Timer;
namespace VeterinaryClinicApp
{
    public partial class bookingadminuc : UserControl
    {
        private Timer timer;
        private FirestoreDb firestoreDb;
        private Panel scrollablePanel;
        private RFIDReader rfidReader;
        private SerialPort rfidSerialPort;
        private TextBox txtScannedRfid;
        private Label lblTotalPrice;

        private List<Booking> allBookings;
        public bookingadminuc()
        {
            InitializeComponent();
            InitializeFirestore();
        }
        public void InitializeFirestore()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"D:\capstone2\codings\netcore\VeterinaryClinicApp\veterinaryclinic-60d24-firebase-adminsdk-ptgn7-ab06babd4f.json");
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
        }
        private async void bookingadminuc_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 60000; 
            timer.Tick += (sender, e) =>
            {
                lblcurrentdate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy h:mm tt");
            };
            timer.Start();
            DisplayCurrentDate();
            InitializeScrollablePanel(); 
            allBookings = await GetBookingsDataAsync(); 
            PopulateDateComboBox(); 
            DisplayBookingsData(allBookings);
        }
        private void InitializeScrollablePanel()
        {
            scrollablePanel = new Panel
            {
                Location = new Point(0, 40),
                Size = new Size(1040, 515), 
                AutoScroll = true 
            };
            this.Controls.Add(scrollablePanel);
        }

        

        private async Task<List<Booking>> GetBookingsDataAsync()
        {
            var bookingsList = new List<Booking>();
            try
            {
                QuerySnapshot snapshot = await firestoreDb.Collection("bookings").GetSnapshotAsync();

                foreach (DocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Exists)
                    {
                        Booking booking = document.ConvertTo<Booking>();
                        booking.DocumentID = document.Id;
                        bookingsList.Add(booking);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users data: {ex.Message}");
            }
            return bookingsList;
        }
        private void PopulateDateComboBox()
        {
            cmbdateselection.Items.Add("All Bookings");
            cmbdateselection.Items.Add("Today");
            var uniqueDates = allBookings.Select(b => b.Date).Distinct().OrderBy(date => DateTime.Parse(date)).ToList();
            foreach (var date in uniqueDates)
            {
                DateTime parsedDate = DateTime.Parse(date);
                string dayOfWeek = parsedDate.ToString("dddd"); 
                string formattedDate = parsedDate.ToString("yyyy-MM-dd") + " (" + dayOfWeek + ")";
                cmbdateselection.Items.Add(formattedDate);
            }
            cmbdateselection.SelectedIndex = 0;
        }
        private void cmbdateselection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedDate = cmbdateselection.SelectedItem.ToString();
            List<Booking> filteredBookings;
            if (selectedDate == "Today")
            {
                DateTime currentDate = DateTime.Parse(lblcurrentdate.Text); 
                filteredBookings = allBookings.Where(b => DateTime.Parse(b.Date).Date == currentDate.Date).ToList();
            }
            else if (selectedDate == "All Bookings")
            {
                filteredBookings = allBookings;
            }
            else
            {
                string dateOnly = selectedDate.Split(' ')[0];
                DateTime selectedDateTime;
                if (DateTime.TryParse(dateOnly, out selectedDateTime))
                {
                    filteredBookings = allBookings.Where(b => DateTime.Parse(b.Date).Date == selectedDateTime.Date).ToList();
                }
                else
                {
                    filteredBookings = new List<Booking>();
                }
            }
            DisplayBookingsData(filteredBookings);
        }
        private void DisplayBookingsData(List<Booking> bookings)
        {
            int columns = 2; 
            int panelWidth = 500;
            int panelHeight = 315;
            int xOffset = 10;
            int yOffset = 10;
            int spacing = 10;
            scrollablePanel.Controls.Clear();
            for (int i = 0; i < bookings.Count; i++)
            {
                var booking = bookings[i];
                int row = i / columns;
                int column = i % columns;
                Panel bookingPanel = new Panel
                {
                    Size = new Size(panelWidth, panelHeight),
                    Location = new Point(xOffset + (column * (panelWidth + spacing)), yOffset + (row * (panelHeight + spacing))),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.LightGray,
                };
                FlowLayoutPanel flowPanel = new FlowLayoutPanel
                {
                    Location = new Point(15, 15),
                    AutoSize = true,
                    FlowDirection = FlowDirection.TopDown
                };
                Label bookingInfoLabel = new Label
                {
                    Text = $"Reference ID: {booking.ReferenceID}\n" +
                                $"User ID: {booking.UserId}\n" +
                                $"Service: {booking.Service}\n" +
                                $"Product: {booking.Product}\n" +
                                $"Payment Method: {booking.PaymentMethod}\n" +
                                $"Animal: {booking.Animal}\n" +
                                $"Appointment Date: {booking.Date} {booking.Time}\n" +
                                $"Date Created: {booking.CreatedAt.ToDateTime():f}\n" +
                                $"Price: {booking.TotalPrice:C}\n",
                    Location = new Point(15, 15),
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                Label statusLabel = new Label
                {
                    Text = $"Status: {booking.Status}",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true
                };
                switch (booking.Status.ToLower())
                {
                    case "PROCESSING":
                        statusLabel.ForeColor = Color.Orange;
                        break;
                    case "CANCELLED":
                        statusLabel.ForeColor = Color.Red;
                        break;
                    default:
                        statusLabel.ForeColor = Color.Black;
                        break;
                }
                flowPanel.Controls.Add(bookingInfoLabel);
                flowPanel.Controls.Add(statusLabel);
                bookingPanel.Controls.Add(flowPanel);
                custombtn btnSend = CreateStyledButton("Send Reminder", new Point(340, 260));
                btnSend.Click += async (sender, args) =>
                {
                    await SaveReminderAsync(booking);
                };
                bookingPanel.Controls.Add(btnSend);
                custombtn btnPayment = CreateStyledButton1("Payment", new Point(220, 260));
                btnPayment.Click += (s, e) =>
                {
                    ShowBookingDetails(booking, bookingPanel.Location);
                };
                bookingPanel.Controls.Add(btnPayment);
                custombtn btnProcess = CreateStyledButton2("Process", new Point(100, 260));
                btnProcess.Click += async (sender, args) =>
                {
                    booking.Status = "Processing";
                    await UpdateBookingStatusAsync(booking);
                    await RefreshBookingsDataAsync();
                    MessageBox.Show("Booking status updated to 'Processing'!");
                };
                bookingPanel.Controls.Add(btnProcess);
                scrollablePanel.Controls.Add(bookingPanel);
            }
        }
        private async Task UpdateBookingStatusAsync(Booking booking)
        {
            try
            {
                var bookingRef = firestoreDb.Collection("bookings").Document(booking.DocumentID);
                await bookingRef.UpdateAsync("Status", "Processing");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating status: " + ex.Message);
            }
        }
        private void ShowBookingDetails(Booking booking, Point clickedLocation)
        {
            scrollablePanel.Controls.Clear();
            Panel detailPanel = new Panel
            {
                Size = new Size(520, 300), 
                Location = clickedLocation,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray
            };
            Label lblReferenceID = new Label { Text = $"Reference ID: {booking.ReferenceID}", Location = new Point(10, 10), Width = 300, Font = new Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblReferenceID);
            Label lblTotalPrice = new Label { Text = $"Total Price: {booking.TotalPrice:C}", Location = new Point(10, 50), Width = 300, Font = new Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblTotalPrice);
            Label lblAmountPaid = new Label { Text = "Amount Paid:", Location = new Point(10, 90), Width = 120, Font = new Font("Arial", 12, FontStyle.Bold) };
            detailPanel.Controls.Add(lblAmountPaid);
            TextBox txtAmountPaid = new TextBox { Location = new Point(200, 90), Width = 200, Font = new Font("Arial", 12) };
            detailPanel.Controls.Add(txtAmountPaid);
            Button btnSave = new Button
            {
                Text = "Save Transaction",
                Location = new Point(200, 130),
                Size = new Size(150, 40),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            btnSave.Click += async (sender, args) =>
            {
                 SaveTransactionAsync(booking, txtAmountPaid.Text);
                scrollablePanel.Controls.Clear();
                await RefreshBookingsDataAsync();
            };
            detailPanel.Controls.Add(btnSave);

            Button btnScanCard = new Button
            {
                Text = "Scan RFID Card",
                Location = new Point(10, 170),
                Size = new Size(150, 40),
                BackColor = Color.Blue,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            btnScanCard.Click += async (sender, args) =>
            {
                //await HandleRFIDScanAsync();
            };
            detailPanel.Controls.Add(btnScanCard);
            // Initialize RFID TextBox if not already initialized
            if (txtScannedRfid == null)
            {
                txtScannedRfid = new TextBox
                {
                    Visible = false,
                    Location = new Point(10, 130),
                    Size = new Size(175, 40)
                };
                detailPanel.Controls.Add(txtScannedRfid);
            }

            scrollablePanel.Controls.Add(detailPanel);
        }
        /*
        private async Task HandleRFIDScanAsync()
        {
            
            if (txtScannedRfid.Visible)
            {
                // If the RFID input box is visible, it means the user has scanned the RFID.
                string rfidTag = txtScannedRfid.Text.Trim();
                if (string.IsNullOrWhiteSpace(rfidTag))
                {
                    MessageBox.Show("Please scan a valid RFID tag.");
                    return;
                }

                // Query Firestore to find the user associated with this RFID tag
                await CheckLoyaltyPointsAsync(rfidTag);
            }
            else
            {
                // Show the TextBox to allow the user to scan the RFID tag
                txtScannedRfid.Visible = true;
                txtScannedRfid.Clear();
            }
        }

        private async Task CheckLoyaltyPointsAsync(string rfidTag)
        {
            try
            {
                // Query Firestore to find the user with the scanned RFID tag
                Query query = firestoreDb.Collection("users").WhereEqualTo("RFIDTag", rfidTag);
                QuerySnapshot snapshot = await query.GetSnapshotAsync();

                if (snapshot.Count == 0)
                {
                    MessageBox.Show("No user found for the scanned RFID tag.");
                    return;
                }

                // Assume the first document is the correct user (RFID tags should be unique)
                DocumentSnapshot document = snapshot.Documents[0];

                // Extract user details
                string userId = document.Id;
                string fullname = document.GetValue<string>("fullName");
                int loyaltyPoints = document.GetValue<int>("loyaltyPoints");

                if (loyaltyPoints <= 0)
                {
                    MessageBox.Show("No loyalty points available to redeem.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Calculate discount (e.g., 1 point = $1 discount)
                decimal discountAmount = loyaltyPoints; // Adjust calculation logic as needed
                MessageBox.Show($"{loyaltyPoints} loyalty points redeemed for a ${discountAmount} discount.", "Loyalty Points Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Deduct loyalty points and update Firestore
                Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { "loyaltyPoints", 0 }  // Reset loyalty points after redeeming
        };

                await document.Reference.UpdateAsync(updates);

                // Apply the discount to the transaction
                ApplyDiscountToTransaction(discountAmount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error handling loyalty points: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyDiscountToTransaction(decimal discountAmount)
        {
            // Logic to apply the discount to the total price
            decimal totalPrice = GetCurrentTransactionTotal(booking); // Retrieve the current transaction total
            decimal newTotal = totalPrice - discountAmount;

            if (newTotal < 0) newTotal = 0; // Ensure the total doesn't go negative

            UpdateTransactionTotal(newTotal); // Update the total price display
            MessageBox.Show($"New total after discount: ${newTotal}", "Discount Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private decimal GetCurrentTransactionTotal(Booking booking)
        {
            return booking.TotalPrice;
        }

        private void UpdateTransactionTotal(decimal newTotal)
        {
            // Update the transaction total in the UI (e.g., label or textbox)
            lblTotalPrice.Text = $"Total Price: ${newTotal}";
        }*/

        private async Task SaveTransactionAsync(Booking booking, string amountPaid)
        {
            try
            {
                decimal paidAmount = decimal.Parse(amountPaid);
                int paidAmountInt = (int)paidAmount;
                string transactionUID = Guid.NewGuid().ToString();
                var transactionData = new Dictionary<string, object>
                {
                    { "referenceID", booking.ReferenceID },
                    { "userId", booking.UserId },
                    { "service", booking.Service },
                    { "product", booking.Product },
                    { "paymentMethod", booking.PaymentMethod },
                    { "animal", booking.Animal },
                    { "date", booking.Date },
                    { "time", booking.Time },
                    { "totalPrice", booking.TotalPrice },
                    { "paidAmount", paidAmountInt },
                    { "Status", "Completed" },
                    { "status", "Paid" },
                    { "message", "You are paid. You have a successful transaction with us." },
                    { "createdAt", Timestamp.GetCurrentTimestamp() }
                };
                CollectionReference transactionsRef = firestoreDb.Collection("transactions");
                DocumentReference transactionRef = transactionsRef.Document(transactionUID);
                await transactionRef.SetAsync(transactionData);
                CollectionReference salesRef = firestoreDb.Collection("sales");
                DocumentReference salesDocRef = salesRef.Document();
                await salesDocRef.SetAsync(transactionData);
                DocumentReference bookingRef = firestoreDb.Collection("bookings").Document(booking.DocumentID);
                await bookingRef.DeleteAsync();
                MessageBox.Show("Transaction saved and booking deleted successfully!");

                // Calculate loyalty points
                int loyaltyPoints = 0;
                if (booking.TotalPrice < 1000)
                {
                    loyaltyPoints = 1; // Adding 0.5 rounded to 1 for integer fields in Firestore
                }
                else if (booking.TotalPrice >= 1000 && booking.TotalPrice < 2000)
                {
                    loyaltyPoints = 2;
                }

                else if (booking.TotalPrice >=2000 && booking.TotalPrice < 3000)
                {
                    loyaltyPoints=3;
                }
                else if (booking.TotalPrice >= 3000 && booking.TotalPrice < 4000)
                {
                    loyaltyPoints = 4;
                }
                else if (booking.TotalPrice >= 4000 && booking.TotalPrice < 5000)
                {
                    loyaltyPoints = 5;
                }

                else if (booking.TotalPrice >= 5000 && booking.TotalPrice < 6000)
                {
                    loyaltyPoints = 6;
                }

                if (loyaltyPoints > 0)
                {
                    await UpdateUserLoyaltyPointsAsync(booking.UserId, loyaltyPoints);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving transaction: {ex.Message}");
            }
        }

        private async Task UpdateUserLoyaltyPointsAsync(string userId, int loyaltyPoints)
        {
            try
            {
                // Get user document
                var userRef = firestoreDb.Collection("users").Document(userId);
                var snapshot = await userRef.GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    var userData = snapshot.ToDictionary();
                    int currentPoints = userData.ContainsKey("loyaltyPoints") ? Convert.ToInt32(userData["loyaltyPoints"]) : 0;

                    // Update loyalty points
                    int updatedPoints = currentPoints + loyaltyPoints;
                    await userRef.UpdateAsync("loyaltyPoints", updatedPoints);

                    MessageBox.Show($"User's loyalty points updated to: {updatedPoints}", "loyalty Points", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User not found. Cannot update loyalty points.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating loyalty points: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SaveReminderAsync(Booking booking)
        {
            try
            {
                var reminderData = new Dictionary<string, object>
                {
                    { "referenceID", booking.ReferenceID },
                    { "userId", booking.UserId },
                    { "service", booking.Service },
                    { "product", booking.Product },
                    { "date", booking.Date },
                    { "time", booking.Time },
                    { "createdAt", booking.CreatedAt },
                    { "message", "You are not paid yet!"}
                };
                CollectionReference remindersRef = firestoreDb.Collection("reminders");
                await remindersRef.AddAsync(reminderData);
                MessageBox.Show($"Reminder set for {booking.UserId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting reminder: {ex.Message}");
            }
        }
        private async Task RefreshBookingsDataAsync()
        {
            List<Booking> bookings = await GetBookingsDataAsync();
            DisplayBookingsData(bookings);
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
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))),
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
        private custombtn CreateStyledButton2(string text, Point location)
        {
            custombtn button = new custombtn
            {
                Text = text,
                BackColor = System.Drawing.Color.FromArgb(0, 255, 0),
                BackgroundColor = System.Drawing.Color.FromArgb(255, 0, 0),
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
        private void DisplayCurrentDate()
        {
            lblcurrentdate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy h:mm tt");
        }
    }
}
