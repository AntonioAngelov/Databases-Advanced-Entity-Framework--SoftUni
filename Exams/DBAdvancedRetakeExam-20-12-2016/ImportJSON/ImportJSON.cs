using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeddingsPlanner.Data.DTOs;
using WeddingsPlanner.Data.Store;

namespace ImportJSON
{
    class ImportJSON
    {
        static void Main(string[] args)
        {
            //ImportAgencies();
            //ImportPeople();
            //ImportWeddings();
        }

        private static void ImportWeddings()
        {
            var data = File.ReadAllText("../../../datasets/weddings.json");

            var weddings = JsonConvert.DeserializeObject<IEnumerable<WeddingDTO>>(data);

            Store.StoreWeddings(weddings);
        }

        private static void ImportPeople()
        {
            var data = File.ReadAllText("../../../datasets/people.json");

            var people = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(data);

            Store.StorePeople(people);
        }

        private static void ImportAgencies()
        {
            var data = File.ReadAllText("../../../datasets/agencies.json");

            var agencies = JsonConvert.DeserializeObject<IEnumerable<AgencieDTO>>(data);

            Store.StoreAgencies(agencies);
        }
    }
}
