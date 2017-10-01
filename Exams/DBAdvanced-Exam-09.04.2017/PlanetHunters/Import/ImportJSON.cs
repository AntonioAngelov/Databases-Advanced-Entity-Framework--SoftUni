using Newtonsoft.Json;
using PlanetHunters.Data.DTOs;
using PlanetHunters.Data.Store;

namespace Import
{
    using System.Collections.Generic;
    using PlanetHunters.Models;

    using System.IO;

    public static class ImportJSON
    {
        public static void ImportAstronomers()
        {
            var json = File.ReadAllText("../../../datasets/astronomers.json");

            List<AstronomerDTO> astronomers = JsonConvert.DeserializeObject<List<AstronomerDTO>>(json);

            Store.StoreAstronomers(astronomers);
        }

        public static void ImportTelescopes()
        {
            var json = File.ReadAllText("../../../datasets/telescopes.json");

            List<TelescopeDTO> telescopes = JsonConvert.DeserializeObject<List<TelescopeDTO>>(json);

            Store.StoreTelescopes(telescopes);

        }

        public static void ImportPlannets()
        {
            var json = File.ReadAllText("../../../datasets/planets.json");

            List<PlanetDTO> planets = JsonConvert.DeserializeObject<List<PlanetDTO>>(json);

            Store.StorePlanets(planets);
        }

    }
    
}
