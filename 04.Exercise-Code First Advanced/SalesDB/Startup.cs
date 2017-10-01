namespace SalesDB
{
    using Models;
    using System;
    using System.Linq;

    class Startup
    {
        static void Main(string[] args)
        {
            var context = new SalesContext();

            context.Customers.Add(new Customer());
            context.SaveChanges();

        }
    }
}
