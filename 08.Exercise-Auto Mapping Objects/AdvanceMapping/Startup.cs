namespace AdvanceMapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AdvanceMapping.Models;
    using AutoMapper;
    using Models_Dto;

    class Startup
    {
        static void Main(string[] args)
        {
            //exercise 2
            GetManagers();

        }

        private static void GetManagers()
        {
            ConfigAutoMapping();

            List<Employee> managers = CreateEmployess.Create();

            var managerDtos = Mapper.Map<List<Employee>, List<ManagerDto>>(managers);

            foreach (var manager in managerDtos)
            {
                Console.WriteLine(manager);
            }
        }

        public static void ConfigAutoMapping()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, ManagerDto>()
             .ForMember(dto => dto.SubordinatesCount, opt => opt.MapFrom(src => src.Subordinates.Count)));
        }
    }
}
