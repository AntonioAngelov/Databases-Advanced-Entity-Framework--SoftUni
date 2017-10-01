namespace Export
{
    using System.IO;
    using Newtonsoft.Json;
    using PlanetHunters.Data.Queries;

    public class ExportJSON
    {
        public static void EportPlanets(string telescopeName)
        {
            var planets = Query.GetPlanetsByTelescope(telescopeName);

            var planetsJson = JsonConvert.SerializeObject(planets, Formatting.Indented);

            File.WriteAllText($"../../../exports/planets-by-{telescopeName}.json", planetsJson);

        }

        public static void ExportAstronomers(string starSystemName)
        {
            var astronomers = Query.GetAstronomersByStarSystem(starSystemName);

            var astronomersJson = JsonConvert.SerializeObject(astronomers, Formatting.Indented);

            File.WriteAllText($"../../../exports/astronomer-of-{starSystemName}.json", astronomersJson);
        }
    }
}
