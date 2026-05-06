using System;

namespace PayrollManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public decimal GetGrossSalary()
        {
            return BaseSalary + Allowance;
        }

        public decimal GetNetSalary()
        {
            return GetGrossSalary() - Deduction;
        }

        public override string ToString()
        {
            return $"{Id} - {FullName} ({Position})";
        }
    }
}
