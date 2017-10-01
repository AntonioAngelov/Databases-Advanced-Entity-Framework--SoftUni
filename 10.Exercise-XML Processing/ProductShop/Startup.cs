using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ProductShop
{
    public class Startup
    {
        public static void Main()
        {
            using (var context = new ProductShopContext())
            {
                //exercise 2. Import data
                //ImportData(context);

                //exercise 3. Query and Export Data
                //Query 1 - Products In Range
                //ProductsInrange(context);

                //Query 2 - Sold Products
                //UsersWithSoldProducts(context);

                //Query 3 - Categories By Products Count
                //CategoriesByProductsCount(context);

                //Query 4 - Users and Products
                UsersAndProducts(context);
            }
        }

        private static void UsersAndProducts(ProductShopContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    SoldProductsCount = u.ProductsSold.Count,
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    ProductsSold = u.ProductsSold.Select(p => new
                    {
                        p.Name,
                        p.Price
                    })

                })
                .OrderByDescending(u => u.SoldProductsCount)
                .ThenBy(u => u.LastName)
                .ToList();

            XDocument usersDocument = new XDocument();

            XElement usersXml = new XElement("users");

            foreach (var user in users)
            {
                XElement userXml = new XElement("user");

                if (user.FirstName != null)
                {
                    XAttribute userFirstNameAtr = new XAttribute("first-name", user.FirstName);
                    userXml.Add(userFirstNameAtr);
                }

                XAttribute userLastNameAtr = new XAttribute("last-name", user.LastName);
                userXml.Add(userLastNameAtr);

                XAttribute userAgeAtr = new XAttribute("age", user.Age);
                userXml.Add(userAgeAtr);

                XElement soldProductsXml = new XElement("sold-products");

                XAttribute countAtr = new XAttribute("count", user.SoldProductsCount);
                soldProductsXml.Add(countAtr);

                foreach (var product in user.ProductsSold)
                {
                    XElement productXml = new XElement("product");

                    XAttribute productNameAtr = new XAttribute("name", product.Name);
                    XAttribute productPriceAtr = new XAttribute("price", product.Price);

                    productXml.Add(productNameAtr);
                    productXml.Add(productPriceAtr);

                    soldProductsXml.Add(productXml);
                }

                userXml.Add(soldProductsXml);
                usersXml.Add(userXml);

            }

            usersDocument.Add(usersXml);
            usersDocument.Save("../../XmlExports/users-and-products.xml");

        }

        private static void CategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(c => new
                {
                    c.Name,
                    productsCount = c.Products.Count,
                    avgProductsPrice = c.Products.Count != 0 ? c.Products.Sum(p => p.Price) / c.Products.Count : 0,
                    totalProductsPrice = c.Products.Count != 0 ? c.Products.Sum(p => p.Price) : 0
                })
                .OrderBy(c => c.productsCount)
                .ToList();

            XDocument categoriesDocument = new XDocument();
            XElement categoriesXml = new XElement("categories");

            foreach (var category in categories)
            {
                XElement categoryXml = new XElement("category");

                XAttribute categoryName = new XAttribute("name", category.Name);

                XElement productsCount = new XElement("products-count");
                productsCount.Value = category.productsCount.ToString();

                XElement avgPrice = new XElement("average-price");
                avgPrice.Value = category.avgProductsPrice.ToString();

                XElement totalRevenue = new XElement("total-revenue");
                totalRevenue.Value = category.totalProductsPrice.ToString();

                categoryXml.Add(categoryName);
                categoryXml.Add(productsCount);
                categoryXml.Add(avgPrice);
                categoryXml.Add(totalRevenue);

                categoriesXml.Add(categoryXml);
            }

            categoriesDocument.Add(categoriesXml);

            categoriesDocument.Save("../../XmlExports/categories-by-product-count.xml");
        }

        private static void UsersWithSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    productSold = u.ProductsSold
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                })
                .OrderBy(u => u.lastName)
                .ThenBy(u => u.firstName)
                .ToList();

            XDocument usersDocumet = new XDocument();
            XElement usersXml = new XElement("users");

            foreach (var user in users)
            {
                XElement userXml = new XElement("user");

                XAttribute lastNameAtr = new XAttribute("last-name", user.lastName);

                XElement soldProducts = new XElement("sold-products");

                foreach (var product in user.productSold)
                {
                    XElement productXml = new XElement("product");
                    XElement productName = new XElement("name");
                    productName.Value = product.name;

                    XElement productPrice = new XElement("price");
                    productPrice.Value = product.price.ToString();

                    productXml.Add(productName);
                    productXml.Add(productPrice);

                    soldProducts.Add(productXml);
                }
                if (user.firstName != null)
                {
                    XAttribute firstNameAtr = new XAttribute("first-name", user.firstName);
                    userXml.Add(firstNameAtr);
                }
                
                userXml.Add(lastNameAtr);
                userXml.Add(soldProducts);

                usersXml.Add(userXml);
            }

            usersDocumet.Add(usersXml);

            usersDocumet.Save("../../XmlExports/users-with-sold-products.xml");
        }

        private static void ProductsInrange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                })
                .OrderBy(p => p.Price)
                .ToList();

            XDocument productsDocumet = new XDocument();
            XElement productsXml = new XElement("products");

            foreach (var product in products)
            {
                XElement productXml = new XElement("product");

                XAttribute name = new XAttribute("name", product.Name);

                XAttribute price = new XAttribute("price", product.Price.ToString());

                productXml.Add(name);
                productXml.Add(price);
                if (product.buyer != " ")
                {
                    XAttribute buyer = new XAttribute("buyer", product.buyer);
                    productXml.Add(buyer);
                }
                
                productsXml.Add(productXml);
            }

            productsDocumet.Add(productsXml);
            productsDocumet.Save("../../XmlExports/products-in-range.xml");




        }

        private static void ImportData(ProductShopContext context)
        {
            ImportUsers(context);
            ImportCategories(context);
            ImportProducts(context);
        }

        private static void ImportProducts(ProductShopContext context)
        {
            XDocument xmlDoc = XDocument.Load("../../XmlImports/products.xml");

            var productsXml = xmlDoc.Root.Elements();

            List<Product> products = new List<Product>();

            var usersCount = context.Users.Count();
            var categoriesCount = context.Categories.Count();
            Random rnd = new Random();
            int counter = 1;

            foreach (var user in productsXml)
            {
                var currentProduct = new Product()
                {
                    Name = user.Element("name").Value,
                    Price = decimal.Parse(user.Element("price").Value),
                    SellerId = rnd.Next(1, usersCount)
                };

                if (counter % 2 == 0)
                {
                    currentProduct.BuyerId = rnd.Next(1, usersCount);
                }
                ++counter;

                for (int i = 0; i < 3; i++)
                {
                    currentProduct.Categories.Add(context.Categories.Find(rnd.Next(1, categoriesCount)));
                }

                products.Add(currentProduct);
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImportCategories(ProductShopContext context)
        {
            XDocument xmlDoc = XDocument.Load("../../XmlImports/categories.xml");

            var categoriesXml = xmlDoc.Root.Elements();

            List<Category> categories = new List<Category>();

            foreach (var category in categoriesXml)
            {
                Category currentCategory = new Category()
                {
                    Name = category.Element("name").Value
                };

                categories.Add(currentCategory);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportUsers(ProductShopContext context)
        {
            XDocument xmlDoc = XDocument.Load("../../XmlImports/users.xml");

            var usersXml = xmlDoc.Root.Elements();

            List<User> users = new List<User>();

            foreach (var user in usersXml)
            {
                var firstName = user.Attribute("first-name") != null ? user.Attribute("first-name").Value : null;
                var lastName = user.Attribute("last-name").Value;
                int age = user.Attribute("age") != null ? int.Parse(user.Attribute("age").Value) : 0;

                User currentIUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age

                };

            users.Add(currentIUser);
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
