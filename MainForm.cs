using System;
using System.Windows.Forms;

namespace PayrollManagement
{
    public partial class MainForm : Form
    {
        private Button btnEmployee;
        private Button btnPayroll;
        private Button btnGenerate;
        private Button btnReport;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "PHẦN MỀM QUẢN LÝ LƯƠNG";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(700, 500);
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

            CreateUI();
        }

        private void CreateUI()
        {
            Panel pnlMain = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(20) };

            // Title
            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ LƯƠNG NHÂN VIÊN",
                Font = new System.Drawing.Font("Arial", 18, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.FromArgb(44, 62, 80),
                TextAlign = System.Windows.Forms.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 80
            };
            pnlMain.Controls.Add(lblTitle);

            // Buttons Panel
            Panel pnlButtons = new Panel() { Dock = DockStyle.Fill, Padding = new System.Windows.Forms.Padding(30) };

            btnEmployee = CreateButton("👥 Quản Lý Nhân Viên", 50);
            btnEmployee.Click += BtnEmployee_Click;

            btnPayroll = CreateButton("💰 Danh Sách Lương", 150);
            btnPayroll.Click += BtnPayroll_Click;

            btnGenerate = CreateButton("🧮 Tính Lương", 250);
            btnGenerate.Click += BtnGenerate_Click;

            btnReport = CreateButton("📊 Báo Cáo Lương", 350);
            btnReport.Click += BtnReport_Click;

            pnlButtons.Controls.Add(btnEmployee);
            pnlButtons.Controls.Add(btnPayroll);
            pnlButtons.Controls.Add(btnGenerate);
            pnlButtons.Controls.Add(btnReport);
            pnlMain.Controls.Add(pnlButtons);

            this.Controls.Add(pnlMain);
        }

        private Button CreateButton(string text, int yPosition)
        {
            Button btn = new Button()
            {
                Text = text,
                Location = new System.Drawing.Point(80, yPosition),
                Width = 540,
                Height = 80,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                BackColor = System.Drawing.Color.FromArgb(52, 152, 219),
                FlatStyle = FlatStyle.Flat,
                Cursor = System.Windows.Forms.Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void BtnEmployee_Click(object sender, EventArgs e)
        {
            EmployeeListForm form = new EmployeeListForm();
            form.ShowDialog();
        }

        private void BtnPayroll_Click(object sender, EventArgs e)
        {
            PayrollListForm form = new PayrollListForm();
            form.ShowDialog();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GeneratePayrollForm form = new GeneratePayrollForm();
            form.ShowDialog();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            PayrollReportForm form = new PayrollReportForm();
            form.ShowDialog();
        }
    }
}
