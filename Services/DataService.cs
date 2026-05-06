using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PayrollManagement.Models;

namespace PayrollManagement.Services
{
    public class DataService
    {
        private readonly string _dataFolder;
        private readonly string _employeesPath;
        private readonly string _payrollPath;

        public DataService()
        {
            _dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            _employeesPath = Path.Combine(_dataFolder, "employees.json");
            _payrollPath = Path.Combine(_dataFolder, "payroll.json");

            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);
        }

        // ========== EMPLOYEE METHODS ==========
        public List<Employee> GetAllEmployees()
        {
            try
            {
                if (!File.Exists(_employeesPath))
                    return new List<Employee>();

                string json = File.ReadAllText(_employeesPath);
                return JsonConvert.DeserializeObject<List<Employee>>(json) ?? new List<Employee>();
            }
            catch
            {
                return new List<Employee>();
            }
        }

        public Employee GetEmployeeById(int id)
        {
            var employees = GetAllEmployees();
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public void AddEmployee(Employee employee)
        {
            var employees = GetAllEmployees();
            employee.Id = employees.Count > 0 ? employees.Max(e => e.Id) + 1 : 1;
            employee.CreatedDate = DateTime.Now;
            employees.Add(employee);
            SaveEmployees(employees);
        }

        public void UpdateEmployee(Employee employee)
        {
            var employees = GetAllEmployees();
            var existing = employees.FirstOrDefault(e => e.Id == employee.Id);
            if (existing != null)
            {
                int index = employees.IndexOf(existing);
                employee.CreatedDate = existing.CreatedDate;
                employees[index] = employee;
                SaveEmployees(employees);
            }
        }

        public void DeleteEmployee(int id)
        {
            var employees = GetAllEmployees();
            employees.RemoveAll(e => e.Id == id);
            SaveEmployees(employees);

            // Xóa payroll liên quan
            var payrolls = GetAllPayrolls();
            payrolls.RemoveAll(p => p.EmployeeId == id);
            SavePayrolls(payrolls);
        }

        private void SaveEmployees(List<Employee> employees)
        {
            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);
            File.WriteAllText(_employeesPath, json);
        }

        // ========== PAYROLL METHODS ==========
        public List<Payroll> GetAllPayrolls()
        {
            try
            {
                if (!File.Exists(_payrollPath))
                    return new List<Payroll>();

                string json = File.ReadAllText(_payrollPath);
                return JsonConvert.DeserializeObject<List<Payroll>>(json) ?? new List<Payroll>();
            }
            catch
            {
                return new List<Payroll>();
            }
        }

        public List<Payroll> GetPayrollByMonth(int month, int year)
        {
            var payrolls = GetAllPayrolls();
            return payrolls.Where(p => p.Month == month && p.Year == year).ToList();
        }

        public List<Payroll> GetPayrollByEmployee(int employeeId)
        {
            var payrolls = GetAllPayrolls();
            return payrolls.Where(p => p.EmployeeId == employeeId).ToList();
        }

        public void AddPayroll(Payroll payroll)
        {
            var payrolls = GetAllPayrolls();
            payroll.Id = payrolls.Count > 0 ? payrolls.Max(p => p.Id) + 1 : 1;
            payroll.CreatedDate = DateTime.Now;
            payrolls.Add(payroll);
            SavePayrolls(payrolls);
        }

        public void UpdatePayroll(Payroll payroll)
        {
            var payrolls = GetAllPayrolls();
            var existing = payrolls.FirstOrDefault(p => p.Id == payroll.Id);
            if (existing != null)
            {
                int index = payrolls.IndexOf(existing);
                payrolls[index] = payroll;
                SavePayrolls(payrolls);
            }
        }

        public void DeletePayroll(int id)
        {
            var payrolls = GetAllPayrolls();
            payrolls.RemoveAll(p => p.Id == id);
            SavePayrolls(payrolls);
        }

        private void SavePayrolls(List<Payroll> payrolls)
        {
            string json = JsonConvert.SerializeObject(payrolls, Formatting.Indented);
            File.WriteAllText(_payrollPath, json);
        }

        // ========== STATISTICS ==========
        public decimal GetTotalPayrollByMonth(int month, int year)
        {
            var payrolls = GetPayrollByMonth(month, year);
            return payrolls.Sum(p => p.NetSalary);
        }

        public decimal GetTotalPayroll()
        {
            var payrolls = GetAllPayrolls();
            return payrolls.Sum(p => p.NetSalary);
        }
    }
}
