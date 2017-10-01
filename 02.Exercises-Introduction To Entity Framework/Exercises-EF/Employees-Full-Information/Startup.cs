namespace Employees_Full_Information
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TaskExtensions = System.Data.Entity.SqlServer.Utilities.TaskExtensions;


    class Startup
    {
        static void Main(string[] args)
        {
            //exercise 3 Employees Full Information
            //EmployeesFullInfo();

            //exercise 4 Employees with Salary Over 50 000
            //EmployeesWithBigSalary();

            //exercise 5 Employees from Research and Development
            //EmployeesFromDepartment();

            //exercise 6. Adding a New Address and Updating Employee
            //CreateAddressAndUpdateEmployee();

            //exercise 7. Find Employees in Period
            //EmployeesInPeriod();

            //exercise 8 Addresses by Town Name
            //GetTowns();

            //exercise 9 Employee with id 147
            //GetEmployeWithSpecificId();

            //exercise 10 Departments with more than 5 employees
            //DepartmentsWithMoreThah5Employees();

            //exercise 11 Find Latest 10 Projects
            //GetLast10Projects();

            //exercise 12 Increase Salaries
            //IncreaseSalaries();

            //exercise 13 Find Employees by First Name Starting with ‘SA’
            //GetEmployeesByFirstName();

            //15. Delete Project by Id
            DeleteProjectById();

        }

        private static void DeleteProjectById()
        {
            SoftUniContext context = new SoftUniContext();

            var project = context.Projects.Find(2);

            foreach (var e in project.Employees)
            {
                e.Projects.Remove(project);
            }

            context.Projects.Remove(project);
            context.SaveChanges();

            var projects = context.Projects.Take(10).ToList();

            foreach (var P in projects)
            {
                Console.WriteLine(P.Name);
            }
        }

        private static void GetEmployeesByFirstName()
        {
            SoftUniContext context = new SoftUniContext();

            var employees = context.Employees
                .Where(e => e.FirstName.ToUpper().Substring(0, 2) == "SA")
                .ToList();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} - {e.JobTitle} - (${e.Salary})");
            }
        }

        private static void IncreaseSalaries()
        {
            SoftUniContext context = new SoftUniContext();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering"
                            || e.Department.Name == "Tool Design"
                            || e.Department.Name == "Marketing"
                            || e.Department.Name == "Information Services");

            foreach (var e in employees)
            {
                e.Salary += e.Salary * (decimal)0.12;
            }

            context.SaveChanges();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} (${e.Salary})");
            }
        }

        private static void GetLast10Projects()
        {
            SoftUniContext context = new SoftUniContext();

            var lastProjects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var p in lastProjects)
            {
                var endDate = p.EndDate != null
                    ? p.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                    : "";
                Console.WriteLine($"{p.Name} {p.Description} {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)}" + endDate);
            }
        }

        private static void DepartmentsWithMoreThah5Employees()
        {
            SoftUniContext context = new SoftUniContext();

            var departments = context
                .Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ToList();

            foreach (var d in departments)
            {
                var manager = context.Employees.Find(d.ManagerID);
                Console.WriteLine($"{d.Name} {manager.FirstName}");

                foreach (var e in d.Employees)
                {
                    Console.WriteLine($"{e.FirstName} {e.LastName} {e.JobTitle}");
                }
            }
        }

        private static void GetEmployeWithSpecificId()
        {
            SoftUniContext context = new SoftUniContext();

            var employee = context
                .Employees
                .Find(147);

            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");

            var projects = employee.Projects
                .OrderBy(p => p.Name)
                .Select(p => p.Name)
                .ToList();

            foreach (var p in projects)
            {
              Console.WriteLine(p);  
            }
        }

        private static void GetTowns()
        {
            SoftUniContext context = new SoftUniContext();

            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Take(10).ToList();

            foreach (var a in addresses)
            {
                Console.WriteLine($"{a.AddressText}, {a.Town.Name} - {a.Employees.Count} employees");
            }

        }

        private static void EmployeesInPeriod()
        {
            SoftUniContext context = new SoftUniContext();

            var employess = context.Employees
                .Where(e => e.Projects
                    .Any(p => DateTime.Compare(p.StartDate, new DateTime(2001, 1, 1)) >= 0
                              & DateTime.Compare(p.StartDate, new DateTime(2003, 12, 31)) < 0))
                .Take(30).ToList();

            foreach (var e in employess)
            {
               Console.WriteLine($"{e.FirstName} {e.LastName} {e.Manager.FirstName}");
                foreach (var p in e.Projects)
                {
                    var endDate = p.EndDate != null
                   ? p.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)
                   : "";
                    Console.WriteLine($"--{p.Name} {p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture)} " + endDate);
                } 
            }
        }

        private static void CreateAddressAndUpdateEmployee()
        {
           SoftUniContext context = new SoftUniContext();

            Address newAddress = new Address()
            {
              AddressText = "Vitoshka 15",
              TownID = 4
            };

            var nakov = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            nakov.Address = newAddress;
            context.SaveChanges();

            var employees = context
                .Employees.OrderByDescending(e => e.AddressID)
                .Take(10)
                .ToList();

            foreach (var e in employees)
            {
                Console.WriteLine(e.Address.AddressText);
            }
        }

        private static void EmployeesFromDepartment()
        {
            SoftUniContext context = new SoftUniContext();

            var employees = context.Employees
                            .Where(e => e.Department.Name == "Research and Development")
                            .Select(e => new
                              {
                               e.FirstName,
                               e.LastName,
                               e.Department.Name,
                               e.Salary
                              })
                            .OrderBy(e => e.Salary)
                            .ThenByDescending(e => e.FirstName)
                            .ToList();

            foreach (var e in employees)
            {
              Console.WriteLine($"{e.FirstName} {e.LastName} from {e.Name} - ${e.Salary:F2}");  
            }
        }

        private static void EmployeesWithBigSalary()
        {
            var context = new SoftUniContext();

            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => e.FirstName)
                .ToList();

            foreach (var e in employees)
            {
                Console.WriteLine(e);
            }
        }

        private static void EmployeesFullInfo()
        {
            var context = new SoftUniContext();

            var employees = context.Employees
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                }).ToList();

            foreach (var e in employees)
            {
                Console.WriteLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary}");
            }
        }
    }
}
