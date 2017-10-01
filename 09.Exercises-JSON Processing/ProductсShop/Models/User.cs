using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ProductсShop.Models
{
    public class User
    {
        public User()
        {
            this.ProductsBought = new HashSet<Product>();
            this.ProductsSold = new HashSet<Product>();
            this.Friends = new HashSet<User>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        [MinLength(3), Required]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> ProductsSold { get; set; }

        public virtual ICollection<Product> ProductsBought { get; set; }

        public virtual ICollection<User> Friends { get; set; }
    }
}
