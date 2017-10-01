using System;
using System.Collections.Generic;

namespace PlanetHunters.Data.DTOs
{
    public class ExportStarDTO
    {
        public ExportStarDTO()
        {
            this.Astronomes= new List<string>();
        }

        public string Name { get; set; }

        public int Temperature { get; set; }

        public string StarSystemName { get; set; }

        public DateTime? DiscoveryDate { get; set; }

        public string TelescopeName { get; set; }

        public List<string> Astronomes { get; set; }

    }
}
