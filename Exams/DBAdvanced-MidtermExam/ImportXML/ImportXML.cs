using System;
using System.Linq;
using System.Xml.Linq;
using MassDefect.Data;
using MassDefect.Models;

namespace ImportXML
{
    class ImportXML
    {
        static void Main(string[] args)
        {
            ImportNewAnomalies();
        }

        private static void ImportNewAnomalies()
        {
            var xml = XDocument.Load("../../../datasets/new-anomalies.xml");

            var anomalies = xml.Root.Elements();

            using (var context = new MassDefectContext())
            {
                foreach (var anomaly in anomalies)
                {
                    ImportAnomalyAndVictims(context, anomaly);
                }
                context.SaveChanges();
            }

        }

        private static void ImportAnomalyAndVictims(MassDefectContext context, XElement anomaly)
        {
            var originPlanetName = anomaly.Attribute("origin-planet");
            var teleportPlanetName = anomaly.Attribute("teleport-planet");

            if (originPlanetName == null || teleportPlanetName == null)
            {
                Console.WriteLine("Error: Invalid data.");
                return;
            }

            var originPlanetEntity = context.Planets.FirstOrDefault(p => p.Name == originPlanetName.Value);
            var teleportingPlanetEntity = context.Planets.FirstOrDefault(p => p.Name == teleportPlanetName.Value);

            if (originPlanetEntity == null || teleportingPlanetEntity == null)
            {
                Console.WriteLine("Error: Invalid data.");
                return;
            }

            var anomalyEntity = new Anomaly()
            {
                OriginPlanet = originPlanetEntity,
                TeleportPlanet = teleportingPlanetEntity
            };

            foreach (var victim in anomaly.Elements("vicims"))
            {
                var victimName = victim.Attribute("name");

                if (victimName == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    return;
                }

                var victimEntity = context.People.FirstOrDefault(p => p.Name == victimName.Value);

                if (victimEntity == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    return;
                }

                anomalyEntity.Victims.Add(victimEntity);
            }

            context.Anomalies.Add(anomalyEntity);

        }
    }
}
