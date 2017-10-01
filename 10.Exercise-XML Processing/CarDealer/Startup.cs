using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CarDealer
{
    class Startup
    {
        static void Main(string[] args)
        {
            using (var context = new CarDealerContext())
            {
                //exercise 1. Car Dealer Import Data
                //ImportData(context);

                //exercise 2. Car Dealer Query and Export Data
                // Query 1 – Cars
                //GetCars(context);

                //Query 2 – Cars from make Ferrari
                //CarsFromMakeFerrari(context);

                //Query 3 – Local Suppliers
                //LocalSuppliers(context);

                //Query 4 – Cars with Their List of Parts
                //CarsWithParts(context);

                //Query 5 – Total Sales by Customer
                //TotalSalsByCustomer(context);

                //Query 6 – Sales with Applied Discount
                //SalesWithDiscount(context);
            }
        }

        private static void SalesWithDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    CarMake = s.Car.Make,
                    CarModel = s.Car.Model,
                    CarDistance = s.Car.TravelledDistance,
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    Price = s.Car.Parts.Sum(p => p.Price),
                    PriceWithDiscount = (s.Car.Parts.Sum(p => p.Price)) - ((s.Car.Parts.Sum(p => p.Price)) * (s.Discount / 100m))

                })
                .ToList();

            XDocument salesDocument = new XDocument();

            XElement salesXml = new XElement("sales");

            foreach (var sale in sales)
            {
                XElement currentSale = new XElement("sale");

                XElement carXml = new XElement("car");

                XAttribute carMakeAtr = new XAttribute("make", sale.CarMake);
                carXml.Add(carMakeAtr);

                XAttribute carModelAtr = new XAttribute("model", sale.CarModel);
                carXml.Add(carModelAtr);

                XAttribute carDistanceAtr = new XAttribute("travelled-distance", sale.CarDistance);
                carXml.Add(carDistanceAtr);

                currentSale.Add(carXml);

                XElement customerNameXml = new XElement("customer-name");
                customerNameXml.Value = sale.CustomerName;
                currentSale.Add(customerNameXml);

                XElement discountXml = new XElement("discount");
                discountXml.Value = sale.Discount.ToString();
                currentSale.Add(discountXml);

                XElement priceXml = new XElement("price");
                priceXml.Value = sale.Price.ToString();
                currentSale.Add(priceXml);

                XElement priceWithDiscountXml = new XElement("price-with-discount");
                priceWithDiscountXml.Value = sale.PriceWithDiscount.ToString();
                currentSale.Add(priceWithDiscountXml);

                salesXml.Add(currentSale);
            }

            salesDocument.Add(salesXml);
            salesDocument.Save("../../XmlExports/sales-with-applied-discount.xml");

        }

        private static void TotalSalsByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count,
                    SpentMoney = c.Sales.Count != 0 ? c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price)) : 0
                })
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToList();

            XDocument customersDocument = new XDocument();

            XElement customersXml = new XElement("customers");

            foreach (var customer in customers)
            {
                XElement currentCustomer = new XElement("customer");

                XAttribute fullNameAtr = new XAttribute("full-name", customer.FullName);
                currentCustomer.Add(fullNameAtr);

                XAttribute boughtCarsAtr = new XAttribute("bought-cars", customer.BoughtCars);
                currentCustomer.Add(boughtCarsAtr);

                XAttribute spentMoneyAtr = new XAttribute("spent-money", customer.SpentMoney.ToString());
                currentCustomer.Add(spentMoneyAtr);

                customersXml.Add(currentCustomer);

            }

            customersDocument.Add(customersXml);
            customersDocument.Save("../../XmlExports/total-sales-by-customer.xml");
        }

        private static void CarsWithParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    carMake = c.Make,
                    carModel = c.Model,
                    travelledDist = c.TravelledDistance,
                    carParts = c.Parts.Select(p => new
                    {
                        p.Name,
                        p.Price
                    })
                })
                .ToList();

            XDocument carsDocument = new XDocument();

            XElement carsXml = new XElement("cars");

            foreach (var car in cars)
            {
                XElement currentCar = new XElement("car");

                XAttribute carMakeAtr = new XAttribute("make", car.carMake);
                currentCar.Add(carMakeAtr);

                XAttribute carModelAtr = new XAttribute("model", car.carModel);
                currentCar.Add(carModelAtr);

                XAttribute carDistanceAtr = new XAttribute("travelled-distance", car.travelledDist);
                currentCar.Add(carDistanceAtr);

                XElement partsXml = new XElement("parts");

                foreach (var part in car.carParts)
                {
                    XElement currentPart = new XElement("part");

                    XAttribute partNameAtr = new XAttribute("name", part.Name);
                    currentPart.Add(partNameAtr);

                    XAttribute partPriceAtr = new XAttribute("price", part.Price);
                    currentPart.Add(partPriceAtr);

                    partsXml.Add(currentPart);
                }

                currentCar.Add(partsXml);
                carsXml.Add(currentCar);
            }

            carsDocument.Add(carsXml);
            carsDocument.Save("../../XmlExports/cars-with-their-parts.xml");
        }

        private static void LocalSuppliers(CarDealerContext context)
        {
            var localSuppliers = context.Suppliers
                .Where(s => s.Isimporter == false)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    numberOfParts = s.Parts.Count
                })
                .ToList();

            XDocument suppliersDocument = new XDocument();

            XElement suppliersXml = new XElement("suppliers");

            foreach (var supplier in localSuppliers)
            {
                XElement currentSupplier = new XElement("supplier");

                XAttribute supplierIdAtr = new XAttribute("id", supplier.Id);
                currentSupplier.Add(supplierIdAtr);

                XAttribute supplierNameAtr = new XAttribute("name", supplier.Name);
                currentSupplier.Add(supplierNameAtr);

                XAttribute supplierPartsCountAtr = new XAttribute("parts-count", supplier.numberOfParts);
                currentSupplier.Add(supplierPartsCountAtr);

                suppliersXml.Add(currentSupplier);
            }

            suppliersDocument.Add(suppliersXml);
            suppliersDocument.Save("../../XmlExports/local-suppliers.xml");
        }

        private static void CarsFromMakeFerrari(CarDealerContext context)
        {
            var ferrariCars = context.Cars
                .Where(c => c.Make == "Ferrari")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            XDocument carsDocument = new  XDocument();

            XElement carsXml = new XElement("cars");

            foreach (var car in ferrariCars)
            {
                XElement currentCar = new XElement("car");

                XAttribute carId = new XAttribute("id", car.Id);
                currentCar.Add(carId);

                XAttribute carModel = new XAttribute("model", car.Model);
                currentCar.Add(carModel);

                XAttribute carTravelledDist = new XAttribute("travelled-distance", car.TravelledDistance);
                currentCar.Add(carTravelledDist);

                carsXml.Add(currentCar);
            }

            carsDocument.Add(carsXml);
            carsDocument.Save("../../XmlExports/cars-from-make-ferrari.xml");
        }

        private static void GetCars(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ToList();

            XDocument carsDocument = new XDocument();

            XElement carsXml = new XElement("cars");

            foreach (var car in cars)
            {
                XElement carXml = new XElement("car");

                XElement carMake = new XElement("make");
                carMake.Value = car.Make;
                carXml.Add(carMake);

                XElement carModel = new XElement("model");
                carModel.Value = car.Model;
                carXml.Add(carModel);

                XElement carTravelledDist = new XElement("travelled-distance");
                carTravelledDist.Value = car.TravelledDistance.ToString();
                carXml.Add(carTravelledDist);

                carsXml.Add(carXml);
            }

            carsDocument.Add(carsXml);
            carsDocument.Save("../../XmlExports/cars.xml");

        }

        private static void ImportData(CarDealerContext context)
        {
            ImportSuppliers(context);
            ImportParts(context);
            ImportCars(context);
            ImportCustomers(context);
            GenerateSales(context);
        }

        private static void GenerateSales(CarDealerContext context)
        {
            var carsCount = context.Cars.Count();
            var customersCount = context.Customers.Count();

            int[] dicounts = new int[]{5, 10, 0, 15, 20, 30, 40, 50};
            Random rnd = new Random();

            List<Sale> sales = new List<Sale>();

            for (int i = 1; i <= carsCount ; i++)
            {
                if (i % 2 == 0)
                {
                    Sale currentSale = new Sale()
                    {
                        CarId = rnd.Next(1, carsCount),
                        CustomerId = rnd.Next(1, customersCount),
                        Discount = dicounts[rnd.Next(1, 8) - 1]
                    };
                    sales.Add(currentSale);
                } 
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void ImportCustomers(CarDealerContext context)
        {
            var data = XDocument.Load("../../XmlImports/customers.xml");

            var customersXml = data.Root.Elements();

            List<Customer> customers = new List<Customer>();

            foreach (var customer in customersXml)
            {
                var name = customer.Attribute("name").Value;
                var birthDate = DateTime.Parse(customer.Element("birth-date").Value);
                var isYoungDriver = bool.Parse(customer.Element("is-young-driver").Value);
                Customer currentCustomer = new Customer()
                {
                    Name = name,
                    BirthDate = birthDate,
                    IsYoungDriver = isYoungDriver
                };

                customers.Add(currentCustomer);
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void ImportCars(CarDealerContext context)
        {
            var data = XDocument.Load("../../XmlImports/cars.xml");

            var carsXml = data.Root.Elements();

            List<Car> cars = new List<Car>();
            var rnd = new Random();

            foreach (var car in carsXml)
            {
              Car currentCar = new Car()
              {
                  Make = car.Element("make").Value,
                  Model = car.Element("model").Value,
                  TravelledDistance = long.Parse(car.Element("travelled-distance").Value)
              };

                int end = rnd.Next(10, 20);
                int partsCount = context.Parts.Count();

                for (int i = 1; i <= end; i++)
                {
                    currentCar.Parts.Add(context.Parts.Find(rnd.Next(1, partsCount)));
                }

                cars.Add(currentCar);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void ImportParts(CarDealerContext context)
        {
            var data = XDocument.Load("../../XmlImports/parts.xml");

            var partsXml = data.Root.Elements();
            List<Part> parts = new List<Part>();

            var suppliersCount = context.Suppliers.Count();
            var rnd = new Random();

            foreach (var part in partsXml)
            {
                Part currentPart = new Part()
                {
                    Name = part.Attribute("name").Value,
                    Price = decimal.Parse(part.Attribute("price").Value),
                    Quantity = int.Parse(part.Attribute("quantity").Value),
                    Supplier_Id = rnd.Next(1, suppliersCount)
                };

                parts.Add(currentPart);
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        private static void ImportSuppliers(CarDealerContext context)
        {
            var data = XDocument.Load("../../XmlImports/suppliers.xml");

            var suppliersXml = data.Root.Elements();
            List<Supplier> suppliers = new List<Supplier>();
            foreach (var supplier in suppliersXml)
            {
                Supplier currentSupplier = new Supplier()
                {
                    Name = supplier.Attribute("name").Value,
                    Isimporter = bool.Parse(supplier.Attribute("is-importer").Value)
                };

                suppliers.Add(currentSupplier);
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }
    }
}
