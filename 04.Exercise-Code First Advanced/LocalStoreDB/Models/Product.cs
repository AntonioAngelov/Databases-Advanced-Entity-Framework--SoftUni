﻿using System.ComponentModel.DataAnnotations;

namespace LocalStoreDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DistributorName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double Weight { get; set; }

        public int Quantity { get; set; }
    }
}
