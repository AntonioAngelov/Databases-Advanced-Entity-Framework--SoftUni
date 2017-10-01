using System;
using System.Collections;
using System.Linq;

namespace ImportJSON
{
    using System.Collections.Generic;
    using System.IO;
    using MassDefect.Data;
    using MassDefect.Data.DTOs;
    using MassDefect.Models;
    using Newtonsoft.Json;

    class ImportJSON
    {
        static void Main(string[] args)
        {
            //ImportSolarSystems();
            //ImportStars();
            ImportAnomalyVictims();

        }

        private static void ImportAnomalyVictims()
        {
            var data = File.ReadAllText("../../../datasets/anomaly-victims.json");

            var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDTO>>(data);

            using (var context = new MassDefectContext())
            {
                foreach (var av in anomalyVictims)
                {
                    if (av.Id == null || av.Person == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }


                    Anomaly anomalyEntity = GetAnomalyById((int)av.Id, context);
                    Person personEntity = GetPersonByName(av.Person, context);

                    if (anomalyEntity == null || personEntity == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    anomalyEntity.Victims.Add(personEntity);
                    
                }

                context.SaveChanges();
            }
            
        }

        private static Person GetPersonByName(string personName, MassDefectContext context)
        {
            return context.People.FirstOrDefault(p => p.Name == personName);
        }

        private static Anomaly GetAnomalyById(int id, MassDefectContext context)
        {
            return context.Anomalies.Find(id);
        }

        private static void ImportStars()
        {
            var data = File.ReadAllText("../../../datasets/stars.json");

            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(data);

            using (var context = new MassDefectContext())
            {
                foreach (var star in stars)
                {
                    if(star.Name == null || star.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var solarSystem = context.SolarSystems.FirstOrDefault(s => s.Name == star.SolarSystem);
                    if (solarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    Star currenStar = new Star()
                    {
                        Name = star.Name,
                        SolarSystem = solarSystem
                    };

                    context.Stars.Add(currenStar);
                    Console.WriteLine($"Successfully imported Star {currenStar.Name}.");

                }
                context.SaveChanges();
            }
        } 

        private static void ImportSolarSystems()
        {
            var data = File.ReadAllText("../../../datasets/solar-systems.json");

            var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(data);

            using (var contex = new MassDefectContext())
            {
                foreach (var solarSystem in solarSystems)
                {
                    var name = solarSystem.Name;
                    if (name == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    var currentSSystem = new SolarSystem()
                    {
                        Name = solarSystem.Name
                    };

                    contex.SolarSystems.Add(currentSSystem);
                    Console.WriteLine($"Successfully imported Solar System {currentSSystem.Name}.");
                }

                contex.SaveChanges();
            }
        }
    }
}
