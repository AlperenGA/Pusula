using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace PusulaApp
{
    public class PeopleStats
    {
        public List<string> Names { get; set; }
        public int TotalSalary { get; set; }
        public int AverageSalary { get; set; }
        public int MaxSalary { get; set; }
        public int Count { get; set; }
    }

    public static class FilterPeopleFromXmlSolver
    {
        public static string FilterPeopleFromXml(string xmlData)
        {
            var doc = XDocument.Parse(xmlData);
            var people = doc.Descendants("Person");

            var filtered = people
                .Select(p => new
                {
                    Name = p.Element("Name")?.Value,
                    Age = int.Parse(p.Element("Age")?.Value ?? "0"),
                    Department = p.Element("Department")?.Value,
                    Salary = int.Parse(p.Element("Salary")?.Value ?? "0"),
                    HireDate = DateTime.Parse(p.Element("HireDate")?.Value ?? "1900-01-01")
                })
                .Where(p => p.Age > 30
                         && p.Department == "IT"
                         && p.Salary > 5000
                         && p.HireDate < new DateTime(2019, 1, 1))
                .ToList();

            var names = filtered.Select(p => p.Name).OrderBy(n => n).ToList();
            int totalSalary = filtered.Sum(p => p.Salary);
            int count = filtered.Count;
            int avgSalary = count > 0 ? totalSalary / count : 0;
            int maxSalary = count > 0 ? filtered.Max(p => p.Salary) : 0;

            var result = new PeopleStats
            {
                Names = names,
                TotalSalary = totalSalary,
                AverageSalary = avgSalary,
                MaxSalary = maxSalary,
                Count = count
            };

            return JsonSerializer.Serialize(result);
        }
    }
}
