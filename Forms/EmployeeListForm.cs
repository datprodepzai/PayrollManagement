using System;
using System.Windows.Forms;
using PayrollManagement.Models;
using PayrollManagement.Services;

namespace PayrollManagement
{
    public partial class EmployeeListForm : Form
    {
        private DataService _dataService;
        private DataGridView dataGridView1;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;

        public EmployeeListForm()
        {
            InitializeComponent();
            _dataService = new DataService();
        }

        private void EmployeeListForm_Load(object sender, EventArgs e)
        {
            this.Text = "Danh Sách Nhân Viên";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(1000, 600);

            CreateUI();
            LoadData();
        }

        private void CreateUI()
        {
            Panel pnlMain = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(10) };

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
            Panel pnlBtn = new Panel() { Dock = DockStyle.Bottom, Height = 60, Padding = new System.Windows.Forms.Padding(10) };

            btnAdd = new Button() { Text = "➕ Thêm", Location = new System.Drawing.Point(10, 10), Width = 100, Height = 40, BackColor = System.Drawing.Color.FromArgb(46, 204, 113), ForeColor = System.Drawing.Color.White, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold) };
            btnAdd.Click += BtnAdd_Click;

            btnEdit = new Button() { Text = "✎ Sửa", Location = new System.Drawing.Point(120, 10), Width = 100, Height = 40, BackColor = System.Drawing.Color.FromArgb(52, 152, 219), ForeColor = System.Drawing.Color.White, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold) };
            btnEdit.Click += BtnEdit_Click;

            btnDelete = new Button() { Text = "✕ Xóa", Location = new System.Drawing.Point(230, 10), Width = 100, Height = 40, BackColor = System.Drawing.Color.FromArgb(231, 76, 60), ForeColor = System.Drawing.Color.White, Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold) };
            btnDelete.Click += BtnDelete_Click;

            pnlBtn.Controls.Add(btnAdd);
            pnlBtn.Controls.Add(btnEdit);
            pnlBtn.Controls.Add(btnDelete);
            pnlMain.Controls.Add(pnlBtn);

            this.Controls.Add(pnlMain);
        }

        private void LoadData()
        {
            try
            {
                var employees = _dataService.GetAllEmployees();
                dataGridView1.DataSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            EmployeeForm form = new EmployeeForm(null, _dataService);
            if (form.ShowDialog() == DialogResult.OK)
                LoadData();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để sửa!");
                    return;
                }

                int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                var employee = _dataService.GetEmployeeById(id);
                EmployeeForm form = new EmployeeForm(employee, _dataService);
                if (form.ShowDialog() == DialogResult.OK)
                    LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên để xóa!");
                    return;
                }

                if (MessageBox.Show("Bạn chắc chắn muốn xóa nhân viên này?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
                    _dataService.DeleteEmployee(id);
                    LoadData();
                    MessageBox.Show("✓ Đã xóa thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
