using System;
using System.Xml.Linq;
using WeddingsPlanner.Data.Store;

namespace ImportXML
{
    class ImportXML
    {
        static void Main(string[] args)
        {
            //ImportVenues();
            //ImportPresents();
        }

        private static void ImportPresents()
        {
            var data = XDocument.Load("../../../datasets/presents.xml");

            var presents = data.Root.Elements();

            Store.StorePresents(presents);


        }

        private static void ImportVenues()
        {
            var data = XDocument.Load("../../../datasets/venues.xml");

            var venues = data.Root.Elements();

            Store.StoreVenues(venues);
        }
    }
}
