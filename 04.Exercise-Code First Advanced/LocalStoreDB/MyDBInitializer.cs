using System.Data.Entity;

namespace LocalStoreDB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;


    public class MyDBInitializer: DropCreateDatabaseIfModelChanges<LocalStoreContext>
    {
        protected override void Seed(LocalStoreContext context)
        {
            var product1 = new Product()
            {
                Name = "Milk",
                DistributorName = "Verea",
                Description = "Bio product",
                Price = 2
            };

            var product2 = new Product()
            {
                Name = "Pork meat",
                DistributorName = "Madjarov",
                Description = "From happy porks",
                Price = 9.50m
            };

            var product3 = new Product()
            {
                Name = "Rice",
                DistributorName = "Krina",
                Description = "Best Price",
                Price = 1
            };

            context.Products.AddRange(new[] {product3, product2, product1});

            base.Seed(context);
        }
    }
}
