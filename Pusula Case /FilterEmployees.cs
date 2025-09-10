using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PusulaApp
{
    public class EmployeeStats
    {
        public List<string> Names { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal AverageSalary { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public int Count { get; set; }
    }

    public static class EmployeeFilterSolver
    {
        public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
        {
            var filtered = employees
                .Where(e => e.Age >= 25 && e.Age <= 40)
                .Where(e => e.Department == "IT" || e.Department == "Finance")
                .Where(e => e.Salary >= 5000m && e.Salary <= 9000m)
                .Where(e => e.HireDate > new DateTime(2017, 12, 31))
                .ToList();

            var names = filtered
                .OrderByDescending(e => e.Name.Length)
                .ThenBy(e => e.Name)
                .Select(e => e.Name)
                .ToList();

            decimal totalSalary = filtered.Sum(e => e.Salary);
            int count = filtered.Count;
            decimal averageSalary = count > 0 ? totalSalary / count : 0;
            decimal minSalary = count > 0 ? filtered.Min(e => e.Salary) : 0;
            decimal maxSalary = count > 0 ? filtered.Max(e => e.Salary) : 0;

            var result = new EmployeeStats
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = Math.Round(averageSalary, 2),
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
