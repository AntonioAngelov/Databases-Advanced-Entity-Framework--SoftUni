using Newtonsoft.Json;
using ProductсShop.Models;

namespace ProductсShop
{
    using System.IO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ProductсShop.Data;

    class Startup
    {
        static void Main(string[] args)
        {
            
            using (var context = new ProductsShopContext())
            {
                context.Database.Initialize(true);

                //exercise 2. Import data
                //ImportData(context);

                //exercise 3. Query and Export Data
                //Query 1 - Products In Range
                //ProductsInRange(context);

                //Query 2 - Successfully Sold Products
                //UsersWithSoldItems(context);

                //Query 3 - Categories By Products Count
                //CategoriesByProductCount(context);

                //Query 4 - Users and Products
                UsersThatHaveSoldProducts(context);

            }
        }

        private static void UsersThatHaveSoldProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count >= 1)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.ProductsSold.Count,
                        products = u.ProductsSold.Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                    }
                })
                .ToList();

            var usersJson = JsonConvert.SerializeObject(new
            {
                usersCount = users.Count,
                users = users
            }, Formatting.Indented);


            File.WriteAllText("../../JsonFiles/users-and-products.json", usersJson);
        }

        private static void CategoriesByProductCount(ProductsShopContext context)
        {
            var categories = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.Products.Count,
                    averagePrice = c.Products.Sum(p => p.Price) / c.Products.Count,
                    totalRevenue = c.Products.Sum(p => p.Price)
                })
                .ToList();

            var categoriesJson = JsonConvert.SerializeObject(categories, Formatting.Indented);

            File.WriteAllText("../../JsonFiles/categoies-by-product-count.json", categoriesJson);
        }

        private static void UsersWithSoldItems(ProductsShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count > 0)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold
                        .Where(p => p.Buyer != null)
                        .Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        })
                })
                .OrderBy(u => u.lastName)
                .ThenBy(u => u.firstName)
                .ToList();

            var usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);

            File.WriteAllText("../../JsonFiles/users-with-products.json", usersJson);

        }

        private static void ProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    name = p.Name,
                    price = p.Price,
                    seller =
                    p.Seller.FirstName != null ? p.Seller.FirstName + " " + p.Seller.LastName : p.Seller.LastName
                })
                .ToList();

            var productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);

            File.WriteAllText("../../JsonFiles/products-in-range.json", productsJson);
            
        }

        private static void ImportData(ProductsShopContext context)
        {
            ImortUsers(context);
            ImportProducts(context);
            ImportCategories(context);

        }

        private static void ImportCategories(ProductsShopContext context)
        {
            var categoriesData = File.ReadAllText("../../Import/categories.json");
            var categories = JsonConvert.DeserializeObject<List<Category>>(categoriesData);

            var products = context.Products.ToList();

            var categoriesCount = categories.Count;
            for (int i = 0; i < products.Count; i++)
            {
                categories[i % categoriesCount].Products.Add(products[i]);
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        private static void ImportProducts(ProductsShopContext context)
        {
            var productsData = File.ReadAllText("../../Import/products.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsData);

            var usersCount = context.Users.Count();

            for (int i = 0; i < 200; i++)
            {
                products[i].Seller = context.Users.Find((i % usersCount) + 1);
                if (i < 160)
                {
                    products[i].Buyer = context.Users.Find(((i * 2) % usersCount) + 1);
                }
            }
            

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static void ImortUsers(ProductsShopContext context)
        {
            var usersData = File.ReadAllText("../../Import/users.json");
            var users = JsonConvert.DeserializeObject<List<User>>(usersData);

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
