using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer
{
    using System.IO;
    using System;
    using CarDealer.Data;

    class Startup
    {
        static void Main()
        {
            using (var context = new CarDealerContext())
            {
                context.Database.Initialize(true);

                //exercise 2. Car Dealer Import Data
                //ImportData(context);

                //exercise 3. Car Dealer Query and Export Data
                //Query 1 – Ordered Customers
                //OrderedCustomers(context);

                //Query 2 – Cars from make Toyota
                //CarsFromMakeToyota(context);

                //Query 3 – Local Suppliers
                //LocalSuppliers(context);

                //Query 4 – Cars with Their List of Parts
                //CarsWithTheirParts(context);

                //Query 5 – Total Sales by Customer
                //TotalSalesByCustomer(context);

                //Query 6 – Sales with Applied Discount
                //GetSales(context);

            }
        }

        private static void GetSales(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    car = new
                    {
                        s.Car.Make,
                        s.Car.Model,
                        s.Car.TravelledDistance
                    },
                    customerName = s.Customer.Name,
                    Discount = s.Discount / 100,
                    price = s.Car.Parts.Sum(p => p.Price),
                    priceWithDiscount = (s.Car.Parts.Sum(p => p.Price)) - ((s.Car.Parts.Sum(p => p.Price)) * (s.Discount / 100))
                })
                .ToList();

            var salesJson = JsonConvert.SerializeObject(sales, Formatting.Indented);

            File.WriteAllText("../../JsonExports/sales.json", salesJson);
        }

        private static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count >= 1)
                .Select(c => new
                {
                    fullName = c.Name,
                    boughtCars = c.Sales.Count,
                    spentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
                })
                .OrderByDescending(c => c.spentMoney)
                .ThenByDescending(c => c.boughtCars)
                .ToList();

            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("../../JsonExports/total-sales-by-customer.json", customersJson);
        }

        private static void CarsWithTheirParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    car = new
                    {
                        c.Make,
                        c.Model,
                        c.TravelledDistance
                    },
                    parts = c.Parts.Select(p => new
                    {
                        p.Name,
                        p.Price
                    })

                })
                .ToList();

            var carsJson = JsonConvert.SerializeObject(cars, Formatting.Indented);

            File.WriteAllText("../../JsonExports/cars-with-their-parts.json", carsJson);
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(s => s.Isimporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    PartsCount = s.Parts.Count
                })
                .ToList();

            var localSuppliersJson = JsonConvert.SerializeObject(localSuppliers, Formatting.Indented);

            File.WriteAllText("../../JsonExports/local-supplier.json", localSuppliersJson);


        }

        private static void CarsFromMakeToyota(CarDealerContext context)
        {
            var toyotaCars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    c.Id,
                    c.Make,
                    c.Model,
                    c.TravelledDistance
                })
                .ToList();

            var toyotaCarsJson = JsonConvert.SerializeObject(toyotaCars, Formatting.Indented);

            File.WriteAllText("../../JsonExports/cars-from-Toyota.json", toyotaCarsJson);
        }

        private static void OrderedCustomers(CarDealerContext context)
        {
            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.BirthDate,
                    c.IsYoungDriver
                    })
                .ToList();

            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);

            File.WriteAllText("../../JsonExports/ordered-customers.json", customersJson);
        }

        private static void ImportData(CarDealerContext context)
        {
            ImportSuppliers(context);
            ImportParts(context);
            ImportCars(context);
            ImportCustomers(context);
            ImportSales(context);
        }

        private static void ImportSales(CarDealerContext context)
        {
            List<Sale> sales = new List<Sale>();

            var carsCount = context.Cars.Count();
            var customersCount = context.Customers.Count();
            int[] discounts = {0, 5, 10, 15, 20, 30, 40, 50};
            Random rnd = new Random();

            for (int i = 0; i <= carsCount; i++)
            {
                if (i % 2 == 0)
                {
                    Sale currentSale = new Sale();
                    currentSale.Discount = discounts[rnd.Next(0, 7)];
                    currentSale.CarId = rnd.Next(1, carsCount);
                    currentSale.CustomerId = rnd.Next(1, customersCount);

                    sales.Add(currentSale);
                }
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();

        }

        private static void ImportCustomers(CarDealerContext context)
        {
            var customersJson = File.ReadAllText("../../Imports/customers.json");

            var customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson);

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            var carsJson = File.ReadAllText("../../Imports/cars.json");

            var cars = JsonConvert.DeserializeObject<List<Car>>(carsJson);

            var partsCount = context.Parts.Count();
            int carsCount = cars.Count;
            Random rnd = new Random();
            var parts = context.Parts.ToList();

            for (int i = 0; i < carsCount; i++)
            {
                int end = rnd.Next(10, 20);
                for (int j = 1; j <= end; j++)
                {
                    cars[i].Parts.Add(parts[rnd.Next(1, partsCount)]);
                }
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            var partsJson = File.ReadAllText("../../Imports/parts.json");

            var parts = JsonConvert.DeserializeObject<List<Part>>(partsJson);

            int suppliersCount = context.Suppliers.Count();
            int partsCount = parts.Count;

            for (int i = 0; i < partsCount; i++)
            {
                parts[i].Supplier = context.Suppliers.Find((i % suppliersCount) + 1);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();

        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            var suppliersJson = File.ReadAllText("../../Imports/suppliers.json");

            var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(suppliersJson);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
    }
}
