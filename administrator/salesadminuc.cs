using Google.Cloud.Firestore;
using Firebase.Database;
using Firebase.Database.Query;
using System.Globalization;
namespace VeterinaryClinicApp.administrator
{
    public partial class salesadminuc : UserControl
    {
        private FirestoreDb firestoreDb;
        public salesadminuc()
        {
            InitializeComponent();
            InitializeFirestore();
        }
        public void InitializeFirestore()
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"D:\capstone2\codings\netcore\VeterinaryClinicApp\veterinaryclinic-60d24-firebase-adminsdk-ptgn7-ab06babd4f.json");
            firestoreDb = FirestoreDb.Create("veterinaryclinic-60d24");
        }
        private async void salesadminuc_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            var sales = await GetSalesDataAsync();
            PopulateMonthsComboBox(sales);
            dgvSales.DataSource = sales;
        }
        private void InitializeDataGridView()
        {
            dgvSales.AutoGenerateColumns = true;
            dgvSales.Columns.Clear();
            dgvSales.DataSource = null;
        }
        private async Task<List<Sale>> GetSalesDataAsync()
        {
            var salesQuery = firestoreDb.Collection("sales").GetSnapshotAsync();
            var snapshot = await salesQuery;
            var sale = snapshot.Documents.Select(doc =>
            {
                return new Sale
                {
                    Date = doc.ContainsField("date") ? DateTime.Parse(doc.GetValue<string>("date")).ToString("yyyy-MM-dd") : "Unknown", 
                    Product = doc.ContainsField("product") ? doc.GetValue<string>("product") : "Unknown",
                    Service = doc.ContainsField("service") ? doc.GetValue<string>("service") : "Unknown",
                    TotalPrice = doc.ContainsField("totalPrice") ? doc.GetValue<int>("totalPrice") : 0,  
                };
            }).ToList();
            return sale;
        }
        private void PopulateMonthsComboBox(List<Sale> sales)
        {
            var months = sales
        .Select(s => DateTime.ParseExact(s.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture)) 
        .Select(d => d.ToString("MMMM yyyy"))  // Format to "Month Year"
        .Distinct()
        .OrderBy(m => DateTime.ParseExact(m, "MMMM yyyy", CultureInfo.InvariantCulture)) 
        .ToList();
            cmbMonths.Items.Clear();
            cmbMonths.Items.AddRange(months.ToArray());
        }
        private void FilterSalesByMonth(List<Sale> sales)
        {
            if (cmbMonths.SelectedItem == null)
                return;
            string selectedMonth = cmbMonths.SelectedItem.ToString();
            var filteredSales = sales
                .Where(s => DateTime.ParseExact(s.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMMM yyyy") == selectedMonth)
                .ToList();
            dgvSales.DataSource = filteredSales;
        }
        private async void cmbMonths_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonths.SelectedItem != null)
            {
                var sales = await GetSalesDataAsync();
                string selectedMonth = cmbMonths.SelectedItem.ToString();
                var filteredSales = sales
                    .Where(s => DateTime.ParseExact(s.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMMM yyyy") == selectedMonth)
                    .ToList();
                int totalSales = filteredSales.Sum(s => s.TotalPrice);
                filteredSales.Add(new Sale
                {
                    Product = "Total Sales",
                    Service = "",
                    TotalPrice = totalSales,
                    Date = ""
                });
                dgvSales.DataSource = null;
                dgvSales.DataSource = filteredSales;
                dgvSales.Refresh();
            }
        }
    }
}
