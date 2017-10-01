using System;
using System.Collections.Generic;
using System.Text;
using AdvanceMapping.Models;

namespace AdvanceMapping.Models_Dto
{
    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Employee> Subordinates { get; set; }

        public int SubordinatesCount { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{FirstName} {LastName} | Employees: {SubordinatesCount}");
            foreach (var employee in Subordinates)
            {
                sb.AppendLine($"     - {employee.FirstName} {employee.LastName} {employee.Salary}");
            }

            return sb.ToString().Trim();
        }
    }
}
