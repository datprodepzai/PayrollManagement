using System;
using System.Windows.Forms;
using PayrollManagement.Models;
using PayrollManagement.Services;

namespace PayrollManagement
{
    public partial class PayrollListForm : Form
    {
        private DataService _dataService;
        private DataGridView dataGridView1;
        private ComboBox cboMonth;
        private ComboBox cboYear;
        private Button btnMarkPaid;

        public PayrollListForm()
        {
            InitializeComponent();
            _dataService = new DataService();
        }

        private void PayrollListForm_Load(object sender, EventArgs e)
        {
            this.Text = "Danh Sách Lương";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(1000, 600);

            CreateUI();
            LoadMonthYear();
            LoadData();
        }

        private void CreateUI()
        {
            Panel pnlMain = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };

            // Filter panel
            Panel pnlFilter = new Panel() { Dock = DockStyle.Top, Height = 60, Padding = new System.Windows.Forms.Padding(10) };
            Label lblMonth = new Label() { Text = "Tháng:", Location = new System.Drawing.Point(10, 15), Width = 50 };
            cboMonth = new ComboBox() { Location = new System.Drawing.Point(70, 15), Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };
            cboMonth.SelectedIndexChanged += (s, e) => LoadData();

            Label lblYear = new Label() { Text = "Năm:", Location = new System.Drawing.Point(160, 15), Width = 50 };
            cboYear = new ComboBox() { Location = new System.Drawing.Point(220, 15), Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };
            cboYear.SelectedIndexChanged += (s, e) => LoadData();

            pnlFilter.Controls.Add(lblMonth);
            pnlFilter.Controls.Add(cboMonth);
            pnlFilter.Controls.Add(lblYear);
            pnlFilter.Controls.Add(cboYear);
            pnlMain.Controls.Add(pnlFilter);

            // DataGridView
            dataGridView1 = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                AllowUserToAddRows = false,
                ReadOnly = true,
                MultiSelect = false
            };
            pnlMain.Controls.Add(dataGridView1);

            // Button panel
            Panel pnlBtn = new Panel() { Dock = DockStyle.Bottom, Height = 50, Padding = new System.Windows.Forms.Padding(10) };
            btnMarkPaid = new Button() { Text = "Đánh Dấu Đã Trả", Location = new System.Drawing.Point(10, 10), Width = 120, Height = 30, BackColor = System.Drawing.Color.FromArgb(52, 152, 219), ForeColor = System.Drawing.Color.White };
            btnMarkPaid.Click += BtnMarkPaid_Click;
            pnlBtn.Controls.Add(btnMarkPaid);
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

        private void LoadData()
        {
            try
            {
                int month = (int)cboMonth.SelectedItem;
                int year = (int)cboYear.SelectedItem;
                var payrolls = _dataService.GetPayrollByMonth(month, year);
                dataGridView1.DataSource = payrolls;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void BtnMarkPaid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn bản lương!");
                    return;
                }

                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                var payrolls = _dataService.GetAllPayrolls();
                var payroll = payrolls.Find(p => p.Id == id);

                if (payroll != null)
                {
                    payroll.Status = "Paid";
                    payroll.PaidDate = DateTime.Now;
                    _dataService.UpdatePayroll(payroll);
                    LoadData();
                    MessageBox.Show("✓ Đã cập nhật trạng thái!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
