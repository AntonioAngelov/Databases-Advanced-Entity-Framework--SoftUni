namespace AdvanceMapping
{
    using System.Collections.Generic;
    using AdvanceMapping.Models;
    using System;

    public class CreateEmployess
    {
        public static List<Employee> Create()
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
                    IsOnHoliday = false,
                    Subordinates = new List<Employee>(),
                    Address = addresses[0],
                };

                for (int h = 1; h <= 4; h++)
                {
                    var currentEmployee = new Employee()
                    {
                        FirstName = firstNames[h],
                        LastName = lastNames[h],
                        BirthDay = birthDates[h],
                        Salary = h * 1000,
                        IsOnHoliday = h % 2 == 0 ? true : false,
                        Address = addresses[h],
                        Manager = manager
                    };

                    manager.Subordinates.Add(currentEmployee);

                }

                managers.Add(manager);
            }


            return managers;
        }
    }
}
