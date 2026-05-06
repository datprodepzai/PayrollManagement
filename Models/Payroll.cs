using System;

namespace PayrollManagement.Models
{
    public class Payroll
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        public string Status { get; set; } // Pending, Paid
        public DateTime CreatedDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string Notes { get; set; }
    }
}
