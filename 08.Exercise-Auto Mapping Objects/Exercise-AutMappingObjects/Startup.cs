namespace Exercise_AutMappingObjects
{
    using Models;
    using System;
    using AutoMapper;

    class Startup
    {
        static void Main()
        {

                        Employee employee = new Employee()
            {
                FirstName = "Ivan",
                LastName = "Petrov",
                Salary = 2000,
                BirthDay = new DateTime(1990, 1, 1),
                Address = "Na sedmoto nebe"
            };

            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>());

            var employeeDto = Mapper.Map<EmployeeDto>(employee);
            Console.WriteLine($"Name: {employeeDto.FirstName} {employeeDto.LastName} \nSalary: {employeeDto.Salary}");

        }
    }
}
