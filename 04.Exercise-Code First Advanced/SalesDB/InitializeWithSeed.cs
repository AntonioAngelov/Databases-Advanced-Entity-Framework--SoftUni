using System;
using System.Data.Entity.Migrations;

namespace SalesDB
{
    using System.Data.Entity;
    using Models;

    public class InitializeWithSeed : CreateDatabaseIfNotExists<SalesContext>
    {
        protected override void Seed(SalesContext context)
        {
            //string[] products = {"Boza", "Milk", "Pork", "Pasta", "Rakia"};
            //string[] customers = {"Pesho", "Gosho", "Mitko", "Desi", "Maria"};
            //string[] locations = {"Drizhba 1, bl. 60", "Busmanci", "Serdika N.23", "Suhata Reka 34", "Mladost 1"};
            //string[] emails = {"a@abv.bg", "b.@gmail.com", "c@yahoo.com", "d.@abv.bg", "e@gmail.com"};
            //int[] crCardNumbers = {14342, 1213242, 132134235, 31331231, 332424};
            //DateTime[] dates =
            //{
            //    new DateTime(2016, 3, 3), new DateTime(2017, 1, 1), new DateTime(2017, 2, 8),
            //    new DateTime(2015, 1, 12), new DateTime(2014, 1, 1)
            //};

            //for (int i = 0; i < 5; i++)
            //{
            //    var product = new Product()
            //    {
            //        Name = products[i],
            //        Quantity = i,
            //        Price = i + 0.99m
            //    };

            //    context.Products.AddOrUpdate(p=> p.Name, product);

            //    var customer = new Customer()
            //    {
            //        Name = customers[i],
            //        Email = emails[i],
            //        CreditCardNumber = crCardNumbers[i]
            //    };

            //    context.Customers.AddOrUpdate(c => c.Name, customer);

            //    var location = new StoreLocation()
            //    {
            //        LocationName = locations[i]
            //    };

            //    context.StoreLocations.AddOrUpdate(l=> l.LocationName ,location);

            //    context.Sales.Add(new Sale()
            //    {
            //        Product = product,
            //        Customer = customer,
            //        StoreLocation = location,
            //        Date = dates[i]
            //    });


            //}

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
