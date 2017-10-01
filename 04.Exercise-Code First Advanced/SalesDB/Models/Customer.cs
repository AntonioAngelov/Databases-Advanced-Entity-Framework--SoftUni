using System.ComponentModel;

namespace SalesDB.Models
{
    using System.Collections.Generic;

    public class Customer
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int CreditCardNumber { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
