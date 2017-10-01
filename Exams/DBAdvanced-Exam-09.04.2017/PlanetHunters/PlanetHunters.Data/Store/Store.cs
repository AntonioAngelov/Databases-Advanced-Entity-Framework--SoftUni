using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Xml.Linq;
using PlanetHunters.Data.DTOs;
using PlanetHunters.Models;

namespace PlanetHunters.Data.Store
{
    public static class Store
    {
        public static void StoreAstronomers(List<AstronomerDTO> astronomers)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var a in astronomers)
                {
                    if (a.FirstName == null || a.LastName == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }

                    var currentAstronomer = new Astronomer()
                    {
                        FirstName = a.FirstName,
                        LastName = a.LastName
                    };

                    try
                    {
                        context.Astronomers.Add(currentAstronomer);
                        context.SaveChanges();
                        Console.WriteLine($"Record {a.FirstName} {a.LastName} successfully imported.");
                    }
                    catch (DbEntityValidationException)
                    {
                        Console.WriteLine("Invalid data format.");

                    }
                }
            }
        }

        public static void StoreTelescopes(List<TelescopeDTO> telescopes)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var t in telescopes)
                {
                    if (t.Name == null || t.Location == null || t.MirrorDiameter == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }
                    
                    try
                    {
                        var currrentTelescope = new Telescope()
                        {
                            Name = t.Name,
                            Location = t.Location,
                            MirrorDiameter = (decimal)t.MirrorDiameter
                        };

                        context.Telescopes.Add(currrentTelescope);
                        context.SaveChanges();
                        Console.WriteLine($"Record {t.Name} successfully imported.");
                    }
                    catch (Exception ex)
                    {
                        if (ex is DbEntityValidationException || ex is ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Invalid data format.");
                        }
                    }
                }
            }
        }

        public static void StorePlanets(List<PlanetDTO> planets)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var p in planets)
                {
                    if (p.Name == null || p.StarSystem == null || p.Mass == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }

                    var starSystem = context.StarSystems.FirstOrDefault(sm => sm.Name == p.StarSystem);
                    
                    try
                    {
                       var currentPlannet = new Planet()
                       {
                           Name = p.Name,
                           Mass = (decimal)p.Mass,
                           HostStarSystem = starSystem == null ? new StarSystem() { Name = p.StarSystem} : starSystem

                       };

                        context.Planets.Add(currentPlannet);
                        context.SaveChanges();
                        Console.WriteLine($"Record {p.Name} successfully imported.");
                    }
                    catch (Exception e)
                    {
                        if (e is DbEntityValidationException || e is ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Invalid data format.");
                        }
                    }

                }
            }
        }

        public static void StoreStars(IEnumerable<XElement> starsXml)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var s in starsXml)
                {
                    var name = s.Element("Name");
                    var temperature = s.Element("Temperature");
                    var starSystemName = s.Element("StarSystem");

                    if (name == null || temperature == null || starSystemName == null)
                    {
                        Console.WriteLine("Invalid data format.");
                        continue;
                    }

                    var starSystemEntity = context.StarSystems.FirstOrDefault(sm => sm.Name == starSystemName.Value);

                    var currentStar = new Star()
                    {
                        Name = name.Value,
                        Temperature = int.Parse(temperature.Value),
                        HostStarSystem = starSystemEntity == null ? new StarSystem() { Name = starSystemName.Value} : starSystemEntity
                    };

                    try
                    {
                        context.Stars.Add(currentStar);
                        context.SaveChanges();
                        Console.WriteLine($"Record {name.Value} successfully imported.");
                    }
                    catch (DbEntityValidationException)
                    {
                        Console.WriteLine("Invalid data format.");
                    }

                }

            }
        }

        public static void StoreDiscoveries(IEnumerable<XElement> discoveriesXml)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var d in discoveriesXml)
                {
                    var dateXml = d.Attribute("DateMade");
                    var telescopeName = d.Attribute("Telescope");
                    var stars = d.Element("Stars");
                    var planets = d.Element("Planets");
                    var pioneers = d.Element("Pioneers");
                    var observes = d.Element("Observers");

                    if (dateXml == null || telescopeName == null )
                    {
                        continue;
                    }

                    var date = DateTime.Parse(dateXml.Value);
                    var telescope = context.Telescopes.FirstOrDefault(t => t.Name == telescopeName.Value);

                    if (telescope == null)
                    {
                        continue;
                    }

                    var currentDiscovery = new Discovery()
                    {
                        AnnounceDate = date,
                        TelescopeUsed = telescope
                    };

                    bool isValidEntity = true;

                    foreach (var s in stars.Elements())
                    {
                        var currentStar = context.Stars.FirstOrDefault(star => star.Name == s.Value);

                        if (currentStar == null)
                        {
                            isValidEntity = false;
                            break;
                        }

                        isValidEntity = true;
                        currentDiscovery.Stars.Add(currentStar);

                    }

                    if (isValidEntity == false)
                    {
                        continue;
                    }

                    if (planets.Elements() != null)
                    {
                        foreach (var p in planets.Elements())
                        {
                            var currentPlanet = context.Planets.FirstOrDefault(pl => pl.Name == p.Value);

                            if (currentPlanet == null)
                            {
                                isValidEntity = false;
                                break;
                            }

                            isValidEntity = true;
                            currentDiscovery.Planets.Add(currentPlanet);
                        }

                        if (isValidEntity == false)
                        {
                            continue;
                        }
                    }

                    foreach (var o in observes.Elements())
                    {
                        var names = o.Value.Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
                        var lastName = names[0];
                        var firstName = names[1];

                        var currentObserver =
                            context.Astronomers.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

                        if (currentObserver == null)
                        {
                            isValidEntity = false;
                            break;
                        }

                        isValidEntity = true;
                        currentDiscovery.Observers.Add(currentObserver);

                    }

                    if (isValidEntity == false)
                    {
                        continue;
                    }

                    foreach (var p in pioneers.Elements())
                    {
                        var names = p.Value.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        var lastName = names[0];
                        var firstName = names[1];

                        var currentPioneer =
                            context.Astronomers.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);

                        if (currentPioneer == null)
                        {
                            isValidEntity = false;
                            break;
                        }

                        isValidEntity = true;
                        currentDiscovery.Pioneers.Add(currentPioneer);

                    }

                    if (isValidEntity == false)
                    {
                        continue;
                    }

                    context.Discoveries.Add(currentDiscovery);
                    context.SaveChanges();
                    Console.WriteLine($"Discovery ({currentDiscovery.AnnounceDate}-{currentDiscovery.TelescopeUsed.Name}) with {currentDiscovery.Stars.Count} star(s), {currentDiscovery.Planets.Count} planet(s), {currentDiscovery.Pioneers.Count} pioneer(s) and {currentDiscovery.Observers.Count} observers successfully  imported.");


                }
            }
        }

    }
}
