namespace Import
{
    using PlanetHunters.Data.Store;

    using System.Xml.Linq;

    public static class ImportXML
    {
        public static void ImportStarts()
        {
            var xml = XDocument.Load("../../../datasets/stars.xml");

            var starsXml = xml.Root.Elements();

            Store.StoreStars(starsXml);
        }

        public static void ImportDiscoveries()
        {
            var xml = XDocument.Load("../../../datasets/discoveries.xml");

            var discoveriesXml = xml.Root.Elements();

            Store.StoreDiscoveries(discoveriesXml);

        }
    }
}
