using System.ComponentModel.DataAnnotations.Schema;

namespace ProductсShop.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class Product
    {
        public Product()
        {
            this.Categories = new HashSet<Category>();
        }

        public int Id { get; set; }

        [MinLength(3), Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("Buyer")]
        public int? BuyerId { get; set; }

        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        
        public virtual User Seller { get; set; }

        public virtual User Buyer { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

    }
}
