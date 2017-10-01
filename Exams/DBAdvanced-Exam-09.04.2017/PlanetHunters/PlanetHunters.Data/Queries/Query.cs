using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using PlanetHunters.Data.DTOs;

namespace PlanetHunters.Data.Queries
{
    public class Query
    {
        public static List<ExportPlanetDTO> GetPlanetsByTelescope(string telescopeName)
        {
            using (var context = new PlanetHuntersContext())
            {
                var telescopeEntity = context.Telescopes.Where(t => t.Name == telescopeName).FirstOrDefault();

                var discoveries = telescopeEntity.Discoveries;

                List<ExportPlanetDTO> planets = new List<ExportPlanetDTO>();
                
                foreach (var d in discoveries)
                {
                    foreach (var p in d.Planets)
                    {
                        var orbiting = new List<string>();
                        orbiting.Add(p.HostStarSystem.Name);

                        var currentPlanet = new ExportPlanetDTO()
                        {
                            Name = p.Name,
                            Mass = (decimal)p.Mass,
                            Orbiting = orbiting
                        };

                        planets.Add(currentPlanet);
                    }
                }

                planets.Sort((a, b) => -1 * a.Mass.CompareTo(b.Mass));

                return planets;

            }
        }

        public static List<ExportAstronomerDTO> GetAstronomersByStarSystem(string starSystemName)
        {
            using (var context = new PlanetHuntersContext())
            {
                var starSystem = context.StarSystems.FirstOrDefault(sm => sm.Name == starSystemName);

                List<ExportAstronomerDTO> astronomers = new List<ExportAstronomerDTO>();

                foreach (var s in starSystem.Stars)
                {
                    if (s.Discovery != null)
                    {
                        foreach (var p in s.Discovery.Pioneers)
                        {
                            ExportAstronomerDTO curentAstronomer = new ExportAstronomerDTO()
                            {
                                Name = p.FirstName + " " + p.LastName,
                                Role = "pioneer"
                            };

                            if (astronomers.Where(a => a.Name == curentAstronomer.Name && a.Role == "pioneer").Count() == 0)
                            {
                                astronomers.Add(curentAstronomer);
                            }
                            
                        }

                        foreach(var o in s.Discovery.Observers)
                        {
                            ExportAstronomerDTO curentAstronomer = new ExportAstronomerDTO()
                            {
                                Name = o.FirstName + " " + o.LastName,
                                Role = "observer"
                            };

                            if (astronomers.Where(a => a.Name == curentAstronomer.Name && a.Role == "observer").Count() == 0)
                            {
                                astronomers.Add(curentAstronomer);
                            }
                        }
                    }
                }

                foreach (var p in starSystem.Planets)
                {
                    if (p.Discovery != null)
                    {
                        foreach (var pioneer in p.Discovery.Pioneers)
                        {
                            ExportAstronomerDTO curentAstronomer = new ExportAstronomerDTO()
                            {
                                Name = pioneer.FirstName + " " + pioneer.LastName,
                                Role = "pioneer"
                            };

                            if (astronomers.Where(a => a.Name == curentAstronomer.Name && a.Role == "pioneer").Count() == 0)
                            {
                                astronomers.Add(curentAstronomer);
                            }
                        }

                        foreach (var o in p.Discovery.Observers)
                        {
                            ExportAstronomerDTO curentAstronomer = new ExportAstronomerDTO()
                            {
                                Name = o.FirstName + " " + o.LastName,
                                Role = "observer"
                            };

                            if (astronomers.Where(a => a.Name == curentAstronomer.Name && a.Role == "observer").Count() == 0)
                            {
                                astronomers.Add(curentAstronomer);

                            }
                        }
                    }
                }

                astronomers.Sort((a,b) => a.Name.Split(' ')[1].CompareTo(b.Name.Split(' ')[1]));

                return astronomers;

            }
        }

        //public static List<ExportStarDTO> GetStars()
        //{
            
        //}

    }
}
