using System.Collections.Generic;

namespace SalesDB.Models
{
    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public string Description { get; set; }
    }
}
