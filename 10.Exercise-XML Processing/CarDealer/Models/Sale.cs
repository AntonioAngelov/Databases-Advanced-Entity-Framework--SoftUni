namespace CarDealer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sale
    {
        public int Id { get; set; }

        public int Discount { get; set; }

        public int CarId { get; set; }

        public int CustomerId { get; set; }

        public virtual Car Car { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
