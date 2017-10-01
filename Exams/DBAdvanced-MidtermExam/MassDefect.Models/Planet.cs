using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassDefect.Models
{
    public class Planet
    {
        public Planet()
        {
            this.People = new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TargettingAnomalies = new HashSet<Anomaly>();
        }
        
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Sun")]
        public int SunId { get; set; }

        [Required]
        public virtual Star Sun { get; set; }

        [ForeignKey("SolarSystem")]
        public int SolarsystemId { get; set; }

        [Required]
        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }

        public virtual ICollection<Anomaly> TargettingAnomalies { get; set; }
    }
}
