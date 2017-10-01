using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefect.Models
{
    public class Anomaly
    {
        public Anomaly()
        {
            this.Victims = new HashSet<Person>();
        }

        public int Id  { get; set; }

        [ForeignKey("OriginPlanet")]
        [Column("OriginPlanetId")]
        public int OriginPlanetId { get; set; }

        [Required]
        public virtual Planet OriginPlanet { get; set; }

        [ForeignKey("TeleportPlanet")]
        [Column("TeleportPlanetId")]
        public int TeleportPlanetId { get; set; }

        [Required]
        public virtual Planet TeleportPlanet { get; set; }

        public virtual ICollection<Person> Victims { get; set; }
        
    }
}
