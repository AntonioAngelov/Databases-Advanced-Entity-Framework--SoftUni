using System.Collections.Generic;

namespace PlanetHunters.Data.DTOs
{
    public class ExportPlanetDTO
    {
        public ExportPlanetDTO()
        {
            this.Orbiting = new List<string>();
        }

        public string Name { get; set; }

        public decimal Mass { get; set; }

        public List<string> Orbiting { get; set; }

    }
}
