namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Discovery
    {
        public Discovery()
        {
            this.Pioneers = new HashSet<Astronomer>();
            this.Observers = new HashSet<Astronomer>();
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime? AnnounceDate { get; set; }

        public virtual ICollection<Astronomer> Pioneers { get; set; }

        public virtual ICollection<Astronomer> Observers { get; set; }

        public virtual ICollection<Star> Stars { get; set; }

        public virtual ICollection<Planet> Planets { get; set; }

        [Required]
        public virtual Telescope TelescopeUsed { get; set; }

        public virtual Publication Publication { get; set; }
    }
}
