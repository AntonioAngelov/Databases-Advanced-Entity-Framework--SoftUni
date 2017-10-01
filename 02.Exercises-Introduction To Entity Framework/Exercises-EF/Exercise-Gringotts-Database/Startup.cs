namespace Exercise_Gringotts_Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Startup
    {
        static void Main(string[] args)
        {
            GringottsContext context = new GringottsContext();

            var wizardNames = context.WizzardDeposits
                .Where(w => w.DepositGroup == "Troll Chest")
                .OrderBy(w => w.FirstName)
                .Select(w => w.FirstName.Substring(0, 1))
                .Distinct()
                .ToList();

            foreach (var name in wizardNames)
            {
                Console.WriteLine(name);
            }

        }
    }
}
