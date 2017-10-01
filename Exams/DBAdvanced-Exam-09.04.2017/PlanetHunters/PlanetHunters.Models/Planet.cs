using System;

namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        private decimal? mass;

        [Required]
        public decimal? Mass {
            get
            {
                return this.mass;
                
            }
            set
            {
                if (value <= 0.00m)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    this.mass = value;
                }
            }
        }

        [Required]
        public virtual StarSystem HostStarSystem { get; set; }

        public virtual Discovery Discovery { get; set; }
    }
}
