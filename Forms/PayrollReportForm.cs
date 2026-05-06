using System;
using System.Windows.Forms;
using PayrollManagement.Services;

namespace PayrollManagement
{
    public partial class PayrollReportForm : Form
    {
        private DataService _dataService;
        private ComboBox cboMonth;
        private ComboBox cboYear;
        private Label lblTotalEmployees;
        private Label lblTotalPayroll;
        private Label lblTotalGross;

        public PayrollReportForm()
        {
            InitializeComponent();
            _dataService = new DataService();
        }

        private void PayrollReportForm_Load(object sender, EventArgs e)
        {
            this.Text = "Báo Cáo Lương";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(500, 400);

            CreateUI();
            LoadMonthYear();
            LoadReport();
        }

        private void CreateUI()
        {
            Panel pnlMain = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(20) };

            Label lblTitle = new Label() { Text = "BÁO CÁO LƯƠNG", Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold), Dock = DockStyle.Top, Height = 40, TextAlign = System.Windows.Forms.ContentAlignment.MiddleCenter };
            pnlMain.Controls.Add(lblTitle);

            // Filter panel
            Panel pnlFilter = new Panel() { Dock = DockStyle.Top, Height = 60, Padding = new System.Windows.Forms.Padding(10) };
            Label lblMonth = new Label() { Text = "Tháng:", Location = new System.Drawing.Point(10, 15), Width = 50 };
            cboMonth = new ComboBox() { Location = new System.Drawing.Point(70, 15), Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };
            cboMonth.SelectedIndexChanged += (s, e) => LoadReport();

            Label lblYear = new Label() { Text = "Năm:", Location = new System.Drawing.Point(160, 15), Width = 50 };
            cboYear = new ComboBox() { Location = new System.Drawing.Point(220, 15), Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };
            cboYear.SelectedIndexChanged += (s, e) => LoadReport();

            pnlFilter.Controls.Add(lblMonth);
            pnlFilter.Controls.Add(cboMonth);
            pnlFilter.Controls.Add(lblYear);
            pnlFilter.Controls.Add(cboYear);
            pnlMain.Controls.Add(pnlFilter);

            // Report panel
            Panel pnlReport = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(20) };

            lblTotalEmployees = new Label() { Text = "Tổng nhân viên: 0", Font = new System.Drawing.Font("Arial", 12), Location = new System.Drawing.Point(10, 20), Width = 400, Height = 30 };
            pnlReport.Controls.Add(lblTotalEmployees);

            lblTotalGross = new Label() { Text = "Tổng lương tính: 0", Font = new System.Drawing.Font("Arial", 12), Location = new System.Drawing.Point(10, 60), Width = 400, Height = 30 };
            pnlReport.Controls.Add(lblTotalGross);

            lblTotalPayroll = new Label() { Text = "Tổng lương thực nhận: 0", Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold), ForeColor = System.Drawing.Color.FromArgb(46, 204, 113), Location = new System.Drawing.Point(10, 100), Width = 400, Height = 30 };
            pnlReport.Controls.Add(lblTotalPayroll);

            pnlMain.Controls.Add(pnlReport);
            this.Controls.Add(pnlMain);
        }

        private void LoadMonthYear()
        {
            for (int i = 1; i <= 12; i++)
                cboMonth.Items.Add(i);
            cboMonth.SelectedIndex = DateTime.Now.Month - 1;

            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 5; i++)
                cboYear.Items.Add(i);
            cboYear.SelectedIndex = 5;
        }

        private void LoadReport()
        {
            try
            {
                int month = (int)cboMonth.SelectedItem;
                int year = (int)cboYear.SelectedItem;

                var payrolls = _dataService.GetPayrollByMonth(month, year);
                decimal totalNetSalary = _dataService.GetTotalPayrollByMonth(month, year);
                decimal totalGrossSalary = 0;

                foreach (var p in payrolls)
                    totalGrossSalary += p.GrossSalary;

                lblTotalEmployees.Text = $"Tổng nhân viên: {payrolls.Count}";
                lblTotalGross.Text = $"Tổng lương tính: {totalGrossSalary:N0} VNĐ";
                lblTotalPayroll.Text = $"Tổng lương thực nhận: {totalNetSalary:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
