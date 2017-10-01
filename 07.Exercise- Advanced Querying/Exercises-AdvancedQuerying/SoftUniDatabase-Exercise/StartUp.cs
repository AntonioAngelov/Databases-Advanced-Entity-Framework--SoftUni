using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniDatabase_Exercise
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var context = new SoftUniDBContext();

            //exercise 3. Employees Maximum Salaries
            //MaxSalaryPerDepartment(context);


        }

        private static void MaxSalaryPerDepartment(SoftUniDBContext context)
        {
            context.Departments
                .Select(d => new
                {
                    Name = d.Name,
                    MaxSalary = d.Employees.Max(e => e.Salary)
                })
                .Where(d => d.MaxSalary < 30000 || d.MaxSalary > 70000)
                .ToList()
                .ForEach(d => Console.WriteLine($"{d.Name} - {d.MaxSalary}"));
        }
    }
}
