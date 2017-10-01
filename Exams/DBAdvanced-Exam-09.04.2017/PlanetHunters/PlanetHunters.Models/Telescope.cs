using System;

namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Telescope
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        private decimal? mirrorDiameter;

        public decimal? MirrorDiameter
        {
            get
            {
                return this.mirrorDiameter;
                
            }
            set
            {
                if (value != null && value <= 0.00m)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (value != null)
                {
                    this.mirrorDiameter = value;
                }
                else
                {
                    this.mirrorDiameter = null;
                }
            }
        }

        public virtual ICollection<Discovery> Discoveries { get; set; }

    }
}
