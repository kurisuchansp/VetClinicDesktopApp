using System.IO.Ports;
using VeterinaryClinicApp.administrator;
namespace VeterinaryClinicApp
{
    public partial class admindashuc : UserControl
    {
        private SerialPort rfidSerialPort;
        private Button _selectedButton;
        private readonly Color DefaultColor = Color.FromArgb(0, 61, 83);
        private readonly Color SelectedColor = Color.FromArgb(0, 100, 150);
        public admindashuc(string fullname, string email, string rfid)
        {
            InitializeComponent();
            lbladname.Text = $"{fullname}";
            lblademail.Text = $"{email}";
        }
        private void fmbtnad_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (_selectedButton != null)
                {
                    _selectedButton.BackColor = DefaultColor;
                }
                _selectedButton = clickedButton;
                _selectedButton.BackColor = SelectedColor;
            }
            ShowPatientsInfo();
        }
        private void bkbtnad_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (_selectedButton != null)
                {
                    _selectedButton.BackColor = DefaultColor;
                }
                _selectedButton = clickedButton;
                _selectedButton.BackColor = SelectedColor;
            }
            ShowBooking();
        }
        private void offersbtnad_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (_selectedButton != null)
                {
                    _selectedButton.BackColor = DefaultColor;
                }
                _selectedButton = clickedButton;
                _selectedButton.BackColor = SelectedColor;
            }
            ShowOffers();
        }
        private void transbtnad_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (_selectedButton != null)
                {
                    _selectedButton.BackColor = DefaultColor;
                }
                _selectedButton = clickedButton;
                _selectedButton.BackColor = SelectedColor;
            }
            ShowTransaction();
        }
        private void usersbtnad_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (_selectedButton != null)
                {
                    _selectedButton.BackColor = DefaultColor;
                }
                _selectedButton = clickedButton;
                _selectedButton.BackColor = SelectedColor;
            }
            ShowUsers();
        }
        private void staffsbtnad_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (_selectedButton != null)
                {
                    _selectedButton.BackColor = DefaultColor;
                }
                _selectedButton = clickedButton;
                _selectedButton.BackColor = SelectedColor;
            }
            ShowStaffs();
        }
        private void salesbtnad_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (_selectedButton != null)
                {
                    _selectedButton.BackColor = DefaultColor;
                }
                _selectedButton = clickedButton;
                _selectedButton.BackColor = SelectedColor;
            }
            ShowSales();
        }
        private void settingsbtnad_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (rfidSerialPort != null && rfidSerialPort.IsOpen)
                {
                    rfidSerialPort.Close();
                    rfidSerialPort.Dispose();
                }
                Application.Restart();
            }
        }
        private void ShowPatientsInfo()
        {
            panelmaindb.Controls.Clear();
            patientsinfouc patientsinfouc = new patientsinfouc();
            patientsinfouc.Dock = DockStyle.Fill;
            panelmaindb.Controls.Add(patientsinfouc);
            patientsinfouc.Show();
        }
        private void ShowBooking()
        {
            panelmaindb.Controls.Clear();
            bookingadminuc bookingadminuc = new bookingadminuc();
            bookingadminuc.Dock = DockStyle.Fill;
            panelmaindb.Controls.Add(bookingadminuc);
            bookingadminuc.Show();
        }
        private void ShowOffers()
        {
            panelmaindb.Controls.Clear();
            offersadminuc offersadminuc = new offersadminuc();
            offersadminuc.Dock = DockStyle.Fill;
            panelmaindb.Controls.Add(offersadminuc);
            offersadminuc.Show();
        }
        private void ShowTransaction()
        {
            panelmaindb.Controls.Clear();
            transadminuc transadminuc = new transadminuc();
            transadminuc.Dock = DockStyle.Fill;
            panelmaindb.Controls.Add(transadminuc);
            transadminuc.Show();
        }
        private void ShowUsers()
        {
            panelmaindb.Controls.Clear();
            usersadminuc usersadminuc = new usersadminuc();
            usersadminuc.Dock = DockStyle.Fill;
            panelmaindb.Controls.Add(usersadminuc);
            usersadminuc.Show();
        }
        private void ShowStaffs()
        {
            panelmaindb.Controls.Clear();
            staffsadminuc staffsadminuc = new staffsadminuc();
            staffsadminuc.Dock = DockStyle.Fill;
            panelmaindb.Controls.Add(staffsadminuc);
            staffsadminuc.Show();
        }
        private void ShowSales()
        {
            panelmaindb.Controls.Clear();
            salesadminuc salesadminuc = new salesadminuc();
            salesadminuc.Dock = DockStyle.Fill;
            panelmaindb.Controls.Add(salesadminuc);
            salesadminuc.Show();
        }  
    }
}
