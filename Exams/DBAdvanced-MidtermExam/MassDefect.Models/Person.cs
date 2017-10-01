using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefect.Models
{
    public class Person
    {
        public Person()
        {
            this.Anomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("HomePlanet")]
        public int HomePlanetId { get; set; }

        public virtual Planet HomePlanet { get; set; }

        public virtual ICollection<Anomaly> Anomalies { get; set; }
    }
}
