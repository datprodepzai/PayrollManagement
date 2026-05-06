using System;
using System.Windows.Forms;
using PayrollManagement.Models;
using PayrollManagement.Services;

namespace PayrollManagement
{
    public partial class EmployeeForm : Form
    {
        private DataService _dataService;
        private Employee _employee;
        private bool _isNewEmployee;
        private TextBox txtFullName;
        private TextBox txtPosition;
        private TextBox txtDepartment;
        private NumericUpDown numBaseSalary;
        private NumericUpDown numAllowance;
        private NumericUpDown numDeduction;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private DateTimePicker dateJoin;
        private Button btnSave;
        private Button btnCancel;

        public EmployeeForm(Employee employee, DataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
            _employee = employee;
            _isNewEmployee = (employee == null);
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            this.Text = _isNewEmployee ? "Thêm Nhân Viên" : "Sửa Nhân Viên";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Size = new System.Drawing.Size(450, 550);

            CreateUI();

            if (!_isNewEmployee)
            {
                txtFullName.Text = _employee.FullName;
                txtPosition.Text = _employee.Position;
                txtDepartment.Text = _employee.Department;
                numBaseSalary.Value = (decimal)_employee.BaseSalary;
                numAllowance.Value = (decimal)_employee.Allowance;
                numDeduction.Value = (decimal)_employee.Deduction;
                txtPhone.Text = _employee.PhoneNumber;
                txtEmail.Text = _employee.Email;
                dateJoin.Value = _employee.JoinDate;
            }
        }

        private void CreateUI()
        {
            Panel pnlMain = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(15) };
            pnlMain.AutoScroll = true;

            int yPos = 10;

            // Full Name
            Label lblFullName = new Label() { Text = "Họ & Tên:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            txtFullName = new TextBox() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25 };
            pnlMain.Controls.Add(lblFullName);
            pnlMain.Controls.Add(txtFullName);
            yPos += 40;

            // Position
            Label lblPosition = new Label() { Text = "Vị Trí:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            txtPosition = new TextBox() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25 };
            pnlMain.Controls.Add(lblPosition);
            pnlMain.Controls.Add(txtPosition);
            yPos += 40;

            // Department
            Label lblDepartment = new Label() { Text = "Phòng Ban:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            txtDepartment = new TextBox() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25 };
            pnlMain.Controls.Add(lblDepartment);
            pnlMain.Controls.Add(txtDepartment);
            yPos += 40;

            // Base Salary
            Label lblBaseSalary = new Label() { Text = "Lương Cơ Bản:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            numBaseSalary = new NumericUpDown() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25, DecimalPlaces = 0, Maximum = 999999999 };
            pnlMain.Controls.Add(lblBaseSalary);
            pnlMain.Controls.Add(numBaseSalary);
            yPos += 40;

            // Allowance
            Label lblAllowance = new Label() { Text = "Phụ Cấp:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            numAllowance = new NumericUpDown() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25, DecimalPlaces = 0, Maximum = 999999999 };
            pnlMain.Controls.Add(lblAllowance);
            pnlMain.Controls.Add(numAllowance);
            yPos += 40;

            // Deduction
            Label lblDeduction = new Label() { Text = "Khấu Trừ:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            numDeduction = new NumericUpDown() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25, DecimalPlaces = 0, Maximum = 999999999 };
            pnlMain.Controls.Add(lblDeduction);
            pnlMain.Controls.Add(numDeduction);
            yPos += 40;

            // Phone
            Label lblPhone = new Label() { Text = "SĐT:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            txtPhone = new TextBox() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25 };
            pnlMain.Controls.Add(lblPhone);
            pnlMain.Controls.Add(txtPhone);
            yPos += 40;

            // Email
            Label lblEmail = new Label() { Text = "Email:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            txtEmail = new TextBox() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25 };
            pnlMain.Controls.Add(lblEmail);
            pnlMain.Controls.Add(txtEmail);
            yPos += 40;

            // Join Date
            Label lblJoin = new Label() { Text = "Ngày Vào:", Location = new System.Drawing.Point(10, yPos), Width = 100 };
            dateJoin = new DateTimePicker() { Location = new System.Drawing.Point(120, yPos), Width = 300, Height = 25 };
            pnlMain.Controls.Add(lblJoin);
            pnlMain.Controls.Add(dateJoin);
            yPos += 50;

            // Buttons
            Panel pnlBtn = new Panel() { Location = new System.Drawing.Point(0, yPos + 20), Width = 420, Height = 60 };
            btnSave = new Button() { Text = "Lưu", Location = new System.Drawing.Point(100, 10), Width = 100, Height = 40, BackColor = System.Drawing.Color.FromArgb(46, 204, 113), ForeColor = System.Drawing.Color.White, Font = new System.Drawing.Font("Arial", 11, System.Drawing.FontStyle.Bold) };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button() { Text = "Hủy", Location = new System.Drawing.Point(220, 10), Width = 100, Height = 40 };
            btnCancel.Click += (s, e) => this.Close();

            pnlBtn.Controls.Add(btnSave);
            pnlBtn.Controls.Add(btnCancel);
            pnlMain.Controls.Add(pnlBtn);

            this.Controls.Add(pnlMain);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên nhân viên!");
                    return;
                }

                if (_isNewEmployee)
                    _employee = new Employee();

                _employee.FullName = txtFullName.Text;
                _employee.Position = txtPosition.Text;
                _employee.Department = txtDepartment.Text;
                _employee.BaseSalary = numBaseSalary.Value;
                _employee.Allowance = numAllowance.Value;
                _employee.Deduction = numDeduction.Value;
                _employee.PhoneNumber = txtPhone.Text;
                _employee.Email = txtEmail.Text;
                _employee.JoinDate = dateJoin.Value;

                if (_isNewEmployee)
                    _dataService.AddEmployee(_employee);
                else
                    _dataService.UpdateEmployee(_employee);

                MessageBox.Show("✓ Lưu thành công!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}
