using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeddingsPlanner.Data.Queries;

namespace ExportJSON
{
    class ExportJSON
    {
        static void Main(string[] args)
        {
            OrderedAgencies();
            GuestLists();
        }

        private static void GuestLists()
        {
            var guestLists = Queries.GetGuestLists();
            var guestListsJSON = JsonConvert.SerializeObject(guestLists, Formatting.Indented);

            File.WriteAllText("../../../exports/guest-lists.json", guestListsJSON);
        }

        private static void OrderedAgencies()
        {
            var agencies = Queries.GetAgencies();

            var agenciesJson = JsonConvert.SerializeObject(agencies, Formatting.Indented);

            File.WriteAllText("../../../exports/ordered-agencies.json", agenciesJson);
        }
    }
}
