using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Projection.Models;
using Projection_Dto;

namespace Projection
{
    class Startup
    {
        static void Main(string[] args)
        {
            //InitializeDb();

            var context = new EmployeesContext();
            //SeedEmployees(context);

            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>());

            context.Employees
                .Where(e => e.BirthDay < new DateTime(1990, 1, 1))
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeDto>()
                .ToList()
                .ForEach(e => Console.WriteLine(e));
        }

        private static void SeedEmployees(EmployeesContext context)
        {
            string[] firstNames =
          {
                "Ivan",
                "Maria",
                "Dragan",
                "Petkan",
                "Divna"
            };

            string[] lastNames =
            {
                "Petrov",
                "Ivanova",
                "Petkanov",
                "Draganov",
                "Divneva"
            };

            DateTime[] birthDates =
            {
                new DateTime(1990, 1, 1),
                new DateTime(1989, 2, 2),
                new DateTime(1980, 3, 3),
                new DateTime(1996, 4, 4),
                new DateTime(1995, 5, 5)
            };


            string[] addresses =
            {
                "Solunska 31",
                "Opulchenska 1",
                "Botevgradsko shose 44",
                "Mirizliv Minzuhar 69",
                "Vkushti"
            };

            List<Employee> managers = new List<Employee>();

            for (int i = 0; i < 3; i++)
            {
                Employee manager = new Employee()
                {
                    FirstName = firstNames[0],
                    LastName = lastNames[i],
                    BirthDay = birthDates[0],
                    Salary = 100000,
                    Address = addresses[0],
                };

                managers.Add(manager);
            }

            context.Employees.AddRange(managers);
            context.SaveChanges();

            for (int h = 1; h <= 4; h++)
            {
                context.Employees.Add(new Employee()
                {
                    FirstName = firstNames[h],
                    LastName = lastNames[h],
                    BirthDay = birthDates[h],
                    Salary = h * 1000,
                    Address = addresses[h],
                    Manager = h < 4 ? context.Employees.Find(h) : context.Employees.Find(1)
                });
            }

            context.SaveChanges();
        }

        private static void InitializeDb()
        {
            EmployeesContext context = new EmployeesContext();
            context.Database.Initialize(true);
        }
    }
}
