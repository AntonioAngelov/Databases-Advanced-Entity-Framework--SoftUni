namespace OOP_Intro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    class Calculation
    {
        private static double planckConstant = 6.62606896e-34;

        private static double pi = 3.14159;

        public static double reducedPlanckConstant()
        {
            return Calculation.planckConstant / (2 * pi);
        }
    }
}
