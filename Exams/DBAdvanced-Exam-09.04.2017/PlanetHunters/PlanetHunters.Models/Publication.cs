using System;
using System.ComponentModel.DataAnnotations;

namespace PlanetHunters.Models
{
    public class Publication
    {
        public int Id { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public virtual Discovery Discovery { get; set; }

        public virtual Journal Journal { get; set; }
        
    }
}
