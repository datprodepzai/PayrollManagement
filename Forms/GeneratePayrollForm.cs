using System;
using System.Windows.Forms;
using PayrollManagement.Models;
using PayrollManagement.Services;

namespace PayrollManagement
{
    public partial class GeneratePayrollForm : Form
    {
        private DataService _dataService;
        private ComboBox cboMonth;
        private ComboBox cboYear;
        private Button btnGenerate;

        public GeneratePayrollForm()
        {
            InitializeComponent();
            _dataService = new DataService();
        }

        private void GeneratePayrollForm_Load(object sender, EventArgs e)
        {
            this.Text = "Tính Lương Hàng Tháng";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(400, 300);

            CreateUI();
            LoadMonthYear();
        }

        private void CreateUI()
        {
            Panel pnlMain = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(20) };

            Label lblTitle = new Label() { Text = "TÍNH LƯƠNG", Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold), Dock = DockStyle.Top, Height = 50, TextAlign = System.Windows.Forms.ContentAlignment.MiddleCenter };
            pnlMain.Controls.Add(lblTitle);

            Panel pnlInput = new Panel() { Dock = DockStyle.Top, Height = 120, Padding = new System.Windows.Forms.Padding(10) };

            Label lblMonth = new Label() { Text = "Chọn Tháng:", Location = new System.Drawing.Point(10, 10), Width = 80 };
            cboMonth = new ComboBox() { Location = new System.Drawing.Point(100, 10), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblYear = new Label() { Text = "Chọn Năm:", Location = new System.Drawing.Point(10, 50), Width = 80 };
            cboYear = new ComboBox() { Location = new System.Drawing.Point(100, 50), Width = 120, DropDownStyle = ComboBoxStyle.DropDownList };

            pnlInput.Controls.Add(lblMonth);
            pnlInput.Controls.Add(cboMonth);
            pnlInput.Controls.Add(lblYear);
            pnlInput.Controls.Add(cboYear);
            pnlMain.Controls.Add(pnlInput);

            Panel pnlBtn = new Panel() { Dock = DockStyle.Bottom, Height = 60, Padding = new System.Windows.Forms.Padding(10) };
            btnGenerate = new Button() { Text = "Tính Lương", Location = new System.Drawing.Point(80, 10), Width = 120, Height = 40, BackColor = System.Drawing.Color.FromArgb(46, 204, 113), ForeColor = System.Drawing.Color.White, Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold) };
            btnGenerate.Click += BtnGenerate_Click;

            Button btnCancel = new Button() { Text = "Hủy", Location = new System.Drawing.Point(210, 10), Width = 100, Height = 40 };
            btnCancel.Click += (s, e) => this.Close();

            pnlBtn.Controls.Add(btnGenerate);
            pnlBtn.Controls.Add(btnCancel);
            pnlMain.Controls.Add(pnlBtn);

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

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int month = (int)cboMonth.SelectedItem;
                int year = (int)cboYear.SelectedItem;

                var employees = _dataService.GetAllEmployees();
                var existingPayrolls = _dataService.GetPayrollByMonth(month, year);

                if (existingPayrolls.Count > 0)
                {
                    MessageBox.Show($"Đã tính lương cho tháng {month}/{year} rồi!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (employees.Count == 0)
                {
                    MessageBox.Show("Chưa có nhân viên nào!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (var employee in employees)
                {
                    var payroll = new Payroll
                    {
                        EmployeeId = employee.Id,
                        EmployeeName = employee.FullName,
                        Month = month,
                        Year = year,
                        BaseSalary = employee.BaseSalary,
                        Allowance = employee.Allowance,
                        Deduction = employee.Deduction,
                        GrossSalary = employee.GetGrossSalary(),
                        NetSalary = employee.GetNetSalary(),
                        Status = "Pending",
                        CreatedDate = DateTime.Now,
                        Notes = ""
                    };
                    _dataService.AddPayroll(payroll);
                }

                MessageBox.Show($"✓ Đã tính lương cho {employees.Count} nhân viên thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
