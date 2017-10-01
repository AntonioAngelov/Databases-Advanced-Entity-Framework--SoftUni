namespace CodeFirst_Gringotts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;

    class Startup
    {
        static void Main(string[] args)
        {
            var context = new GringottsContext();
            
            WizardDeposit newDeposit = new WizardDeposit()
            {
                FirstName = "Albus",
                LastName = "Dumbledore",
                Age = 150,
                MagicWandCreator = "Antioch Peverell",
                MagicWandSize = 15,
                DepositStartDate = new DateTime(2016, 10, 20),
                DepositExpirationDate = new DateTime(2020, 10, 20),
                DepositAmount = 20000.24,
                DepositCharge = 0.2,
                IsDepositExpired = false
            };

            context.WizardDeposits.Add(newDeposit);
            context.SaveChanges();
        }
    }
}
